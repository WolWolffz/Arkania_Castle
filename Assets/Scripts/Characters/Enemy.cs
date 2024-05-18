using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        UpdateMovement();

        if (life <= 0)
            Die();
    }

    public void Die(){
        characterGroup.enemies.Remove(this);
        Destroy(gameObject, 1);
    }

    void UpdateMovement()
    {
        if (isFighting)
        {
            if (transform.position != characterGroup.enemyFightPosition) // Posição de batalha
                transform.position = Vector3.Lerp(
                    transform.position,
                    characterGroup.enemyFightPosition,
                    jumpTime*Time.deltaTime
                );
        }
        else if (fightFinishTriggered)
        {
            if (transform.position != movePoints[pointIndex]) // Posição de batalha
                transform.position = Vector3.Lerp(transform.position, movePoints[pointIndex], jumpTime*Time.deltaTime);
            else
                fightFinishTriggered = false;
        }
        else
        {
            if (transform.position == movePoints[pointIndex])
            {
                if (pointIndex < movePoints.Count - 1)
                {
                    pointIndex++;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    movePoints[pointIndex],
                    speed * Time.deltaTime
                );
            }
        }
    }
}
