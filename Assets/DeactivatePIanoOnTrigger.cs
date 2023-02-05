using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePIanoOnTrigger : MonoBehaviour
{
    public int index = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(index == 5)
        {
            FindObjectOfType<PianoSources>().FadeOutMelody();
        } else
        {
            FindObjectOfType<PianoSources>().FadeOutPiano(index);
        }

    }
}
