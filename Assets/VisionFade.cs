using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFade : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float timeDilation = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeOutVision()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while(spriteRenderer.color.a < 1)
        {
            Color fade = spriteRenderer.color;
            fade.a = fade.a + Time.deltaTime / timeDilation;

            spriteRenderer.color = fade;

            yield return null;
        }
    }

    public void FadeBackVision()
    {
        StartCoroutine(FadeBack());
    }

    private IEnumerator FadeBack()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color fade = spriteRenderer.color;
            fade.a = fade.a - Time.deltaTime / (timeDilation);

            spriteRenderer.color = fade;

            yield return null;
        }
    }
}
