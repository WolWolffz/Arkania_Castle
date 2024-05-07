using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject archer;
    public GameObject soldier;
    public GameObject shielder;
    public GameObject healer;
    public GameObject champion;

    public GameManager gm;
    public Level level;
    public Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        level = gm.level;
        spawnPos =new Vector3(level.transform.GetChild(0).position.x,level.transform.GetChild(0).position.y);
    }

    public void SpawnArcher(){
        Instantiate(archer, spawnPos, Quaternion.identity).SetActive(true);
    }

    public void SpawnSoldier(){
        Instantiate(soldier, spawnPos, Quaternion.identity).SetActive(true);
    }

    public void SpawnShielder(){
        Instantiate(shielder, spawnPos, Quaternion.identity).SetActive(true);
    }   

    public void SpawnHealer(){
        Instantiate(healer, spawnPos, Quaternion.identity).SetActive(true);
    }

    public void SpawnChampion(){
        Instantiate(champion, spawnPos, Quaternion.identity).SetActive(true);
    }

}
