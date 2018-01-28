using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPackageBackdoor : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Packet packet = collision.GetComponent<Packet>();
        if(packet.GetPacketType() == PacketType.Bad)
        {
            packet.OnCollected();
        }
    }
}
