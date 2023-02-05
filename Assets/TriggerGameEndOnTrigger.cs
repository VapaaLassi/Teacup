using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameEndOnTrigger : MonoBehaviour
{
    public GameObject sittingAnimation;
    public GameObject root;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;

        collision.gameObject.GetComponent<Movement>().TriggerSit();

        collision.gameObject.GetComponentInChildren<Camera>().transform.parent = root.transform;

        collision.gameObject.SetActive(false);

        sittingAnimation.SetActive(true);
    }
}
