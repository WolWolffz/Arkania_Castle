using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Level level;
    public string gameTurn = "PLAYER"; // PLAYER - ENEMY - BATTLE
    public bool canSpawnAndMove = true;
    public List<Enemy> enemiesList = new List<Enemy>();

    public int playerMana = 3;
    public int playerMaxMana = 3;
    public int enemyMana = 3;
    public int enemyMaxMana = 3;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void NextTurn()
    {
        switch (gameTurn)
        {
            case "PLAYER":
                gameTurn = "ENEMY";
                enemyMana = enemyMaxMana;
                break;

            case "ENEMY":
                gameTurn = "BATTLE";
                break;

            case "BATTLE":
                gameTurn = "PLAYER";
                playerMana = playerMaxMana;
                break;
        }

        ShowTurn();
        canSpawnAndMove = gameTurn == "PLAYER";
    }

    public void ShowTurn()
    {
        Debug.Log("TURNO: " + gameTurn);
    }

    public void SpawnAllie(Allie allie){
        Instantiate(allie, level.allieSpawnPoint.position, Quaternion.identity);
        playerMana -= allie.manaCost;
    }

    public void SpawnEnemy(Enemy enemy){
        Instantiate(enemy, level.enemySpawnPoint.position, Quaternion.identity);
        enemyMana -= enemy.manaCost;
    }

    public void SpawnEnemies(){
        var enemyArena = level.floors[level.floors.Count-1].arenas[0];
        
        while(enemyMana >= enemiesList.Min(enemy => enemy.manaCost) && enemyArena.characterGroup.freeEnemiesSlots > 0){
            var x = Random.Range(0, enemiesList.Count);
            SpawnEnemy(enemiesList[x]);
        }
    }
}
