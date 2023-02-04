using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource baseLayer;
    public AudioSource layer2Extra;

    public AudioSource[] layers;

    private int activeLayers = 2;

    public void ActivateLayer(int layer)
    {
        if(layer > activeLayers)
        {
            for (int i = 0; i < layer - activeLayers; i++)
            {
                if(i == 2)
                {
                    continue;
                }
                FadeOut(layers[i]);
                if(i == 0)
                {
                    FadeOut(layer2Extra);
                }
            }
        }
    }

    public void FadeOut(AudioSource audioSource)
    {
        if(audioSource.volume > 0)
        {
            StartCoroutine(FadeOutLayerVolume(audioSource));
        }
    }


    private void Update()
    {
    }

    private IEnumerator FadeOutLayerVolume(AudioSource audioSource)
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / 2;
            yield return null;
        }
    }

    private IEnumerator FadeInLayerVolume(AudioSource audioSource, float maximumVolume = 1)
    {
        while (audioSource.volume < maximumVolume)
        {
            audioSource.volume += Time.deltaTime / (4 * (1 / maximumVolume));
            yield return null;
        }
    }

    public void TriggerEnding()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if(i == 0)
            {
                StartCoroutine(FadeInLayerVolume(layers[0], 0.25f));
                StartCoroutine(FadeInLayerVolume(layer2Extra, 0.25f));
            } else
            {
                StartCoroutine(FadeInLayerVolume(layers[i]));
            }
        }
    }
}
