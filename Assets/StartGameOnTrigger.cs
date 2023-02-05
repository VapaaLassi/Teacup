using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class StartGameOnTrigger : MonoBehaviour
{
    public AnimationCurve vignetteIntensity;

    public PostProcessVolume postProcess;
    public Vignette vignette;

    public GameObject colliders;
    public GameObject deadEnds;

    private void Start()
    {

        if (postProcess.profile.TryGetSettings(out Vignette vignette)) // for e.g set vignette intensity to .4f
        {
            vignette.intensity.Override(1f);
            this.vignette = vignette;
        }

    }

    void StartGame()
    {
        colliders.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().StartGame();
        StartGame();

        StartCoroutine(FadeOutVignette());
    }

    private IEnumerator FadeOutVignette()
    {
        yield return new WaitForSeconds(1);
        float time = 0;
        while(vignette.intensity > 0.5f)
        {
            vignette.intensity.Override(vignetteIntensity.Evaluate(time));
            time += Time.deltaTime / 2;
            yield return null;
        }
    }
}
