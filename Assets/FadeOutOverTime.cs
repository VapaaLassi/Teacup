using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOverTime : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(5f);

        while(spriteRenderer.color.a > 0)
        {
            Color fade = spriteRenderer.color;
            fade.a -= Time.deltaTime / 15f;

            spriteRenderer.color = fade;

            yield return null;
        }
    }

}
