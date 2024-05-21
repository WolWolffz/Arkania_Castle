using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayButton(){
        SceneManager.LoadScene("LevelSelection");
    }

    public void ConfigButton(){
        if(AudioManager.instance.GetComponent<AudioSource>().mute){
            GameObject.Find("/Canvas/Config/MuteOff").SetActive(false);
            GameObject.Find("/Canvas/Config/MuteOn").SetActive(true);
        }else{
            GameObject.Find("/Canvas/Config/MuteOff").SetActive(true);
            GameObject.Find("/Canvas/Config/MuteOn").SetActive(false);
        }
    }

    public void AjudaButton(){

    }

    public void LevelButton(){
        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}
