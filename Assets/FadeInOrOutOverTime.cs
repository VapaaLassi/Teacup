using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOrOutOverTime : MonoBehaviour
{
    public bool fadeOut = true;
    public bool runAtStart = true;

    public float wait = 5f;
    public float secondsToFade = 15f;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (runAtStart)
        {
            Run();
        }
    }

    public void Run()
    {
        if (fadeOut)
            StartCoroutine(FadeOut());
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(wait);

        while(spriteRenderer.color.a > 0)
        {
            Color fade = spriteRenderer.color;
            fade.a -= Time.deltaTime / secondsToFade;

            spriteRenderer.color = fade;

            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(wait);

        while (spriteRenderer.color.a < 1)
        {
            Color fade = spriteRenderer.color;
            fade.a += Time.deltaTime / secondsToFade;

            spriteRenderer.color = fade;

            yield return null;
        }
    }

}
