using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAnimationOnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator friendAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        friendAnimator.SetTrigger("PlayerArrives");
    }
}
