using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAnimationOnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator friendAnimator;
    public Transform cameraLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        friendAnimator.SetTrigger("PlayerArrives");

        GameObject cameraObject = collision.gameObject.GetComponentInChildren<Camera>().transform.gameObject;
        cameraObject.transform.parent = null;

        cameraObject.GetComponent<PanToLocation>().PanTo(cameraLocation.position);
    }
}
