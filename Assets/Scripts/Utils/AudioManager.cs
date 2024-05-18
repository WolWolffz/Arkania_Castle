using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;
    private List<string> menuScenes = new List<string> { "Menu", "LevelSelection" };
    private string currentScene;

    public AudioClip menuMusic;
    public AudioClip actionMusic;

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
    void Start() { }

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
}
