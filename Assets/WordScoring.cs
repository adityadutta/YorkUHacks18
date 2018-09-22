using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordScoring : MonoBehaviour {

    public WordManager wordManager;

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 150), "Total: " + wordManager.totalWord.ToString());
        GUI.Label(new Rect(0, 20, 100, 150), "Scored: " + wordManager.wordTyped.ToString());
    }
}
