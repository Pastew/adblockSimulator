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
    private Vector2 destinationPosition;

    public Packet(int col, int row, Sprite sprite, PacketType packetType)
    {
        this.packetType = packetType;
        this.col = col;
        this.row = row;
        this.sprite = sprite;
        state = PacketState.NotLaunchedYet;
    }

    private void Update()
    {

        // Ale gunwo, popraw to.
        if (state == PacketState.MovingToDestination)
        {
            //gameObject.transform.position = Vector2.MoveTowards(transform.position, destinationPosition, Time.deltaTime * 5f);
            transform.position = Vector2.Lerp(transform.position, destinationPosition, Time.deltaTime * 4f);

            if (Vector2.Distance(transform.position, destinationPosition) < 0.1)
            {
                transform.position = destinationPosition;
                state = PacketState.CorrectlyAssigned;
            }
        }
    }

    internal void Assign(PacketType assignedTo)
    {
        if (assignedTo == PacketType.Good)
        {
            destinationPosition = GameObject.Find("GoodImageResult").transform.position;
        }
        else if (assignedTo == PacketType.Bad)
        {
            destinationPosition = GameObject.Find("BadImageResult").transform.position;
        }

        Invoke("SetupMoveToDestination", 0.5f);
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

    private void SetupMoveToDestination()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<TrailRenderer>());
        transform.rotation = Quaternion.identity;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float w = sr.sprite.bounds.size.x;
        float h = sr.sprite.bounds.size.y;
        destinationPosition.x += col * w + w * 0.5f;
        destinationPosition.y -= row * h + h * 0.5f;
        //transform.position = destinationPosition;
        state = PacketState.MovingToDestination;
    }

    internal void SetType(PacketType type)
    {
        this.packetType = type;
    }
}
