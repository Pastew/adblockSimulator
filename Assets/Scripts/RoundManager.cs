using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject packetPrefab;

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
        // Gunwo, popraw kiedyś.
        goodSplittedImage = new GameObject().AddComponent<SplittedImage>();
        goodSplittedImage.gameObject.transform.parent = transform;
        goodSplittedImage.GetComponent<SplittedImage>().packetPrefab = packetPrefab;
        goodSplittedImage.Init(goodImages[currentRound], PacketType.Good);

        badSplittedImage = new GameObject().AddComponent<SplittedImage>();
        badSplittedImage.gameObject.transform.parent = transform;
        badSplittedImage.GetComponent<SplittedImage>().packetPrefab = packetPrefab;
        badSplittedImage.Init(badImages[currentRound], PacketType.Bad);

        InvokeRepeating("LaunchNextPacket", timeBetweenLaunches[currentRound], timeBetweenLaunches[currentRound]);
    }

    public void LaunchNextPacket()
    {
        if (goodSplittedImage.AllPacketsCollected())
        {
            print("Finish round");
            FinishRound();
            return;
        }

        PacketLauncher randomPacketLauncher = packetLaunchers[UnityEngine.Random.Range(0, packetLaunchers.Length)];
        randomPacketLauncher.Launch(GetNextRandomPacketFromRandomImage());
    }

    private void FinishRound()
    {
        CancelInvoke("LaunchNextPacket");
        currentRound++;
        badSplittedImage.Die();
        goodSplittedImage.Die();
        if(currentRound < badImages.Length)
            countdown.StartCountdown();
    }


    private GameObject GetNextRandomPacketFromRandomImage()
    {
        if (goodSplittedImage.NoPackageToLaunch() && badSplittedImage.NoPackageToLaunch())
            return null;

        if (goodSplittedImage.NoPackageToLaunch())
            return badSplittedImage.GetNextPacket();

        if (badSplittedImage.NoPackageToLaunch())
            return goodSplittedImage.GetNextPacket();

        if (UnityEngine.Random.value > 0.5f)
            return badSplittedImage.GetNextPacket();
        else
            return goodSplittedImage.GetNextPacket();
    }
}
