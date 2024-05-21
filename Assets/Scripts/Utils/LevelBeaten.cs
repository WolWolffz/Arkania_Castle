using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "LevelBeaten", menuName = "Scripts/LevelBeaten", order = 1)]

class LevelBeaten : MonoBehaviour{
    public List<char> levelBeaten;
    public LevelBeaten instance;
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

    }

}
