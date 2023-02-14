using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

public class GameManager : MonoBehaviour
{
    bool inTitle = true;

    public Rigidbody2D playerRB;
    public AudioSource teacupBreak;

    public GameObject basicTitle;
    public GameObject animatedTitle;

    public VisionFade playerFade;

    public SpriteRenderer titleBackground;
    public SpriteRenderer animatedTitleSprite;
    public SpriteRenderer anykey;


    public PostProcessVolume postProcessing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inTitle && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } else if (Input.anyKeyDown && inTitle)
        {
            StartGame();
        }
    }


    public void StartGame()
    {
        inTitle = false;
        teacupBreak.Play();

        basicTitle.SetActive(false);
        animatedTitle.SetActive(true);

        StartCoroutine(TimedGameStart());

        // Fade player to view
    }


    private IEnumerator TimedGameStart()
    {
        playerFade.FadeBackVision();

        float time = 0;
        while(time < 1.5f)
        {
            yield return null;
            time += Time.deltaTime;
            postProcessing.weight = 0.5f + time / 4f;
            Color fade = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, playerFade.spriteRenderer.color.a); 
            titleBackground.color = fade;
            animatedTitleSprite.color = fade;
            Color keyfade = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, 1 - time);
            anykey.color = keyfade;
        }
        playerRB.simulated = true;
        while (titleBackground.color.a > 0)
        {
            yield return null;
            time += Time.deltaTime;
            postProcessing.weight = 0.5f + time / 4f;
            Color fade = new Color(titleBackground.color.r, titleBackground.color.g, titleBackground.color.b, playerFade.spriteRenderer.color.a);
            titleBackground.color = fade;
            animatedTitleSprite.color = fade;
        }


    }

}
