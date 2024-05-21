using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Level level;
    public string gameTurn = "PLAYER"; // PLAYER - ENEMY - BATTLE
    public bool canSpawnAndMove = true;
    public List<Enemy> enemiesList = new List<Enemy>();

    public int playerMana = 4;
    public int playerMaxMana = 4;
    private int enemyMana = 3;
    private int enemyMaxMana = 3;
    private TMP_Text turnText;
    private TMP_Text manaText;
    private Button turnButton;

    public int debugInt;
    private int lastDebugInt;
    private Scene currentScene;

    void Awake()
    {
        instance = this;
        try
        {
            if(GameObject.Find("LevelBeaten").GetComponent<LevelBeaten>().alreadyPlayedVideo)
                CloseVideo(GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>());
            else
                GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>().loopPointReached += CloseVideo;
        }
        catch {}
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            turnButton = GameObject.Find("/Canvas/HUD/Turn").GetComponent<Button>();
            turnText = GameObject.Find("/Canvas/HUD/Turn/Text").GetComponent<TMP_Text>();
            manaText = GameObject.Find("/Canvas/HUD/Mana/Text").GetComponent<TMP_Text>();
        }
        catch {}
    }

    public void CloseVideo(VideoPlayer vp){
        vp.GameObject().SetActive(false);
        foreach(GameObject go in SceneManager.GetActiveScene().GetRootGameObjects()){
            if(go.name == "Canvas"){
                go.SetActive(true);
                break;
            }
        } 
        GameObject.Find("LevelBeaten").GetComponent<LevelBeaten>().alreadyPlayedVideo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (manaText != null)
            manaText.SetText("" + playerMana);
        if (debugInt != lastDebugInt)
        {
            lastDebugInt = debugInt;
            switch (debugInt)
            {
                case 1:
                    Win();
                    break;
                case 2:
                    Defeat();
                    break;
            }
        }
        //check para saber se mudou de scene
        if(currentScene != SceneManager.GetActiveScene()){
            currentScene = SceneManager.GetActiveScene();

            //check para saber quais levels na tela de seleção de level estão liberados
            if(currentScene.buildIndex == SceneManager.GetSceneByName("LevelSelection").buildIndex){
                Button[] components = GameObject.Find("Levels").GetComponentsInChildren<Button>(true);
                foreach(Button comp in components){
                    GameObject.Find("LevelBeaten").GetComponent<LevelBeaten>().levelBeaten.ForEach(c => {if(comp.gameObject.name.ToCharArray().Last() == (c+1)) comp.interactable = true;});
                }
            }
        }
    }

    public void NextTurn()
    {
        switch (gameTurn)
        {
            case "PLAYER":
                gameTurn = "ENEMY";
                enemyMana = enemyMaxMana;
                ShowTurn("INIMIGO");
                turnButton.interactable = false;
                break;

            case "ENEMY":
                gameTurn = "BATTLE";
                ShowTurn("BATALHA");
                break;

            case "BATTLE":
                gameTurn = "PLAYER";
                playerMana = playerMaxMana;
                ShowTurn("PASSAR");
                turnButton.interactable = true;
                break;
        }
        canSpawnAndMove = gameTurn == "PLAYER";
    }

    public void ShowTurn(string turno)
    {
        Debug.Log("TURNO: " + gameTurn);
        turnText.SetText(turno);
    }

    public void SpawnAllie(Allie allie)
    {
        Instantiate(allie, level.allieSpawnPoint.position, Quaternion.identity);
        playerMana -= allie.manaCost;
    }

    public void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy, level.enemySpawnPoint.position, Quaternion.identity);
        enemyMana -= enemy.manaCost;
    }

    public void SpawnEnemies()
    {
        var enemyArena = level.floors[level.floors.Count - 1].arenas[0];

        while (
            enemyMana >= enemiesList.Min(enemy => enemy.manaCost)
            && enemyArena.characterGroup.freeEnemiesSlots > 0
        )
        {
            var x = UnityEngine.Random.Range(0, enemiesList.Count);
            SpawnEnemy(enemiesList[x]);
        }
    }

    public void Defeat()
    {
        GameObject.Find("/Canvas/HUD/Cards Set").SetActive(false);
        GameObject.Find("/Canvas/HUD/Turn").SetActive(false);
        GameObject.Find("/Canvas/HUD/Mana").SetActive(false);

        GameObject canvas = GameObject.Find("/Canvas");
        Instantiate(
            Resources.Load("DefeatScreen", typeof(GameObject)),
            canvas.GetComponent<Transform>().position,
            canvas.GetComponent<Transform>().rotation,
            canvas.transform
        );
    }

    public void Win()
    {
        GameObject.Find("LevelBeaten").GetComponent<LevelBeaten>().levelBeaten.Add(SceneManager.GetActiveScene().name.ToCharArray().Last());

        GameObject.Find("/Canvas/HUD/Cards Set").SetActive(false);
        GameObject.Find("/Canvas/HUD/Turn").SetActive(false);
        GameObject.Find("/Canvas/HUD/Mana").SetActive(false);

        GameObject canvas = GameObject.Find("/Canvas");
        Instantiate(
            Resources.Load("WinScreen", typeof(GameObject)),
            canvas.GetComponent<Transform>().position,
            canvas.GetComponent<Transform>().rotation,
            canvas.transform
        );

        if(SceneManager.GetActiveScene().name == "Level-3")
            GameObject.Find("/Canvas/WinScreen(Clone)/Next Button").SetActive(false);
        
    }

    public void EndGameOption(int option)
    {
        /*
        Lista de cases. A ação do botão para mudança de tela deve passar como parâmetro um int, seguindo um desses casos
        0 = MainMenu
        1 = reiniciar fase ativa
        2 = ir para a proxima fase
        3 = ir para seleção de fase
        */
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("Menu");
                break;
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 3:
                SceneManager.LoadScene("LevelSelection");
                break;
        }
    }

    public void SetMute(bool mute)
    {
        AudioManager.instance.GetComponent<AudioSource>().mute = mute;
    }

    public void ChangeVolume(){
        AudioManager.instance.ChangeVolume();
    }
}
