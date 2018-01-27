using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSpritesScrool : MonoBehaviour {

    public GameObject go1, go2;
    public float moveSpeed = 0.1f;

    private GameObject nextToGoBack, theSecondOne;
    Vector2 startPos;

	void Start () {
        startPos = new Vector2(go1.transform.position.x, go1.transform.position.y);
        nextToGoBack = go1;
        theSecondOne = go2;
    }
	
	void Update () {

        go1.transform.Translate(Vector3.right * moveSpeed);
        go2.transform.Translate(Vector3.right * moveSpeed);

        if (theSecondOne.transform.position.x >= startPos.x)
        {
            nextToGoBack.transform.position = new Vector2(startPos.x - theSecondOne.GetComponent<SpriteRenderer>().bounds.size.x, startPos.y);
            nextToGoBack = nextToGoBack == go1 ? go2 : go1;
            theSecondOne = theSecondOne == go1 ? go2 : go1;
        }

	}
}
