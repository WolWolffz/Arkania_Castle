using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int pointIndex = 0;
    protected float jumpTime = speed * 6;
    protected List<Vector3> movePoints = new List<Vector3>();
    
    private AnimationController animationController;
    private Vector3 lastPosition;
    private Vector2 movementFlags;
    private string animationState = "Idle";
    private Character target;
    private float animationDuration = 0;
    private float attackAnimDuration = 0.33f;

    public CharacterGroup characterGroup;
    public bool isMoving = false;
    public bool isFighting = false;
    public bool fightFinishTriggered = false;
    public static float speed = 3;
    public float life = 1;
    public float damage = 1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        lastPosition = transform.position;
        movePoints.Add(transform.position);
        animationController = GetComponent<AnimationController>();
        //Invoke("Attack", 2f);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //UpdateMovement();
        DetectMovement();

        if (characterGroup != null)
            UpdateAnimation();
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

    void UpdateAnimation(string newAnimationState = null)
    {
        if (Time.time > animationDuration)
        {
            if (newAnimationState == null && !fightFinishTriggered)
            {
                if (isFighting)
                {
                    newAnimationState = "Fighting";
                }
                else if (movementFlags.x != 0)
                {
                    newAnimationState = movementFlags.x > 0 ? "Walk Right" : "Walk Left";
                }
                else if (movementFlags.y != 0)
                {
                    newAnimationState = movementFlags.y > 0 ? "Walk Up" : "Walk Down";
                }
                else
                {
                    newAnimationState = "Idle";
                }
            }
            else
            {
                if (newAnimationState == "Attack")
                    animationDuration = Time.time + attackAnimDuration * 1f;
            }

            if (newAnimationState != animationState)
            {
                animationState = newAnimationState;
                animationController.SetAnimation(animationState);
            }
        }
    }

    public void Attack(Character character)
    {
        isFighting = true;
        target = character;
        Invoke("StartAttack", 0.6f);
    }

    private void StartAttack()
    {
        UpdateAnimation("Attack");
        target.life -= damage;
        Invoke("FinishAttack", attackAnimDuration * 1.25f);
    }

    private void FinishAttack()
    {
        fightFinishTriggered = true;
        isFighting = false;
    }
}
