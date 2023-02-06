using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraOnTrigger : MonoBehaviour
{
    public Transform cameraLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject cameraObject = collision.gameObject.GetComponentInChildren<Camera>().transform.gameObject;
        cameraObject.transform.parent = null;

        cameraObject.GetComponent<PanToLocation>().PanTo(cameraLocation.position);
    }
}
