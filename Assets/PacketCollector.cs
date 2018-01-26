using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketCollector : MonoBehaviour {

    private enum Colors { Red, Green };
    private ParticleSystem redParticleSystem, greenParticleSystem;

    void Start()
    {
        redParticleSystem = transform.Find("RedParticleSystem").GetComponent<ParticleSystem>();
        greenParticleSystem = transform.Find("GreenParticleSystem").GetComponent<ParticleSystem>();
    }

    void Update()
    {

    }

    public void Open() { SetColor(Colors.Green); }

    public void Close() { SetColor(Colors.Red); }

    private void SetColor(Colors color)
    {
        if (color == Colors.Red)
        {
            redParticleSystem.Play();
            greenParticleSystem.Stop();
        }
        else
        {
            redParticleSystem.Stop();
            greenParticleSystem.Play();
        }
    }
}
