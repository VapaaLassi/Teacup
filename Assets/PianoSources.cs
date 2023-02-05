using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSources : MonoBehaviour
{

    public AudioSource[] pianos;

    private AudioManager manager;

    public AudioSource melody;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
    }

    public void ResetAll()
    {
        foreach (AudioSource item in pianos)
        {
            item.time = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOutMelody()
    {
        manager.FadeOut(melody);
    }

    public void FadeOutPiano(int i)
    {
        manager.FadeOut(pianos[i]);
    }

    private void DeactivatePiano(int i)
    {
        //pianos[i].enabled = false;
    }

    int activePiano = 4;

    public void ActivatePiano(int i)
    {
        activePiano = i;
        //if (i - 2 >= 0)
        //{
        //    DeactivatePiano(i - 2);
        //}
        //if (i - 1 >= 0)
        //{
        //    DeactivatePiano(i - 1);
        //}
        //pianos[i].enabled = true;
    }
}
