using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToLocation : MonoBehaviour
{

    public AnimationCurve panCurve;

    public float timeDilation = 10f;

    protected Coroutine activePanning;

    public IEnumerator PanToCurve(Vector2 target, float overrideTimeDilation = 0f, float zCoordinate = 0)
    {
        float time = 0;

        float tempDilation = timeDilation;
        if(overrideTimeDilation != 0)
            tempDilation = overrideTimeDilation;

        Vector2 originalPosition = transform.position;

        while(Vector2.Distance(transform.position, target) > 0.01f)
        {
            Vector2 nextPosition = originalPosition + panCurve.Evaluate(time) * (target - originalPosition);
            transform.position = new Vector3(nextPosition.x,nextPosition.y,zCoordinate);
            time += Time.deltaTime / tempDilation;
            yield return null;
        }
        activePanning = null;
    }
}
