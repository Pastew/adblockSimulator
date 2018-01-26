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

    void Start()
    {
        goodSplittedImage = new SplittedImage(goodImage);
        badSplittedImage = new SplittedImage(badImage);
        packetLaunchers = FindObjectsOfType<PacketLauncher>();
        InvokeRepeating("LaunchNewPacket", 1, 0.4f);
    }

    public void LaunchNewPacket()
    {
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
