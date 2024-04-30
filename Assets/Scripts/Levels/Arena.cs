using System;
using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         //Select stage
        //         Debug.Log(hit.transform.name);
        //     }
        // }

        // Debug.Log(ways.Count.ToString());
    }

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

    public void MoveTroops(Arena toArena)
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
            characterGroup.MoveTroops(toArena.characterGroup, way);
        }
        else
        {
            gameManager.level.CantMoveTroops(toArena);
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

        if (collision.gameObject.GetComponent<Allie>() != null)
        {
            var component = collision.gameObject.GetComponent<Allie>();

            if (characterGroup.allies.IndexOf(component) < 0)
            {
                characterGroup.allies.Add(component);
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
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // if (collision.gameObject.GetComponent<Allie>() != null)
        // {
        //     var component = collision.gameObject.GetComponent<Allie>();
        //     characterGroup.allies.Remove(component);
        //     characterGroup.OrderTroops();
        // }
    }
}