﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCollector : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private PacketType currentPacketType = PacketType.None;
    public bool activeTrigger;
    public GameObject boomPrefab;
    private Blower blower;

    private AudioSource audioSource;

    void Awake()
    {
        blower = FindObjectOfType<Blower>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = transform.Find("Blower").GetComponent<ParticleSystem>();
        Invoke("StartParticleSystem", 1f);
    }

    void StartParticleSystem()
    {
        particleSystem.startColor = Color.white;
        particleSystem.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Packet packet = collision.GetComponent<Packet>();
        if (packet.GetState() != PacketState.Flying)
            return;

        PacketType packetType = packet.GetPacketType();

        // user destroys packet
        if (currentPacketType == PacketType.Bad)
        {
            Instantiate(boomPrefab, packet.transform.position, Quaternion.identity).transform.parent = transform;
            //Destroy(packet.gameObject);
            packet.GoBackToLauncher();
        }

        // user collects any packet
        if (currentPacketType == PacketType.Good)
        {
            packet.OnCollected();
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeTrigger = false;
    }

    public void CollectMode()
    {
        if (currentPacketType != PacketType.Good)
        {
            currentPacketType = PacketType.Good;
            particleSystem.startColor = Color.green;
            blower.Set(PacketType.Good);
        }
    }

    public void DestroyMode()
    {
        if (currentPacketType != PacketType.Bad)
        {
            currentPacketType = PacketType.Bad;
            particleSystem.startColor = Color.red;
            blower.Set(PacketType.Bad);

        }
    }

    public void TurnOff()
    {
        if (currentPacketType != PacketType.None)
        {
            currentPacketType = PacketType.None;
            Color myColor = Color.gray;
            myColor.a = 0.4f;
            particleSystem.startColor = myColor;
            blower.Set(PacketType.None);
        }
    }

    internal void GoIntoBlue()
    {
        if (currentPacketType != PacketType.Blue)
        {
            currentPacketType = PacketType.Blue;
            particleSystem.startColor = Color.blue;
        }
    }
}
