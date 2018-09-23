using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UberGameManager : MonoBehaviour
{

    public static UberGameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void toGameSelect()
    {
        SceneManager.LoadScene("GameSelect");
    }   

    public void toPatientLogin()
    {
        SceneManager.LoadScene("PatientLogin");
    }

    public void toPatientProfile()
    {
        SceneManager.LoadScene("PatientProfile");
    }

    public void toTypeGameStats()
    {
        SceneManager.LoadScene("TypeGameStats");
    }

    public void toMatchGameStats()
    {
        SceneManager.LoadScene("MatchGameStats");
    }

    public void toPatientSelect()
    {
        SceneManager.LoadScene("DoctorPatients");
    }

    public void toDoctorLogin()
    {
        SceneManager.LoadScene("DoctorLogin");
    }

    public void loadTypeGame()
    {
        SceneManager.LoadScene("TypingGame");
    }

    public void loadMatchGame()
    {
        SceneManager.LoadScene("MatchGameMenu");
    }
}
