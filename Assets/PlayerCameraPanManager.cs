using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraPanManager : PanToLocation
{
    private Transform player;

    public Transform playerCameraReferencePosition;

    public bool chasePlayer = false;

    private void Start()
    {
        player = transform.parent;
    }

    private void Update()
    {
        if (chasePlayer)
            ChasePlayerLogic();
    }

    public void ReturnToPlayer()
    {
        if(activePanning != null)
        {
            StopCoroutine(activePanning);
            activePanning = null;
        }
        ChasePlayer();
    }

    public void PanTo(Vector2 target, float overrideTimeDilation = 0f)
    {
        if(chasePlayer == true)
        {
            chasePlayer = false;
        }
        activePanning = StartCoroutine(PanToCurve(target, overrideTimeDilation, -10f));
    }

    private void ChasePlayer()
    {
        Vector3 target = playerCameraReferencePosition.transform.position;

        startingDistance = Vector2.Distance(transform.position, target);

        chasePlayer = true;    

        //StartCoroutine(ChasePlayerCoroutine(startingDistance));
    }

    private float startingDistance;

    private void ChasePlayerLogic()
    {
        Vector3 target = playerCameraReferencePosition.transform.position;

        if (Vector2.Distance(transform.position, target) > 0.01f)
        {
            target = playerCameraReferencePosition.transform.position;
            float distancePercentage = 1f - Vector2.Distance(transform.position, target) / startingDistance;

            float movementFactor = Mathf.Max(((1 - distancePercentage) * 1/60f), 1f/120f);

            Vector2 nextPosition = transform.position + movementFactor * (target - transform.position);
            transform.position = new Vector3(nextPosition.x, nextPosition.y, -10);
        }
        
    }
}
