using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    private List<string> menuScenes = new List<string> { "Menu", "LevelSelection" };
    private string currentScene;

    public AudioClip menuMusic;
    public AudioClip actionMusic;

    public Slider slider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            slider = GameObject.Find("/Canvas/Config/Slider").GetComponent<Slider>();
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        var tempScene = SceneManager.GetActiveScene().name;

        if (tempScene != currentScene)
        {
            currentScene = tempScene;

            var musicToPlay = menuScenes.Contains(tempScene) ? menuMusic : actionMusic;

            if (musicToPlay != audioSource.clip)
                ChangeMusic(musicToPlay);
        }
    }

    void ChangeMusic(AudioClip music)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
    }

    public void ChangeVolume()
    {
        if (slider == null)
            slider = GameObject.Find("/Canvas/Config/Slider").GetComponent<Slider>();
        audioSource.volume = slider.value;
    }
}
