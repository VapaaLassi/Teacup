using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraOnTrigger : MonoBehaviour
{
    public Transform cameraLocation;
    public float panSpeedOverride = 0;

    private GameObject cameraObject;

    private void Start()
    {
        cameraObject = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().transform.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(cameraObject.transform.parent != null)
            cameraObject.transform.parent = null;

        cameraObject.GetComponent<PlayerCameraPanManager>().PanTo(cameraLocation.position,panSpeedOverride);
    }
}
