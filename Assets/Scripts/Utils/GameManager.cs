using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Level level;
    public string gameTurn = "PLAYER"; // PLAYER - ENEMY - BATTLE
    public bool isControlEnabled = true;
    
    //private int playerMana = 4;
    //private int enemyMana = 4;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
