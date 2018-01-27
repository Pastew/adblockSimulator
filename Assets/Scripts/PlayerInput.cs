using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    PacketCollector packetCollector;

	void Start () {
        packetCollector = FindObjectOfType<PacketCollector>();
	}
	
	void Update () {
        if (Input.GetKeyDown("z"))
            packetCollector.Open();
        else if (Input.GetKeyDown("x"))
            packetCollector.Close();
    }
}
