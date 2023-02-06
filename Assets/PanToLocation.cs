using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToLocation : MonoBehaviour
{

    private Vector2 target;
    public AnimationCurve panCurve;


    public void PanTo(Vector2 target)
    {
        this.target = target;
        StartCoroutine(PanToCurve());
    }

    private IEnumerator PanToCurve()
    {
        float time = 0;

        Vector2 originalPosition = transform.position;

        while(Vector2.Distance(transform.position, target) > 0.01f)
        {
            Vector2 nextPosition = originalPosition + panCurve.Evaluate(time) * (target - originalPosition);
            transform.position = new Vector3(nextPosition.x,nextPosition.y,-10);
            time += Time.deltaTime / 10;
            yield return null;
        }
    }
}
