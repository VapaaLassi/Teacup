using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    public AnimationCurve movementCurve;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public enum Direction { Up, Down, Left, Right , UpRight, DownRight, DownLeft, UpLeft};

    private Direction currentDirection = Direction.Down;

    // Start is called before the first frame update

    private bool blockMovement = true;

    bool firstMove = false;

    private AudioSource audioSource;
    private AudioClip[] footsteps;
    private AudioClip[] milksteps;


    private Collider2D myCollider;
    private PanToLocation pan;


    public VisionFade playerVision;

    private Transform LastCheckpoint;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        footsteps = Resources.LoadAll<AudioClip>("Sounds/RegularFootsteps");
        milksteps = Resources.LoadAll<AudioClip>("Sounds/MilkFootsteps");
        myCollider = GetComponent<Collider2D>();
        pan = GetComponent<PanToLocation>();
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

    private int steps = 0;

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


                int frame = ParseFrameFromName(spriteRenderer.sprite.name);
                //if (Mathf.FloorToInt(ongoingTime / 0.75f)  > steps)
                if (IsStep(frame))
                {
                    steps++;
                    audioSource.PlayOneShot(FootstepAudio());
                }

                lastframe = frame;
                //print("curveMultiplier " + curveMultiplier);
            }
        }
        else
        {
            if (moving)
            {
                moving = false;
                steps = 0;
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

    private bool inMilk;

    private AudioClip FootstepAudio()
    {
        if (inMilk)
        {
            return milksteps[Random.Range(0, milksteps.Length)];
        }

        return footsteps[Random.Range(0, footsteps.Length)];
    }

    public void SetMilkStatus(bool status)
    {
        inMilk = status;
    }

    public void ToggleMilkStatus()
    {
        inMilk = !inMilk;
    }

    private int lastframe;

    private bool IsStep(int frame)
    {
        if(frame > 5 && frame < 19 && frame % 3 == 0)
        {
            return frame != lastframe;
        }
        return false;
    }

    private int ParseFrameFromName(string name)
    {
        //print(name + " length is " + name.Length);

        if(name.Length > 23)
        {
            return int.Parse(name.Substring(name.Length - 2));
        }
        else if (name.Length > 22)
        {
            return int.Parse(name[name.Length - 1] + "");
        }

        if(name.Length > 19)
        {
            return int.Parse(name.Substring(name.Length - 2));
        } else
        {
            return int.Parse(name[name.Length - 1] + "");
        }
    }

    private bool IsYMovementFrozen()
    {
        return rigidbody2D.constraints.Equals(RigidbodyConstraints2D.FreezePositionY);
    }

    private void SetAnimationState(float x, float y)
    {
        if (IsYMovementFrozen())
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

        if (Math.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                if(y > 0.1)
                {
                    UpdateDirection(Direction.UpRight, "UpRight");
                    return;
                }
                if(y < -0.1)
                {
                    UpdateDirection(Direction.DownRight, "DownRight");
                    return;
                }

                UpdateDirection(Direction.Right, "Right");
                return;
            }
            else if (x < 0)
            {
                if (y > 0.1)
                {
                    UpdateDirection(Direction.UpLeft, "UpLeft");
                    return;
                }
                if (y < -0.1)
                {
                    UpdateDirection(Direction.DownLeft, "DownLeft");
                    return;
                }

                UpdateDirection(Direction.Left, "Left");
                return;
            }
        }
        else
        {
            if (y > 0)
            {
                if (x > 0.1)
                {
                    UpdateDirection(Direction.UpRight, "UpRight");
                    return;
                }
                if (x < -0.1)
                {
                    UpdateDirection(Direction.UpLeft, "UpLeft");
                    return;
                }
                UpdateDirection(Direction.Up, "Up");

                return;
            }
            else if (y < 0)
            {
                if (x > 0.1)
                {
                    UpdateDirection(Direction.DownRight, "DownRight");
                    return;
                }
                if (x < -0.1)
                {
                    UpdateDirection(Direction.DownLeft, "DownLeft");
                    return;
                }
                UpdateDirection(Direction.Down, "Down");

                return;
            }

        }
    }

    private void UpdateDirection(Direction direction, string trigger)
    {
        print("Moving " + direction.ToString() + " flip status " + spriteRenderer.flipX);

        if(FacingLeft(direction) && spriteRenderer.flipX)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft")
                || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUpLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDownLeft"))
            {
                spriteRenderer.flipX = true;
            } else
            {
                spriteRenderer.flipX = false;
            }
        }

        if(direction == currentDirection && moving)
        {
            return;
        }
        currentDirection = direction;
        animator.SetTrigger(trigger);
        if (FacingLeft(direction))
        {
            spriteRenderer.flipX = true;
        } else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkRight") || animator.GetCurrentAnimatorStateInfo(0).IsName("IdleRight") 
                || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUpRight") || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDownRight"))
            {
                spriteRenderer.flipX = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeft")
                || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkUpLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("WalkDownLeft"))
            {

            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        return;
    }

    private bool FacingLeft(Direction direction)
    {
        return direction == Direction.Left || direction == Direction.UpLeft || direction == Direction.DownLeft;
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

    public void DeadEnd()
    {
        blockMovement = true;
        ReturnToLastCheckpoint();
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


    public void ReturnToLastCheckpoint()
    {
        StartCoroutine(MoveToLocation());
    }

    public void SetCheckPoint(Transform checkPoint)
    {
        LastCheckpoint = checkPoint;
    }


    private IEnumerator MoveToLocation()
    {
        myCollider.enabled = false;

        yield return new WaitForSeconds(2);

        playerVision.FadeOutVision();

        yield return new WaitForSeconds(4);


        yield return pan.PanToCurve(LastCheckpoint.position);

        playerVision.FadeBackVision();

        myCollider.enabled = true;

        yield return new WaitForSeconds(2f);

        animator.SetTrigger("GetUp");

        yield return new WaitForSeconds(1f);

        UnblockMovement();

    }
}
