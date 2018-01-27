using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    public int redColorCounterThreshold = 10000;
    public int boxSize = 10;
    public bool renderCam = true;
    private PacketCollector packetCollector;
    
    void Start()
    {
        webCamTexture = new WebCamTexture();
        packetCollector = FindObjectOfType<PacketCollector>();

        if(renderCam)
            GetComponent<Renderer>().material.mainTexture = webCamTexture;

        webCamTexture.Play();
    }
    
    void Update()
    {
        Color[] pixels = webCamTexture.GetPixels();

        int redPixelsNumber = 0;

        for(int i = 0; i < pixels.Length; ++i)
        {
            if (pixels[i].r > pixels[i].g + pixels[i].b)
                ++redPixelsNumber;
        }

        if (redPixelsNumber > redColorCounterThreshold)
            packetCollector.DestroyPackets();
        else
            packetCollector.TurnOff();

    }
}
