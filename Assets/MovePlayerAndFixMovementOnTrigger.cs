using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerAndFixMovementOnTrigger : MonoBehaviour
{
    private GameObject endingElements;

    private void Start()
    {
        endingElements = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement playerMovementScript = collision.gameObject.GetComponent<Movement>();

        endingElements.transform.position = new Vector2(transform.parent.position.x,collision.gameObject.transform.position.y + 1.35f);

        playerMovementScript.FixPlayerYAndMovement(transform.position.y);
    }
}
