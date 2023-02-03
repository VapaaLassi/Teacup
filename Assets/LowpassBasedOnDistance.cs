using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class LowpassBasedOnDistance : MonoBehaviour
{
    public Transform target;
    AudioSource source;
    AudioMixer mixer;
    public AnimationCurve lowpassCurve;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        mixer = source.outputAudioMixerGroup.audioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        print(distance);
        mixer.SetFloat("Lowpass", lowpassCurve.Evaluate(distance));
    }
}
