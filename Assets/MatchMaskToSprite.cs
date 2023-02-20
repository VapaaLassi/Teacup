using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMaskToSprite : MonoBehaviour
{
    private SpriteMask mask;
    private SpriteRenderer sprite;

    private void Start()
    {
        mask = GetComponent<SpriteMask>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mask.sprite = sprite.sprite;
    }
}
