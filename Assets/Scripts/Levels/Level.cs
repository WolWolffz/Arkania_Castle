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

    public void CantMoveTroops(Arena arena)
    {
        Instantiate(deniedFX, arena.transform.position, Quaternion.identity);
        // Debug.Log("CANT MOVE TROOPS: " + arena.name);
    }

    public void ArenaClicked(Arena arena)
    {
        if (fromArena == null)
        {
            if (!arena.isFighting && arena.characterGroup.allies.Count > 0)
            {
                fromArena = arena;
                arena.Select(true);
            }
            else
            {
                CantMoveTroops(arena);
            }
        }
        else
        {
            
                if (arena == fromArena)
                {
                    fromArena = null;
                    arena.Select(false);
                }
                else
                {
                    toArena = arena;
                    arena.Select(true);
                    Invoke("MoveTroops", 0.2f);
                }
            
        }
    }

    private void MoveTroops()
    {
        fromArena.MoveTroops(toArena);
        ClearSelectedArenas();
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
