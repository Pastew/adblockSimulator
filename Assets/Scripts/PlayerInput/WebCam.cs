using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    public int redThreshold = 10000;
    public int greenThreshold = 20000;
    public int blueThreshold = 150000;

    public int reds = 0;
    public int greens = 0;
    public int blues = 0;

    public bool renderCam = true;

    private WebCamTexture webCamTexture;
    private PacketCollector packetCollector;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        packetCollector = FindObjectOfType<PacketCollector>();

        if (renderCam)
            GetComponent<Renderer>().material.mainTexture = webCamTexture;
        else
            Destroy(GetComponent<Renderer>());

        webCamTexture.Play();
    }

    void Update()
    {
        Color[] pixels = webCamTexture.GetPixels();

        reds = 0;
        greens = 0;
        blues = 0;

        for (int i = 0; i < pixels.Length; ++i)
        {
            if (pixels[i].r > pixels[i].g + pixels[i].b)
                ++reds;

            if (pixels[i].g > pixels[i].r && pixels[i].g > pixels[i].b)
                ++greens;

            if (pixels[i].b > pixels[i].r && pixels[i].b > pixels[i].g)
                ++blues;
        }
            
        if ( greens > greenThreshold)
            packetCollector.CollectPackets();
        else if(reds > redThreshold)
            packetCollector.DestroyPackets();
        else if (blues > blueThreshold)
            packetCollector.GoIntoBlue();
        else
            packetCollector.TurnOff();

    }
}
