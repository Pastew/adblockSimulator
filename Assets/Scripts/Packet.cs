using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packet : MonoBehaviour {
    private int col;
    private int row;
    private Sprite sprite;
    private PacketState state;

    public Packet(int col, int row, Sprite sprite)
    {
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
}
