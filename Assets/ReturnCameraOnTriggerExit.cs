using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCameraOnTriggerExit : MonoBehaviour
{
    public PlayerCameraPanManager cameraPanning;

    private void OnTriggerExit2D(Collider2D collision)
    {
        cameraPanning.ReturnToPlayer();
    }
}
