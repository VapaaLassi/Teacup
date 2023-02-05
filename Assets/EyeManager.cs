using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EyeManager : MonoBehaviour
{
    // Start is called before the first frame update

    PositionConstraint pupilConstraint;
    Animator animator;

    public SpriteRenderer pupil;

    void Start()
    {
        animator = GetComponent<Animator>();

        pupil.enabled = false;

        pupilConstraint = GetComponentInChildren<PositionConstraint>();

        ConstraintSource playerSource = new ConstraintSource();
        playerSource.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerSource.weight = 1;

        pupilConstraint.AddSource(playerSource);

        pupilConstraint.weight = 0.05f;
    }

    private bool blinking = false;

    private void Update()
    {
        if (blinking)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("EyeOpen"))
            {
                pupil.enabled = true;
                blinking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Blink");
        pupil.enabled = false;
        blinking = true;
        pupilConstraint.constraintActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pupilConstraint.constraintActive = false;
    }


}
