using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Level level;
    public string gameTurn = "PLAYER"; // PLAYER - ENEMY - BATTLE
    public bool canSpawnAndMove = true;
    public List<Enemy> enemiesList = new List<Enemy>();

    public int playerMana = 4;
    public int playerMaxMana = 4;
    public int enemyMana = 4;
    public int enemyMaxMana = 4;
    private TMP_Text turnText;
    private TMP_Text manaText;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() { 
       turnText = GameObject.Find("/Canvas/Turn/Text").GetComponent<TMP_Text>();
       manaText = GameObject.Find("/Canvas/Mana/Text").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update() { 
        manaText.SetText(""+playerMana);
    }

    public void NextTurn()
    {
        switch (gameTurn)
        {
            case "PLAYER":
                gameTurn = "ENEMY";
                enemyMana = enemyMaxMana;
                ShowTurn("BATALHA");
                break;

            case "ENEMY":
                gameTurn = "BATTLE";
                ShowTurn("JOGAR");
                break;

            case "BATTLE":
                gameTurn = "PLAYER";
                playerMana = playerMaxMana;
                ShowTurn("PASSAR");
                break;
        }
        canSpawnAndMove = gameTurn == "PLAYER";
    }

    public void ShowTurn(string turno)
    {
        Debug.Log("TURNO: " + gameTurn);
        turnText.SetText(turno);

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
            var x = UnityEngine.Random.Range(0, enemiesList.Count);
            SpawnEnemy(enemiesList[x]);
        }
    }
}
