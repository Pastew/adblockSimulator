using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableTexture : MonoBehaviour {

    public Vector2 translateVector;

    void Update () {
        Vector2 offset = GetComponent<MeshRenderer>().material.mainTextureOffset;
        offset.x += Time.deltaTime * translateVector.x;
        offset.y += Time.deltaTime * translateVector.y;
        GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
    }
}
