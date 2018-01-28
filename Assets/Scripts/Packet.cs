using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour
{
    public int col;
    public int row;
    private Sprite sprite;
    private PacketState state;
    private PacketType packetType;
    public float xSpeed;
    private float rotationSpeed;

    private Vector2 destinationPosition;
    private SplittedImage parentSplittedImage;

    public void Init(SplittedImage parentSplittedImage, int col, int row, Sprite sprite, PacketType packetType)
    {
        this.parentSplittedImage = parentSplittedImage;
        this.packetType = packetType;
        this.col = col;
        this.row = row;
        GetComponent<SpriteRenderer>().sprite = sprite;
        state = PacketState.ReadyToLaunch;
        name = sprite.name;
    }

    private void Update()
    {
        // Ale gunwo, popraw to.

        if (state == PacketState.Flying)
        {
            transform.Translate(Vector3.right * xSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        if (state == PacketState.MovingToDestination)
        {
            //gameObject.transform.position = Vector2.MoveTowards(transform.position, destinationPosition, Time.deltaTime * 5f);
            transform.position = Vector2.Lerp(transform.position, destinationPosition, Time.deltaTime * 4f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10f);

            if (Vector2.Distance(transform.position, destinationPosition) < 0.1)
            {
                transform.position = destinationPosition;
                state = PacketState.Collected;
            }
        }
    }

    internal void OnCorrectlyCollected()
    {
        state = PacketState.MovingToDestination;
        destinationPosition = GameObject.Find("GoodImageResult").transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float w = sr.sprite.bounds.size.x;
        float h = sr.sprite.bounds.size.y;
        destinationPosition.x += col * w + w * 0.5f;
        destinationPosition.y -= row * h + h * 0.5f;
    }

    internal void Launch(float xSpeed, float rotationSpeed)
    {
        state = PacketState.Flying;
        this.xSpeed = xSpeed;
        this.rotationSpeed = rotationSpeed;
        //this.rotationSpeed = 0;
    }

    public PacketState GetState()
    {
        return state;
    }

    internal Sprite GetSprite()
    {
        return sprite;
    }

    internal void SetState(PacketState newState)
    {
        state = newState;
    }

    internal PacketType GetPacketType()
    {
        return packetType;
    }

    internal void SetType(PacketType type)
    {
        this.packetType = type;
    }

    public void GoBackToLauncher()
    {
        parentSplittedImage.Enqueue(this.gameObject);
    }
}

