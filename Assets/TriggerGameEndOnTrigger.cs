using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameEndOnTrigger : MonoBehaviour
{
    public GameObject sittingAnimation;
    public GameObject root;

    public AudioSource gameEndingMusic;

    public GameObject credits;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;

        collision.gameObject.GetComponent<Movement>().TriggerSit();

        collision.gameObject.SetActive(false);

        sittingAnimation.SetActive(true);

        gameEnding = true;
        gameEndingMusic.loop = false;
    }

    private bool gameEnding = false;

    private void Update()
    {
        if (gameEnding && !gameEndingMusic.isPlaying)
        {
            credits.SetActive(true);
        }
    }
}
