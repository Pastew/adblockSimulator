using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour {
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
        if(state == PacketState.MoveToCorrectlyAssigned)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, destinationPosition, Time.deltaTime * 3f);
            if (Vector2.Distance(transform.position, destinationPosition) < 0.01)
            {
                print("DONE");
                transform.position = destinationPosition;
                state = PacketState.CorrectlyAssigned;
            }
        }

        if (state == PacketState.MoveToIncorrectlyAssigned)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, destinationPosition, Time.deltaTime * 3f);

            if (Vector2.Distance(transform.position, destinationPosition) < 0.01)
            {
                transform.position = destinationPosition;
                state = PacketState.IncorrectlyAssigned;
            }
        }
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

    internal void CorrectlyAssign()
    {
        state = PacketState.MoveToCorrectlyAssigned;
        SetupMoveToDestination();
    }

    internal void IncorrectlyAssign()
    {
        state = PacketState.MoveToIncorrectlyAssigned;
        SetupMoveToDestination();
    }

    private void SetupMoveToDestination()
    {
        destinationPosition = new Vector2(100, 3000);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<TrailRenderer>());
        transform.rotation = Quaternion.identity;
    }

    internal void SetType(PacketType type)
    {
        this.packetType = type;
    }
}
