using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text timerText;
    private float startTime;
    public bool isRunning;

	void Start () {
        startTime = Time.time;
        isRunning=true;
    }

    // Update is called once per frame
    void Update() {
        if (isRunning) { 
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("00");

        timerText.text = minutes + ":" + seconds;
    }
	}
}
