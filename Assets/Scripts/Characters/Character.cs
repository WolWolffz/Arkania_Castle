using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    private List<Vector3> movePoints = new List<Vector3>();
    private int pointIndex = 0;
    private Vector3 lastPosition;

    public static float speed = 3;
    public Vector2 movementFlags;
    public bool isMoving = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        lastPosition = transform.position;
        movePoints.Add(transform.position);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        UpdateMovement();
        DetectMovement();
    }

    void UpdateMovement()
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

    public void DetectMovement()
    {
        var movement = (transform.position - lastPosition) / Time.deltaTime;

        if (movement.x != 0)
            movementFlags.x = movement.x > 0 ? 1f : -1f;
        else
            movementFlags.x = 0f;

        if (movement.y != 0)
            movementFlags.y = movement.y > 0 ? 1f : -1f;
        else
            movementFlags.y = 0f;

        lastPosition = transform.position;

        isMoving = movementFlags.x != 0 || movementFlags.y != 0;
    }

    public void Move(List<Vector3> newMovePoints)
    {
        movePoints = newMovePoints;
        pointIndex = 0;
    }
}
