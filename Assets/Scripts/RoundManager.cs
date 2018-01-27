using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public Texture2D[] goodImages;
    public Texture2D[] badImages;

    private SplittedImage goodSplittedImage, badSplittedImage;
    private PacketLauncher[] packetLaunchers;
    private Countdown countdown;

    public float[] timeBetweenLaunches;

    public int currentRound = 0;

    void Start()
    {
        packetLaunchers = FindObjectsOfType<PacketLauncher>();
        countdown = FindObjectOfType<Countdown>();
        countdown.StartCountdown();
    } 

    public void StartNextRound()
    {
        goodSplittedImage = new SplittedImage(goodImages[currentRound], PacketType.Good);
        badSplittedImage = new SplittedImage(badImages[currentRound], PacketType.Bad);
        InvokeRepeating("LaunchNewPacket", timeBetweenLaunches[currentRound], timeBetweenLaunches[currentRound]);
    }

    public void LaunchNewPacket()
    {
        if (goodSplittedImage.GetUnusedPacketsLeft() + badSplittedImage.GetUnusedPacketsLeft() == 0)
        {
            FinishRound();
            return;
        }

        PacketLauncher randomPacketLauncher = packetLaunchers[UnityEngine.Random.Range(0, packetLaunchers.Length)];
        randomPacketLauncher.GeneratePacketGameObjectAndLaunch(getNextRandomPacketFromRandomImage());
    }

    private void FinishRound()
    {
        CancelInvoke("LaunchNewPacket");
        currentRound++;
        countdown.StartCountdown();
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
