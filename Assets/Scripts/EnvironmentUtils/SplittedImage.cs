using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittedImage : MonoBehaviour {

    private FlyingPacketsContainer flyingPacketsContainer;
    private PacketLauncher packetLauncher;

    Dictionary<int, int> lengthToColumsDict = new Dictionary<int, int>()
    {
        {2, 2 },
        {6, 3 },
        {12, 4 },
        {20, 5 },
        {30, 6 }
    };

    private List<GameObject> packetList;
    private Queue<GameObject> packetQueue;
    private int columns;
    private int rows;
    public GameObject packetPrefab;

    private void Awake()
    {
        flyingPacketsContainer = FindObjectOfType<FlyingPacketsContainer>();
        packetLauncher = FindObjectOfType<PacketLauncher>();
    }

    internal void Init(Texture2D texture, PacketType packetType)
    {
        Sprite[] imageSprites = Resources.LoadAll<Sprite>(texture.name);
        columns = lengthToColumsDict[imageSprites.Length];
        rows = columns - 1;

        packetQueue = new Queue<GameObject>();
        packetList = new List<GameObject>();
        int i = 0;
        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < columns; ++col)
            {
                GameObject packetGameObject = generatePacketGameObject(col, row, imageSprites[i++], packetType);
                packetQueue.Enqueue(packetGameObject);
                packetList.Add(packetGameObject);
            }

        Utils.Shuffle(packetQueue);
    }

    private GameObject generatePacketGameObject(int col, int row, Sprite sprite, PacketType packetType)
    {
        Transform parent = FindObjectOfType<PacketLauncher>().transform;
        GameObject packetGameObject = Instantiate(packetPrefab,
            parent.position,
            parent.rotation,
            parent);

        packetGameObject.GetComponent<Packet>().Init(this, col, row, sprite, packetType);

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

    internal bool AllPacketsCollected()
    {
        foreach(GameObject packet in packetList)
        {
            if (packet.GetComponent<Packet>().GetState() != PacketState.Collected)
                return false;
        }

        return true;
    }

    internal bool NoPackageToLaunch()
    {
        foreach (GameObject packet in packetList)
            if (packet.GetComponent<Packet>().GetState() == PacketState.ReadyToLaunch)
                return false;

        return true;
    }

    internal void Enqueue(GameObject packet)
    {
        packet.transform.position = packetLauncher.transform.position;
        packet.GetComponent<Packet>().xSpeed = 0;
        packet.GetComponent<Packet>().SetState(PacketState.ReadyToLaunch);
        packetQueue.Enqueue(packet);
    }
}
