using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInMilkOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().ToggleMilkStatus();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().ToggleMilkStatus();
    }


}
