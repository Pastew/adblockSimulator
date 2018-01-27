using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittedImage : MonoBehaviour {

    private FlyingPacketsContainer flyingPacketsContainer;

    Dictionary<int, int> lengthToColumsDict = new Dictionary<int, int>()
    {
        {2, 2 },
        {6, 3 },
        {12, 4 },
        {20, 5 },
        {30, 6 }
    };

    private Queue<GameObject> packetQueue;
    private int columns;
    private int rows;
    public GameObject packetPrefab;

    private void Awake()
    {
        flyingPacketsContainer = FindObjectOfType<FlyingPacketsContainer>();
    }

    internal void Init(Texture2D texture, PacketType packetType)
    {
        Sprite[] imageSprites = Resources.LoadAll<Sprite>(texture.name);
        columns = lengthToColumsDict[imageSprites.Length];
        rows = columns - 1;

        packetQueue = new Queue<GameObject>();
        int i = 0;
        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < columns; ++col)
            {
                GameObject packetGameObject = generatePacketGameObject(col, row, imageSprites[i++], packetType);
                packetQueue.Enqueue(packetGameObject) ;
            }

        Utils.Shuffle(packetQueue);
    }

    private GameObject generatePacketGameObject(int col, int row, Sprite sprite, PacketType packet)
    {
        Transform parent = FindObjectOfType<PacketLauncher>().transform;
        GameObject packetGameObject = Instantiate(packetPrefab,
            parent.position,
            parent.rotation,
            parent);

        Packet p = packetGameObject.GetComponent<Packet>();
        p.SetType(packet);
        p.col = col;
        p.row = row;
        SpriteRenderer sr = packetGameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        packetGameObject.name = sr.sprite.name;

        return packetGameObject;
    }

    internal GameObject GetNextPacket()
    {
        if(packetQueue.Count == 0)
        {
            Debug.LogError("There is no packet left! This should NEVER happen. Fix this!");
            return null;
        }

        return packetQueue.Dequeue();
    }

    internal int GetUnusedPacketsLeft()
    {
        int unusedPacketsLeft = 0;
        foreach(GameObject packet in packetQueue)
        {
            if (packet.GetComponent<Packet>().GetState() == PacketState.NotLaunchedYet)
                ++unusedPacketsLeft;
        }

        return unusedPacketsLeft;
    }

    internal void Enqueue(GameObject packet)
    {
        packetQueue.Enqueue(packet);
    }
}
