using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arena : MonoBehaviour
{
    private GameManager gameManager;

    private List<Way> upWays = new List<Way>();
    private List<Way> downWays = new List<Way>();
    public CharacterGroup characterGroup;

    public bool isSelected = false;
    public bool isFighting = false;

    private SpriteRenderer spriteRenderer;
    private Color unselectedColor,
        selectedColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterGroup = gameObject.GetComponentInChildren<CharacterGroup>();
    }

    void Start()
    {
        gameManager = GameManager.instance;

        unselectedColor = spriteRenderer.color;
        selectedColor = new Color(
            unselectedColor.r / 2,
            unselectedColor.g / 2,
            unselectedColor.b / 2
        );
    }

    void Update() { }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && gameManager.isControlEnabled)
        {
            gameManager.level.ArenaClicked(this);
        }
    }

    public void Select(bool value)
    {
        isSelected = value;

        if (isSelected)
            spriteRenderer.color = selectedColor;
        else
            spriteRenderer.color = unselectedColor;
    }

    public void AttackRound()
    {
        List<Character> allies = characterGroup.allies.Cast<Character>().ToList();
        List<Character> enemies = characterGroup.enemies.Cast<Character>().ToList();
        
        List<List<Character>> chars = characterGroup.ListsSort(allies, enemies);

        List<Character> attackers = chars[0];
        List<Character> defensors = chars[1];
        for (int i = 0; i < attackers.Count; i++)
        {
            if (defensors[0].life > 0)
            {
                attackers[i].Attack(defensors[0]);
                defensors[0].Attack(attackers[i]);
            }
            List<List<Character>> temp = characterGroup.ListsSort(attackers, defensors);
            attackers = temp[0];
            defensors = temp[1];
        }
        
        characterGroup.OrderAllies();
        characterGroup.OrderEnemies();
    }


    public void MoveAllies(Arena toArena)
    {
        Way way = null;
        foreach (Way upWay in upWays)
        {
            if (upWay.topArena == toArena)
            {
                way = upWay;
                break;
            }
        }

        if (way != null && toArena.characterGroup.freeAlliesSlots > 0)
        {
            characterGroup.MoveAllies(toArena.characterGroup, way);
        }
        else
        {
            gameManager.level.CantMoveAllies(toArena);
        }
    }

    public void MoveEnemies()
    {
        List<Arena> bottomArenas = new List<Arena>();
        foreach (Way way in downWays)
        {
            bottomArenas.Add(way.bottomArena);
        }

        bottomArenas = bottomArenas.OrderByDescending(a => a.characterGroup.freeEnemiesSlots).ToList();

        foreach (Arena bottomArena in bottomArenas){
            if(bottomArena.downWays.Count > 0){
                //characterGroup.MoveEnemies(bottomArena.characterGroup, //way entre as duas);
            }
        }

            // if (toArena.characterGroup.freeEnemiesSlots > 0)
            // {
            //     characterGroup.MoveEnemies(toArena.characterGroup, way);
            // }
            // else
            // {
            //     //gameManager.level.CantMoveAllies(toArena);
            // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Way>() != null)
        {
            var component = collision.gameObject.GetComponent<Way>();

            if (collision.collider.gameObject.name == "Bottom")
                if (upWays.IndexOf(component) < 0)
                    upWays.Add(component);

            if (collision.collider.gameObject.name == "Top")
                if (downWays.IndexOf(component) < 0)
                    downWays.Add(component);
        }
        else if (collision.gameObject.GetComponent<Allie>() != null)
        {
            var component = collision.gameObject.GetComponent<Allie>();

            if (characterGroup.allies.IndexOf(component) < 0)
            {
                characterGroup.allies.Add(component);
                component.characterGroup = characterGroup;
                component.Move(
                    new List<Vector3>
                    {
                        characterGroup.alliesSlotsPositions[
                            characterGroup.allies.IndexOf(component)
                        ]
                    }
                );
            }
        }
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            var component = collision.gameObject.GetComponent<Enemy>();

            if (characterGroup.enemies.IndexOf(component) < 0)
            {
                characterGroup.enemies.Add(component);
                component.characterGroup = characterGroup;
                component.Move(
                    new List<Vector3>
                    {
                        characterGroup.enemiesSlotsPositions[
                            characterGroup.enemies.IndexOf(component)
                        ]
                    }
                );
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // if (collision.gameObject.GetComponent<Allie>() != null)
        // {
        //     var component = collision.gameObject.GetComponent<Allie>();
        //     characterGroup.allies.Remove(component);
        //     characterGroup.OrderAllies();
        // }
    }
}
