using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D goodImage;
    [SerializeField]
    private Texture2D badImage;

    private SplittedImage goodSplittedImage, badSplittedImage;
    private PacketLauncher[] packetLaunchers;
    private Countdown countdown;

    public float timeBetweenLaunches = 0.5f;

    void Start()
    {
        goodSplittedImage = new SplittedImage(goodImage, PacketType.Good);
        badSplittedImage = new SplittedImage(badImage, PacketType.Bad);
        packetLaunchers = FindObjectsOfType<PacketLauncher>();
        countdown = FindObjectOfType<Countdown>();
        countdown.StartCountdown();
    } 

    public void StartNextRound()
    {
        InvokeRepeating("LaunchNewPacket", timeBetweenLaunches, timeBetweenLaunches);
    }

    public void LaunchNewPacket()
    {
        if (goodSplittedImage.GetUnusedPacketsLeft() + badSplittedImage.GetUnusedPacketsLeft() == 0)
        {
            CancelInvoke("LaunchNewPacket");
            return;
        }

        PacketLauncher randomPacketLauncher = packetLaunchers[UnityEngine.Random.Range(0, packetLaunchers.Length)];
        randomPacketLauncher.GeneratePacketGameObjectAndLaunch(getNextRandomPacketFromRandomImage());
    }

    private Packet getNextRandomPacketFromRandomImage()
    {
        if (goodSplittedImage.GetUnusedPacketsLeft() + badSplittedImage.GetUnusedPacketsLeft() == 0)
        {
            Debug.LogError("No parts left to shoot. This should NEVER happen. Fix it");
            return null;
        }

        if (goodSplittedImage.GetUnusedPacketsLeft() == 0)
            return badSplittedImage.GetNextPacket();

        if (badSplittedImage.GetUnusedPacketsLeft() == 0)
            return goodSplittedImage.GetNextPacket();

        if (UnityEngine.Random.value > 0.5f)
            return badSplittedImage.GetNextPacket();
        else
            return goodSplittedImage.GetNextPacket();
    }
}
