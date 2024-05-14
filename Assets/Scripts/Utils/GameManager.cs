using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Level level;
    public string gameTurn = "PLAYER"; // PLAYER - ENEMY - BATTLE
    public bool canSpawnAndMove = true;

    //private int playerMana = 4;
    //private int enemyMana = 4;

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
                break;

            case "ENEMY":
                gameTurn = "BATTLE";
                break;

            case "BATTLE":
                gameTurn = "PLAYER";
                break;
        }

        ShowTurn();
        canSpawnAndMove = gameTurn == "PLAYER";
    }

    public void ShowTurn()
    {
        Debug.Log("TURNO: " + gameTurn);
    }
}
