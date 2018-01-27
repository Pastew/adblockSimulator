using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLauncher : MonoBehaviour
{
    public GameObject packetPrefab;
    public float launchForce = 10f;

    private Animator animator;

    private FlyingPacketsContainer flyingPacketsContainer;

    private void Start()
    {
        flyingPacketsContainer = FindObjectOfType<FlyingPacketsContainer>();
    }

    internal void GeneratePacketGameObjectAndLaunch(Packet packet)
    {
        animator = GetComponentInChildren<Animator>();
        GameObject packetGameObject = generatePacketGameObject(packet);
        Vector2 force = packetGameObject.transform.up * launchForce;
        Rigidbody2D rigid = packetGameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(force, ForceMode2D.Impulse);

        rigid.AddTorque(UnityEngine.Random.Range(-200, 200));
        animator.SetTrigger("LaunchTrigger");
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

}
