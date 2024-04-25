using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private List<Vector3> movePoints = new List<Vector3>();
    private int pointIndex = 0;

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        movePoints.Add(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement(){
        if(transform.position == movePoints[pointIndex]){
            if(pointIndex < movePoints.Count-1){
                pointIndex++;
            }
        }else{
            transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex], speed * Time.deltaTime);
        }
    }

    public void Move(List<Vector3> newMovePoints){
        movePoints = newMovePoints;
        pointIndex = 0;
    }
}
