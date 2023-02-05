using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

public class LowpassBasedOnDistance : MonoBehaviour
{
    public Transform target;
    public AudioMixer mixer;
    public AnimationCurve lowpassCurve;
    public AnimationCurve distortionCurve;

    public AnimationCurve postProcessionCurve;

    public AnimationCurve melodyCurve; 

    private AudioSource mySource;

    public AudioSource melody;

    private float maxdistance = 25f;

    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        maxdistance = mySource.maxDistance;

        if (postProcess.profile.TryGetSettings(out Vignette vignette)) // for e.g set vignette intensity to .4f
        {
            this.vignette = vignette;
        }
        if (postProcess.profile.TryGetSettings(out ChromaticAberration chromatic)) // for e.g set vignette intensity to .4f
        {
            this.chromatic = chromatic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        // Needs to work through the piano system. TODODODODO

        PostProcessingEffects(distance);

        melody.volume = melodyCurve.Evaluate(1 - distance / maxdistance) / 2;
        //print(distance);
        //mixer.SetFloat("Lowpass", lowpassCurve.Evaluate(distance));
        mixer.SetFloat("DistortionLevel", distortionCurve.Evaluate(distance));
    }

    public PostProcessVolume postProcess;
    public Vignette vignette;
    public ChromaticAberration chromatic;

    void PostProcessingEffects(float distance)
    {
        vignette.intensity.Override(postProcessionCurve.Evaluate(1 - (distance + 5) / maxdistance));
        chromatic.intensity.Override(postProcessionCurve.Evaluate(1 - distance / maxdistance));


    }
}
