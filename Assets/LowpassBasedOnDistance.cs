using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class LowpassBasedOnDistance : MonoBehaviour
{
    public Transform target;
    public AudioMixer mixer;
    public AnimationCurve lowpassCurve;
    public AnimationCurve distortionCurve;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        //print(distance);
        //mixer.SetFloat("Lowpass", lowpassCurve.Evaluate(distance));
        mixer.SetFloat("DistortionLevel", distortionCurve.Evaluate(distance));
    }
}
