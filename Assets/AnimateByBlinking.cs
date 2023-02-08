using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateByBlinking : MonoBehaviour
{

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        InvokeRepeating("Blink", 0.4f, 0.4f);
    }

    void Blink()
    {
        sprite.enabled = !sprite.enabled;
    }

}
