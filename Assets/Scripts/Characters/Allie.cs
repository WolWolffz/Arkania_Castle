using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Allie : Character
{

    private AudioManager audioManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        audioManager = AudioManager.instance;
        audioSource = GetComponent<AudioSource>();
        // print("audiosource id"+audioSource.GetInstanceID());
        audioSource.mute = audioManager.GetComponent<AudioSource>().mute;
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
        characterGroup.allies.Remove(this);
        Destroy(gameObject, 0.35f);
    }

    void UpdateMovement()
    {
        if (isFighting)
        {
            if (transform.position != characterGroup.allieFightPosition) // Posição de batalha
                transform.position = Vector3.Lerp(
                    transform.position,
                    characterGroup.allieFightPosition,
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
