using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class Arena : MonoBehaviour
{
    private GameManager gameManager;

    private List<Way> upWays = new List<Way>();
    public List<Way> downWays = new List<Way>();
    public CharacterGroup characterGroup;

    public bool isSelected = false;
    public bool isFighting = false;

    private SpriteRenderer spriteRenderer;
    private Color unselectedColor,
        selectedColor;
    private Vector3 clickOrigin,
        clickDest;
    public bool isPlayerSpawn;
    public bool isEnemySpawn;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterGroup = gameObject.GetComponentInChildren<CharacterGroup>();
    }

    void Start()
    {
        gameManager = GameManager.instance;

        clickDest = Vector3.zero;
        var currentColor = spriteRenderer.color;

        unselectedColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        selectedColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.25f);

        spriteRenderer.color = unselectedColor;
        //Invoke("AttackRound",3);
    }

    void Update() {
        isFighting = characterGroup.allies.Count > 0 && characterGroup.enemies.Count > 0;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickDest = Input.mousePosition;

            if (clickOrigin.y > clickDest.y - 2 && clickOrigin.y < clickDest.y + 2 && gameManager.canSpawnAndMove)
            {
                clickDest = Vector3.up;
                gameManager.level.ArenaClicked(this);
            }
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
        StartCoroutine(AttackRoundDelayed());
    }

    public IEnumerator AttackRoundDelayed()
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
            yield return new WaitForSeconds(0.33f * 1.25f + 1f);
            List<List<Character>> temp = characterGroup.ListsSort(attackers, defensors);
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
        List<Way> orderedDownWays = downWays;

        orderedDownWays = orderedDownWays.OrderByDescending(a => a.bottomArena.characterGroup.freeEnemiesSlots).ToList();
        
        StartCoroutine(MoveEnemiesDelayed(orderedDownWays));
    }

    IEnumerator MoveEnemiesDelayed(List<Way> orderedDownWays){
        for (int i = 0; i < orderedDownWays.Count; i++)
        {
            characterGroup.MoveEnemies(orderedDownWays[i].bottomArena.characterGroup, orderedDownWays[i]);
            yield return new WaitForSeconds(Character.speed * 0.07f * characterGroup.enemies.Count + 2f);
        }
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
