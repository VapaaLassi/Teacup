using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerSprites : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        mySpriteRenderer.sprite = playerSpriteRenderer.sprite;
        mySpriteRenderer.flipX = !playerSpriteRenderer.flipX;
    }
}
