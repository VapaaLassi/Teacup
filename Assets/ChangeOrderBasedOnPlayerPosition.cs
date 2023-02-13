using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrderBasedOnPlayerPosition : MonoBehaviour
{
    public SpriteRenderer sprite;
    private Transform referencePoint;
    public Transform player;

    private bool trackPlayerPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        referencePoint = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trackPlayerPosition = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trackPlayerPosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackPlayerPosition)
        {
            if(referencePoint.position.y > player.position.y)
            {
                sprite.sortingOrder = 4;
            } else
            {
                sprite.sortingOrder = 6;
            }
        }
    }
}
