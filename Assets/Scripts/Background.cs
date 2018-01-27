using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public float bgSpeed = 10;

    void Update () {
        Vector2 offset = GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.y += Time.deltaTime * bgSpeed;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
    }
}
