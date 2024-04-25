using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGroup : MonoBehaviour
{
    private Transform enemiesSlots;
    private Transform alliesSlots;
    private List<Vector3> enemiesSlotsPositions = new List<Vector3>();
    private List<Vector3> alliesSlotsPositions = new List<Vector3>();
    private List<Enemy> enemies = new List<Enemy>();
    private List<Allie> allies = new List<Allie>();
    private int totalEnemiesSlots;
    private int totalAlliesSlots;

    public int freeEnemiesSlots;
    public int freeAlliesSlots;

    // Start is called before the first frame update
    void Start()
    {
        enemiesSlots = transform.Find("Enemies");
        alliesSlots = transform.Find("Allies");

        foreach (Transform child in enemiesSlots)
        {
            enemiesSlotsPositions.Add(child.position);
        }
        foreach (Transform child in alliesSlots)
        {
            alliesSlotsPositions.Add(child.position);
        }

        totalEnemiesSlots = enemiesSlotsPositions.Count;
        totalAlliesSlots = alliesSlotsPositions.Count;

        // Otimizado
        // foreach (Transform child in transform.Find("Enemies")){
        //     enemiesSlots.Add(child.position);
        // }
        // foreach (Transform child in transform.Find("Allies")){
        //     alliesSlots.Add(child.position);
        // }
    }

    // Update is called once per frame
    void Update() { }

    public void MoveTroops(CharacterGroup toCharacterGroup, Way byWay)
    {
        var movePoints = new List<Vector3>
        {
            byWay.bottomPosition,
            byWay.topPosition
        };
        
        for (int i = 0; i < Math.Min(allies.Count, toCharacterGroup.freeAlliesSlots); i++) {
            allies[i].Move(movePoints);
        }
    }
}
