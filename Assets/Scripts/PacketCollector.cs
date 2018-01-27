using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCollector : MonoBehaviour
{

    private ParticleSystem particleSystem;
    private PacketType curentPacketType = PacketType.None;

    void Start()
    {
        particleSystem = transform.Find("RedParticleSystem").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Packet packet = collision.GetComponent<Packet>();

        if (packet == null)
        {
            Debug.LogError("Packet collector collided with something else than Packet, " +
                "that's weird, check this! Collided with" + collision.name);
            return;
        }

        packet.Assign(curentPacketType);
    }

    public void CollectPackets()
    {
        if (curentPacketType != PacketType.Good)
        {
            curentPacketType = PacketType.Good;
            particleSystem.startColor = Color.green;
        }
    }

    public void DestroyPackets()
    {
        if (curentPacketType != PacketType.Bad)
        {
            curentPacketType = PacketType.Bad;
            particleSystem.startColor = Color.red;
        }
    }

    public void TurnOff()
    {
        if (curentPacketType != PacketType.None)
        {
            curentPacketType = PacketType.None;
            Color myColor = Color.gray;
            myColor.a = 0.4f;
            particleSystem.startColor = myColor;
        }
    }

    internal void GoIntoBlue()
    {
        if (curentPacketType != PacketType.Blue)
        {
            curentPacketType = PacketType.Blue;
            particleSystem.startColor = Color.blue;
        }
    }
}
