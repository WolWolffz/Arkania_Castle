using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private GameManager gameManager;

    private List<Floor> floors = new List<Floor>();
    private Arena fromArena;
    private Arena toArena;

    public GameObject deniedFX;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        gameManager = GameManager.instance;
        gameManager.level = this;

        foreach (Transform child in transform)
        {
            var component = child.GetComponent<Floor>();

            if (component != null)
                floors.Add(component);
        }
    }

    // Update is called once per frame
    void Update() { }

    public void CantMoveAllies(Arena arena)
    {
        Instantiate(deniedFX, arena.transform.position, Quaternion.identity);
        // Debug.Log("CANT MOVE ALLIES: " + arena.name);
    }

    public void ArenaClicked(Arena arena)
    {
        // Se primeira arena não selecionada
        if (fromArena == null)
        {
            // Se não está em batalha e tem alguma tropa
            if (!arena.isFighting && arena.characterGroup.allies.Count > 0)
            {
                fromArena = arena;
                arena.Select(true);
            }
            else
            {
                CantMoveAllies(arena);
            }
        }
        else
        {
            // Se arena clicada já está selecionada
            if (arena == fromArena)
            {
                fromArena = null;
                arena.Select(false);
            }
            else
            {
                toArena = arena;
                arena.Select(true);
                Invoke("MoveAllies", 0.2f);
            }
        }
    }

    private void MoveAllies()
    {
        fromArena.MoveAllies(toArena);
        ClearSelectedArenas();
    }

    private void MoveEnemies()
    {
        List<Arena> arenas = new List<Arena>();

        foreach (Floor floor in floors)
        {
            foreach (Arena arena in floor.arenas)
            {
                if (arena.characterGroup.enemies.Count > 0)
                    arenas.Add(arena);
            }
        }

        arenas.Reverse();
        MoveEnemiesPerArena(arenas);
    }

    IEnumerator MoveEnemiesPerArena(List<Arena> arenas)
    {
        foreach (Arena arena in arenas)
        {
            arena.MoveEnemies();
            yield return new WaitForSeconds(Character.speed * 0.07f * arena.characterGroup.enemies.Count);
        }
    }

    public void ClearSelectedArenas()
    {
        if (fromArena != null)
        {
            fromArena.Select(false);
            fromArena = null;
        }

        if (toArena != null)
        {
            toArena.Select(false);
            toArena = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { }
}
