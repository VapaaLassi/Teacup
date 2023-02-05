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

    public AnimationCurve melodyCurve; 

    private AudioSource mySource;

    public AudioSource melody;

    private float maxdistance = 25f;

    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        maxdistance = mySource.maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        // Needs to work through the piano system. TODODODODO

        melody.volume = melodyCurve.Evaluate(1 - distance / maxdistance) / 2;
        //print(distance);
        //mixer.SetFloat("Lowpass", lowpassCurve.Evaluate(distance));
        mixer.SetFloat("DistortionLevel", distortionCurve.Evaluate(distance));
    }
}
