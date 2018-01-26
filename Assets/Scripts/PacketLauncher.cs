using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLauncher : MonoBehaviour {

    private FlyingPacketsContainer flyingPacketsContainer;

    private void Start()
    {
        flyingPacketsContainer = FindObjectOfType<FlyingPacketsContainer>();
    }

    internal void GeneratePacketGameObjectAndLaunch(Packet packet)
    {
        GameObject packetGameObject = generatePacketGameObject(packet);
    }

    private GameObject generatePacketGameObject(Packet packet)
    {
        GameObject packetGameObject = new GameObject();
        Packet p = packetGameObject.AddComponent<Packet>();
        p = packet;
        p.SetState(PacketState.Flying);
        packetGameObject.transform.position = transform.position;
        packetGameObject.transform.rotation = transform.rotation;
        packetGameObject.transform.parent = flyingPacketsContainer.transform;

        SpriteRenderer packetSpriteRenderer = packetGameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        packetSpriteRenderer.sprite = packet.GetSprite();
        packetGameObject.name = packetSpriteRenderer.sprite.name;

        packetGameObject.AddComponent(typeof(Rigidbody2D));
        packetGameObject.AddComponent(typeof(BoxCollider2D));
        return packetGameObject;
    }
}
