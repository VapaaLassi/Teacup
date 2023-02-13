using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToLocation : MonoBehaviour
{

    public AnimationCurve panCurve;

    public float timeDilation = 10f;

    public void PanTo(Vector2 target)
    {
        StartCoroutine(PanToCurve(target));
    }

    public IEnumerator PanToCurve(Vector2 target)
    {
        float time = 0;

        Vector2 originalPosition = transform.position;

        while(Vector2.Distance(transform.position, target) > 0.01f)
        {
            Vector2 nextPosition = originalPosition + panCurve.Evaluate(time) * (target - originalPosition);
            transform.position = new Vector3(nextPosition.x,nextPosition.y,-10);
            time += Time.deltaTime / timeDilation;
            yield return null;
        }
    }
}
