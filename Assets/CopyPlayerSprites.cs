using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerSprites : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer mySpriteRenderer;

    public Transform pivot;
    public Transform player;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        mySpriteRenderer.sprite = playerSpriteRenderer.sprite;
        mySpriteRenderer.flipX = !playerSpriteRenderer.flipX;

        Vector2 playerPositionInverted = pivot.position - (player.position - pivot.position);

        transform.position = new Vector2(playerPositionInverted.x, player.position.y + 1.15f);
        
        //transform.position = pivot.position - (player.position - pivot.position);      
    }
}
