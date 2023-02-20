using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EyeManager : MonoBehaviour
{
    // Start is called before the first frame update

    PositionConstraint pupilConstraint;
    Animator animator;
    public Animator maskAnimator;

    public SpriteRenderer pupil;
    private SpriteRenderer eyeSprite;

    private bool dormant = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        eyeSprite = GetComponent<SpriteRenderer>();

        Color transparent = Color.white;
        transparent.a = 0f;
        pupil.color = transparent;
        eyeSprite.color = transparent;

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
        if (dormant)
        {
            StartCoroutine(FadeIn());
        }

        animator.SetTrigger("Blink");
        maskAnimator.SetTrigger("Blink");
        pupil.enabled = false;
        blinking = true;
        pupilConstraint.constraintActive = true;
    }

    private IEnumerator FadeIn()
    {
        while(eyeSprite.color.a < 1)
        {
            Color fade = Color.white;
            fade.a = eyeSprite.color.a + Time.deltaTime;
            eyeSprite.color = fade;
            pupil.color = fade;
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pupilConstraint.constraintActive = false;
    }


}
