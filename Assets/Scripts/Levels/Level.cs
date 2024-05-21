using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    private GameManager gameManager;

    public List<Floor> floors = new List<Floor>();
    private Arena fromArena;
    private Arena toArena;
    private string lastTurn = "PLAYER";
    public List<Arena> fightRunningArenas = new List<Arena>();

    public GameObject deniedFX;
    public Transform enemySpawnPoint;
    public Transform allieSpawnPoint;

    public Deck deckManager;

    public Arena PlayerSpawn;
    public Arena EnemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        enemySpawnPoint = transform.Find("Enemy Spawn");
        allieSpawnPoint = transform.Find("Allie Spawn");

        gameManager = GameManager.instance;
        gameManager.level = this;

        foreach (Transform child in transform)
        {
            var component = child.GetComponent<Floor>();

            if (component != null)
                floors.Add(component);
        }


        floors.ForEach(f =>
        {
            f.arenas.ForEach(a =>
            {
                a.isPlayerSpawn = PlayerSpawn == a ? true : false;
                a.isEnemySpawn = EnemySpawn == a ? true : false;
            });
        });

        deckManager = GameObject.Find("/Canvas/HUD/Cards Set/DeckManager").GetComponent<Deck>();
        
        Invoke("PlayerTurn", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameTurn != lastTurn)
        {
            lastTurn = gameManager.gameTurn;

            switch (gameManager.gameTurn)
            {
                case "PLAYER":
                    PlayerTurn();
                    break;

                case "ENEMY":
                    EnemyTurn();
                    break;

                case "BATTLE":
                    BattleTurn();
                    break;
            }
        }

        if (gameManager.gameTurn == "BATTLE")
        {
            if (fightRunningArenas.Count > 0)
            {
                foreach (Arena arena in fightRunningArenas)
                {
                    if (!arena.fightingRunning)
                        fightRunningArenas.Remove(arena);
                }
            }
            else
            {
                gameManager.NextTurn();
            }
        }
    }

    void PlayerTurn()
    {
        if (CheckWinCondition() == 0)
            deckManager.CallFillHand();
        else if (CheckWinCondition() == 1)
        {
            gameManager.Win();
        }
        else if (CheckWinCondition() == 2)
        {
            gameManager.Defeat();
        }
    }

    int CheckWinCondition()
    {
        //se começar o turno do jogador com uma dessas condições, retorna um resultado equivalente: 1=winm 2=lose,0=neutro
        if (
            PlayerSpawn.characterGroup.enemies.Count > 0
            && PlayerSpawn.characterGroup.allies.Count < 1
        )
            return 2;
        else if (EnemySpawn.characterGroup.allies.Count > 0 && EnemySpawn.characterGroup.enemies.Count < 1)
            return 1;

        return 0;
    }

    void EnemyTurn()
    {
        gameManager.SpawnEnemies();
        Invoke("MoveEnemies", 2);
    }

    void BattleTurn()
    {
        foreach (Floor floor in floors)
        {
            foreach (Arena arena in floor.arenas)
            {
                arena.alreadyMoved = false;
                if (arena.isFighting)
                {
                    fightRunningArenas.Add(arena);
                    arena.AttackRound();
                }
            }
        }
        // Wait Battle Turn flag
    }

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
            // Se não está em batalha, tem alguma tropa e arena de origem ainda não foi utilizada para mover
            if (!arena.isFighting && arena.characterGroup.allies.Count > 0 && !arena.alreadyMoved)
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

        //arenas.Reverse();
        StartCoroutine(MoveEnemiesPerArena(arenas));
    }

    IEnumerator MoveEnemiesPerArena(List<Arena> arenas)
    {
        foreach (Arena arena in arenas)
        {
            if (!arena.isFighting)
            {
                arena.MoveEnemies();
                yield return new WaitForSeconds(
                    Character.speed
                        * 0.07f
                        * arena.downWays.Count
                        * arena.characterGroup.enemies.Count
                        + 2f
                );
            }
        }
        gameManager.NextTurn();
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
