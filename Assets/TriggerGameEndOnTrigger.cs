using System;
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

        gameEndingMusic.loop = false;

        StartCoroutine(TransitionToCredits());
    }

    private IEnumerator TransitionToCredits()
    {
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().FadeOut(gameEndingMusic);
        yield return new WaitForSeconds(4f);
        FindObjectOfType<PlayerCameraPanManager>().PanTo(credits.transform.position, 12f);
        yield return new WaitForSeconds(1f);
        credits.SetActive(true);
    }

}
