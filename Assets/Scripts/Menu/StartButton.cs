using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {


    public int playersNumber = 1;

    void OnMouseDown()
    {
        SceneManager.LoadScene("OnePlayerGame", LoadSceneMode.Single);
    }
}
