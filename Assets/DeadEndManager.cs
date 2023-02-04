using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEndManager : MonoBehaviour
{
    public Transform[] deadEnds;
    public Transform player;

    private void Start()
    {
        InvokeRepeating("CheckDeadEnds", 1, 1);
    }

    public void CheckDeadEnds()
    {
        int closest = 0;
        float closestDistance = 10000;
        for (int i = 0; i < deadEnds.Length; i++)
        {
            float distance = Vector2.Distance(player.position, deadEnds[i].position);

            if(distance < closestDistance)
            {
                closest = i;
                closestDistance = distance;
            }

        }

        ActivateDeadEnd(closest);
    }

    private void ActivateDeadEnd(int index)
    {
        for (int i = 0; i < deadEnds.Length; i++)
        {
            if(index == i)
            {
                deadEnds[i].gameObject.SetActive(true);
            } else
            {
                deadEnds[i].gameObject.SetActive(false);
            }
        }
    }


    // Might need toggling logic for dead ends. Due to the way the distance algorithm works, they would be fighting each other.


}
