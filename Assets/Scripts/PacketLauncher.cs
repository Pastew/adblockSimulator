using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLauncher : MonoBehaviour
{
    public float xSpeed = 5;
    public float rotateMax = 5;

    internal void Launch(GameObject packet)
    {
        if (null == packet)
            return;

        packet.GetComponent<Packet>().Launch(
            xSpeed, 
            rotateMax * UnityEngine.Random.Range(-1f, 1f));
    }
}
