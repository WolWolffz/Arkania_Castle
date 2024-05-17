using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayButton(){
        SceneManager.LoadScene("LevelSelection");
    }

    public void LevelButton(){
        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}
