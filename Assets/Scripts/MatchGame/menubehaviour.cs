using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menubehaviour : MonoBehaviour {
    public void triggermenubehaviour(int i)
    {
        switch (i)
        {
            default:
            case (0):
                SceneManager.LoadScene("MatchGameLevel");
                break;
            case (1):
                Application.Quit();
                break;
        }
    }
    
}
