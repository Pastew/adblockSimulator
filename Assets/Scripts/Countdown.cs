using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    private SpriteRenderer sr;
    public float delayBetweenSeconds = 1f;
    public Sprite[] sprites;

    private int currentSprite;

    public void StartCountdown()
    {
        currentSprite = 0;
        InvokeRepeating("NextNumber", 0, delayBetweenSeconds);
        sr = GetComponent<SpriteRenderer>();
        GetComponent<AudioSource>().Play();
    }

    void NextNumber()
    {
        if (currentSprite == sprites.Length)
        {
            CancelInvoke();
            FindObjectOfType<RoundManager>().StartNextRound();
            AudioSource music = FindObjectOfType<MusicManager>().GetComponent<AudioSource>();
            if(!music.isPlaying)
                music.Play();

            sr.sprite = null;   
            return;
        }

        sr.sprite = sprites[currentSprite++];
    }

}
