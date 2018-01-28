using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCollector : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private PacketType currentPacketType = PacketType.None;

    void Start()
    {
        particleSystem = transform.Find("ParticleSystem").GetComponent<ParticleSystem>();
        Invoke("StartParticleSystem", 1f);
    }

    void StartParticleSystem()
    {
        particleSystem.startColor = Color.white;
        particleSystem.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered");
        Packet packet = collision.GetComponent<Packet>();

        if (packet == null)
        {
            Debug.LogError("Packet collector collided with something else than Packet, " +
                "that's weird, check this! Collided with" + collision.name);
            return;
        }

        // Packet is bad and user destroys it
        if(packet.GetPacketType() == currentPacketType && currentPacketType == PacketType.Bad)
        {
            Destroy(packet.gameObject);
        }
    }

    public void CollectPackets()
    {
        if (currentPacketType != PacketType.Good)
        {
            currentPacketType = PacketType.Good;
            particleSystem.startColor = Color.green;
        }
    }

    public void DestroyPackets()
    {
        if (currentPacketType != PacketType.Bad)
        {
            currentPacketType = PacketType.Bad;
            particleSystem.startColor = Color.red;
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
