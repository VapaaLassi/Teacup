using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneAtTheEndOfSong : MonoBehaviour
{
    private AudioSource creditsSong;
    public AudioManager manager;


    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        creditsSong = GetComponent<AudioSource>();
        
    }

    bool waitingForCreditsSongtoEnd = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (!waitingForCreditsSongtoEnd && !manager.endingMix.isPlaying)
        {
            if (!creditsSong.isPlaying && timer < 10)
            {
                creditsSong.Play();
            }
            waitingForCreditsSongtoEnd = true;
        }

        if (!creditsSong.isPlaying && waitingForCreditsSongtoEnd)
        {
            waitingForCreditsSongtoEnd = false;
            SceneManager.LoadScene("Cracks");
        }   
    }
}
