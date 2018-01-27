using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLauncher : MonoBehaviour
{
    public float launchForce = 10f;

    private Animator animator;

    internal void Launch(GameObject packet)
    {
        animator = GetComponentInChildren<Animator>();
        Vector2 force = packet.transform.up * launchForce;
        Rigidbody2D rigid = packet.GetComponent<Rigidbody2D>();
        rigid.AddForce(force, ForceMode2D.Impulse);

        rigid.AddTorque(UnityEngine.Random.Range(-200, 200));
        animator.SetTrigger("LaunchTrigger");
    }
}
