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
        GetComponent<SpriteRenderer>().enabled = false;
        
        arena = GetComponentInParent<Arena>();
        enemiesSlots = transform.Find("Enemies");
        alliesSlots = transform.Find("Allies");

        var enemyFight = transform.Find("Enemy Fight");
        var allieFight = transform.Find("Allie Fight");
        
        enemyFight.GetComponent<SpriteRenderer>().enabled = false;
        allieFight.GetComponent<SpriteRenderer>().enabled = false;

        enemyFightPosition = enemyFight.position;
        allieFightPosition = allieFight.position;


        foreach (Transform child in enemiesSlots)
        {
            child.GetComponent<SpriteRenderer>().enabled = false;
            enemiesSlotsPositions.Add(child.position);
        }
        foreach (Transform child in alliesSlots)
        {
            child.GetComponent<SpriteRenderer>().enabled = false;
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

    public void MoveAllies(CharacterGroup toCharacterGroup, Way byWay)
    {
        StartCoroutine(MoveAlliesDelayed(toCharacterGroup, byWay));
    }

    

    IEnumerator MoveAlliesDelayed(CharacterGroup toCharacterGroup, Way byWay){
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
        OrderAllies();
    }
    
    public List<List<Character>> ListsSort(List<Character> allies, List<Character> enemies)
    {
        List<List<Character>> result = new List<List<Character>>();
        if (allies.Count > enemies.Count || allies.Count == enemies.Count)
        {
            var orderedAllies = allies.OrderByDescending(c => c.damage).ToList();
            var orderedEnemies = enemies.OrderByDescending(c => c.life).ToList();
            result.Add(orderedAllies);
            result.Add(orderedEnemies);
            return result;
        }
        else
        {
            allies = allies.OrderByDescending(c => c.life).ToList();
            enemies = enemies.OrderByDescending(c => c.damage).ToList();
            result.Add(enemies);
            result.Add(allies);
            return result;
        }

    }

    public void MoveEnemies(CharacterGroup toCharacterGroup, Way byWay)
    {
        StartCoroutine(MoveEnemiesDelayed(toCharacterGroup, byWay));
    }


    IEnumerator MoveEnemiesDelayed(CharacterGroup toCharacterGroup, Way byWay){
        int nEnemies = enemies.Count;
        int nFreeEnemiesSlots = toCharacterGroup.freeEnemiesSlots;
        int nMoves = Math.Min(nEnemies, nFreeEnemiesSlots);

        List<Enemy> toRemove = new List<Enemy>();

        for (int i = 0; i < nMoves; i++)
        {
            var movePoints = new List<Vector3>
            {
                byWay.topPosition,
                byWay.bottomPosition,
            };
            enemies[i].Move(movePoints);
            toRemove.Add(enemies[i]);
            yield return new WaitForSeconds(Character.speed * 0.06f);
        }
        foreach(Enemy enemy in toRemove) enemies.Remove(enemy);
        
        OrderEnemies();
    }

    public void OrderAllies(){
        foreach(Allie allie in allies){
            if (!allie.isMoving)
                allie.Move(new List<Vector3> { alliesSlotsPositions[allies.IndexOf(allie)] });
        }
    }

    public void OrderEnemies(){
        foreach(Enemy enemy in enemies){
            if (!enemy.isMoving)
                enemy.Move(new List<Vector3> { enemiesSlotsPositions[enemies.IndexOf(enemy)] });
        }
    }

    
}
