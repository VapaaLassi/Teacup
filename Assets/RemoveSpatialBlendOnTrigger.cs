using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSpatialBlendOnTrigger : MonoBehaviour
{
    AudioSource source;

    public int layerNumber;

    public bool piano;

    AudioManager manager;

    SpriteRenderer spriteRenderer;  

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        // If something goes wrong with playing the music later, it's because we parse the layer from the file name.
        if (!piano)
        {
            layerNumber = int.Parse(source.clip.name[0] + "");
            if (layerNumber == 1)
                layerNumber = 10;
            layerNumber -= 2;
        }

        manager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(FadeOutSpatialBlend());
        if(!piano)
            manager.ActivateLayer(layerNumber);
        else
            FindObjectOfType<PianoSources>().ActivatePiano(layerNumber);
    }

    IEnumerator FadeOutSpatialBlend()
    {
        while (source.spatialBlend > 0)
        {
            source.spatialBlend -= Time.deltaTime / 2;
            yield return null;
        }
        spriteRenderer.enabled = false;
    }

}
