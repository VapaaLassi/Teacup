using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSetMilkStatus : MonoBehaviour
{
    public bool status = false;

    //public PushPlayerWithinBounds pushPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().SetMilkStatus(status);
        //if(pushPlayer != null)
        //{
        //    pushPlayer.SetPushing(status);
        //}
    }
}
