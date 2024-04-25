using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroup : MonoBehaviour
{
    private Transform enemies;
    private Transform allies;
    private List<Vector3> enemiesSlots = new List<Vector3>();
    private List<Vector3> alliesSlots = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        enemies = transform.Find("Enemies");
        allies = transform.Find("Allies");

        foreach (Transform child in enemies){
            enemiesSlots.Add(child.position);
        }
        foreach (Transform child in allies){
            alliesSlots.Add(child.position);
        }

        // Otimizado
        // foreach (Transform child in transform.Find("Enemies")){
        //     enemiesSlots.Add(child.position);
        // }
        // foreach (Transform child in transform.Find("Allies")){
        //     alliesSlots.Add(child.position);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
