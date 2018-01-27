using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour {
    public int col;
    public int row;
    private Sprite sprite;
    private PacketState state;
    private int packetType;

    public Packet(int col, int row, Sprite sprite, int packetType)
    {
        this.packetType = packetType;
        this.col = col;
        this.row = row;
        this.sprite = sprite;
        state = PacketState.NotLaunchedYet;
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

    internal int GetPacketType()
    {
        return packetType;
    }

    internal void CorrectlyAssign()
    {
        state = PacketState.CorrectlyAssigned;
        print("success");

    }

    internal void IncorrectlyAssign()
    {
        state = PacketState.IncorrectlyAssigned;
        print("Fail");
    }

    internal void SetType(int type)
    {
        this.packetType = type;
    }
}
