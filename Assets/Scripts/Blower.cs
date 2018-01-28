using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{

    public AudioClip redAudioClip, greenAudioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    internal void Set(PacketType packetType)
    {
        if (packetType == PacketType.None)
        {
            audioSource.Stop();
            return;
        }

        if (packetType == PacketType.Bad)
            audioSource.clip = redAudioClip;
        else if (packetType == PacketType.Good)
            audioSource.clip = greenAudioClip;

        audioSource.Play();
    }
}
