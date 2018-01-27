using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLauncher : MonoBehaviour
{
    public GameObject packetPrefab;
    public float launchForce = 10f;

    private FlyingPacketsContainer flyingPacketsContainer;

    private void Start()
    {
        flyingPacketsContainer = FindObjectOfType<FlyingPacketsContainer>();
    }

    internal void GeneratePacketGameObjectAndLaunch(Packet packet)
    {
        GameObject packetGameObject = generatePacketGameObject(packet);
        Vector2 force = packetGameObject.transform.up * launchForce;
        Rigidbody2D rigid = packetGameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(force, ForceMode2D.Impulse);

        rigid.AddTorque(UnityEngine.Random.Range(-200, 200));
    }

    private GameObject generatePacketGameObject(Packet packet)
    {
        GameObject packetGameObject = Instantiate(packetPrefab, 
            transform.position, 
            transform.rotation, 
            flyingPacketsContainer.transform);

        Packet p = packetGameObject.GetComponent<Packet>();
        p.SetState(PacketState.Flying);
        p.SetType(packet.GetPacketType());
        p.col = packet.col;
        p.row = packet.row;
        SpriteRenderer sr = packetGameObject.GetComponent<SpriteRenderer>();
        sr.sprite = packet.GetSprite();
        packetGameObject.name = sr.sprite.name;

        return packetGameObject;
    }

    private GameObject generatePacketGameObjectInCode(Packet packet)
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
