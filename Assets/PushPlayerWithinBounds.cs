using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerWithinBounds : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private bool pushing = false;

    public float pushStrength = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        pushing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pushing = false;   
    }

    private void Update()
    {
        if (pushing)
        {
            playerRb.AddForce(transform.right * Time.deltaTime * pushStrength);
        }
    }

    internal void SetPushing(bool status)
    {
        pushing = status;
    }
}
