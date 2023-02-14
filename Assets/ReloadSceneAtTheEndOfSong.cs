using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneAtTheEndOfSong : MonoBehaviour
{
    private AudioSource creditsSong;

    // Start is called before the first frame update
    void Start()
    {
        creditsSong = GetComponent<AudioSource>();
        if (!creditsSong.isPlaying)
        {
            creditsSong.Play();
        }
    }

    bool waitingForSong = true;

    // Update is called once per frame
    void Update()
    {
        if (!creditsSong.isPlaying && waitingForSong)
        {
            waitingForSong = false;
            SceneManager.LoadScene("Cracks");
        }   
    }
}
