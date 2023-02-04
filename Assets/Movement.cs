using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public AnimationCurve movementCurve;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public enum Direction { Up, Down, Left, Right };

    private Direction currentDirection = Direction.Down;

    // Start is called before the first frame update
    void Start()
    {
    }

    public float speed = 5;

    bool moving = false;
    float ongoingTime;

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float curveMultiplier = 1;

        //print("x: " + x + " y: " + y);
        SetAnimationState(x, y);

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            if (!moving)
            {
                moving = true;
                ongoingTime = 0;
            }
            else
            {
                ongoingTime += Time.deltaTime;
                curveMultiplier = movementCurve.Evaluate((ongoingTime % 6f)/3);
                //print("curveMultiplier " + curveMultiplier);
            }
        } else
        {
            if (moving)
            {
                moving = false;
            }
        }

        //print(moving);

        float speedMultiplier = speed * Time.deltaTime * curveMultiplier;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier *= 2;
        }

        //print(speedMultiplier);
        animator.SetBool("Moving", moving);

        transform.Translate(x * speedMultiplier, y * speedMultiplier,0);
    }

    private void SetAnimationState(float x, float y)
    {
        if (Math.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                UpdateDirection(Direction.Right, "Right");
                return;
            }
            else if (x < 0)
            {
                UpdateDirection(Direction.Left, "Left");

                return;
            }
        }
        else
        {
            if (y > 0)
            {
                UpdateDirection(Direction.Up, "Up");

                return;
            }
            else if (y < 0)
            {
                UpdateDirection(Direction.Down, "Down");

                return;
            }

        }
    }

    private void UpdateDirection(Direction direction, string trigger)
    {
        if(direction == currentDirection && moving)
        {
            return;
        }
        currentDirection = direction;
        animator.SetTrigger(trigger);
        if (direction == Direction.Left)
        {
            spriteRenderer.flipX = true;
        } else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft"))
            {

            } else
            {
                spriteRenderer.flipX = false;
            }
        }
        return;
    }
}
