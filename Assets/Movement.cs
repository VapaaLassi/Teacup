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

    private bool blockMovement = true;

    bool firstMove = false;

    void Start()
    {
    }

    public float speed = 5;

    bool moving = false;
    float ongoingTime;

    // Update is called once per frame
    void Update()
    {
        if(!blockMovement)
            HandleMovement();
    }

    private void HandleMovement()
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
                if (!firstMove)
                {
                    firstMove = true;
                    FindObjectOfType<FadeInMelody>().FadeIn();
                    FindObjectOfType<AudioManager>().ResetAll();
                }
                ongoingTime = 0;
            }
            else
            {
                ongoingTime += Time.deltaTime;
                curveMultiplier = movementCurve.Evaluate((ongoingTime % 0.75f));
                //print("curveMultiplier " + curveMultiplier);
            }
        }
        else
        {
            if (moving)
            {
                moving = false;
            }
        }

        //print(moving);

        float speedMultiplier = speed * Time.deltaTime * curveMultiplier;

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    speedMultiplier *= 2;
        //}

        //print(speedMultiplier);
        animator.SetBool("Moving", moving);

        //rigidbody2D.MovePosition((Vector2)transform.position + new Vector2(x * speedMultiplier, y * speedMultiplier));

        if (IsYMovementFrozen())
        {
            y = 0;
        }
        transform.Translate(x * speedMultiplier, y * speedMultiplier, 0);
    }

    private bool IsYMovementFrozen()
    {
        return rigidbody2D.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
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
        else if(!IsYMovementFrozen())
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

    private Rigidbody2D rigidbody2D;

    internal void StartGame()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
        blockMovement = false;
        animator.SetTrigger("Impact");
    }

    internal void TriggerSit()
    {
        blockMovement = true;
        animator.SetTrigger("Ending");
    }

    public void BlockMovement()
    {
        blockMovement = true;
    }

    public void UnblockMovement()
    {
        blockMovement = false;
    }

    internal void FixPlayerYAndMovement(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
}
