using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterGroup : MonoBehaviour
{
    private Arena arena;
    private Transform enemiesSlots;
    private Transform alliesSlots;
    private int totalEnemiesSlots;
    private int totalAlliesSlots;

    public List<Vector3> enemiesSlotsPositions = new List<Vector3>();
    public List<Vector3> alliesSlotsPositions = new List<Vector3>();
    public List<Allie> allies = new List<Allie>();
    public List<Enemy> enemies = new List<Enemy>();
    public Vector3 allieFightPosition;
    public Vector3 enemyFightPosition;
    public int freeEnemiesSlots;
    public int freeAlliesSlots; 

    // Start is called before the first frame update
    void Start()
    {
        arena = GetComponentInParent<Arena>();
        enemiesSlots = transform.Find("Enemies");
        alliesSlots = transform.Find("Allies");

        enemyFightPosition = transform.Find("Enemy Fight").position;
        allieFightPosition = transform.Find("Allie Fight").position;

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
    void Update()
    {
        freeEnemiesSlots = Math.Abs(totalEnemiesSlots - enemies.Count);
        freeAlliesSlots = Math.Abs(totalAlliesSlots - allies.Count);
    }

    public void MoveTroops(CharacterGroup toCharacterGroup, Way byWay)
    {
        StartCoroutine(MoveTroopsDelayed(toCharacterGroup, byWay));
    }

    IEnumerator MoveTroopsDelayed(CharacterGroup toCharacterGroup, Way byWay){
        int nAllies = allies.Count;
        int nFreeAlliesSlots = toCharacterGroup.freeAlliesSlots;
        int nMoves = Math.Min(nAllies, nFreeAlliesSlots);
        List<Allie> toRemove = new List<Allie>();

        for (int i = 0; i < nMoves; i++)
        {
            var movePoints = new List<Vector3>
            {
                byWay.bottomPosition,
                byWay.topPosition,
            };
            allies[i].Move(movePoints);
            toRemove.Add(allies[i]);

            yield return new WaitForSeconds(Character.speed * 0.06f);
        }
        foreach(Allie allie in toRemove) allies.Remove(allie);
        arena.isFighting = allies.Count > 0 && enemies.Count > 0;
        OrderTroops();
    }

    public void OrderTroops(){
        foreach(Allie allie in allies){
            if (!allie.isMoving)
                allie.Move(new List<Vector3> { alliesSlotsPositions[allies.IndexOf(allie)] });
        }
    }
}
