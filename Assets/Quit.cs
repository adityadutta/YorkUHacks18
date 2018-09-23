using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {

    //Go back to menu
    public void quit()
    {
        Debug.Log("Player Quit");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
