using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMelody : MonoBehaviour
{
    AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();    
    }

    public void FadeIn()
    {
        m_AudioSource.volume = 0.1f;

        StartCoroutine(FadeInOverTime());

    }

    private IEnumerator FadeInOverTime()
    {
        while (m_AudioSource.volume < 0.5f)
        {
            yield return null;

            m_AudioSource.volume += Time.deltaTime / 4;
        }
    }
}
