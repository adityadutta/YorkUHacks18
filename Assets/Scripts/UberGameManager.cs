using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UberGameManager : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void toPatientLogin()
    {
        SceneManager.LoadScene("PatientLogin");
    }

    public void toDoctorLogin()
    {
        SceneManager.LoadScene("DoctorLogin");
    }
}
