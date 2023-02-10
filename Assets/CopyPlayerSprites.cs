using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerSprites : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer mySpriteRenderer;

    public Transform pivot;
    public Transform fadePivot;
    public Transform player;

    public float fadingStrength = 1;

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

        Color fade = mySpriteRenderer.color;

        fade.a = 1 - Vector2.Distance(transform.position, fadePivot.position) / fadingStrength;

        mySpriteRenderer.color = fade;

        float skew = 1 - Mathf.Abs(transform.position.y - pivot.position.y) / fadingStrength;

        transform.position = new Vector2(playerPositionInverted.x + skew, player.position.y + 1.15f);


        //transform.position = pivot.position - (player.position - pivot.position);      
    }
}
