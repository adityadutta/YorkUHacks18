using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Firebase.Database;

public class FormManager : MonoBehaviour
{

    //UI objects linked from the inspector
    public InputField _name;
    public InputField emailInput;
    public InputField passwordInput;
    public InputField emailInputLogin;
    public InputField passwordInputLogin;

    public Button signUpButton;
    public Button loginButton;

    public GameObject registerPanel;
    public GameObject loginPanel;

    public Text statusText;

    public AuthManager authManager;

    private void Awake()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
        ToggleButtonStates(false);

        //Auth Delegate subscriptions
        authManager.authCallBack += HandleAuthCallBack;
    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/userCredentials.dat"))
        {
            GoToLogin();
            LoadCredentials();
            OnLogin();
        }
    }

    //Validates email input
    public void ValidateEmail()
    {
        string email = emailInput.text;
        string emailLogin = emailInputLogin.text;

        var regexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        if (email != " " && Regex.IsMatch(email, regexPattern) || emailLogin != " " && Regex.IsMatch(emailLogin, regexPattern))
            ToggleButtonStates(true);
        else
            ToggleButtonStates(false);
    }

    public void OnSignIn()
    {
        authManager.SignUpNewUser(emailInput.text, passwordInput.text);
        UpdateStatus("\nWelcome to AlzWorld " + _name.text);
        Debug.Log("Sign In");
    }

    public void OnLogin()
    {
        authManager.LoginExistingUser(emailInputLogin.text, passwordInputLogin.text);
        Router.Players().ValueChanged += ValueChanged;
        UpdateStatus("Loading");
        Debug.Log("Login");
    }

    public void OnSignInDoctor()
    {
        authManager.SignUpNewDoctor(emailInput.text, passwordInput.text);
        UpdateStatus("\nWelcome to AlzWorld " + _name.text);
        Debug.Log("Sign In");
    }

    public void OnLoginDoctor()
    {
        authManager.LoginExistingDoctor(emailInputLogin.text, passwordInputLogin.text);
        Router.Doctors().ValueChanged += ValueChanged;
        UpdateStatus("Loading");
        Debug.Log("Login");
    }

    private void ValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.Snapshot.Value == null)
        {
            Debug.Log("No data at node");
        }
        //else
        //{
        //    if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();
        //        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        //        PlantInfo pi = (PlantInfo)bf.Deserialize(file);
        //        file.Close();
        //        plantName = pi.plantName;
        //    }
        //    Debug.Log("Value Changed");
        //}
    }

    IEnumerator HandleAuthCallBack(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            UpdateStatus("Sorry, there was an error creating your new account. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "sign_up")
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;
                Debug.LogFormat("Welcome to AlzWorld {0}", newPlayer.Email);

                Player player = new Player(newPlayer.Email);
                DatabaseManager.sharedInstance.CreateNewPlayer(player, newPlayer.UserId);

                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("GameSelect");
            }
            if (operation == "sign_up_doctor")
            {
                Firebase.Auth.FirebaseUser newDoctor = task.Result;
                Debug.LogFormat("Welcome to AlzWorld {0}", newDoctor.Email);

                Doctor doctor = new Doctor(newDoctor.Email);
                DatabaseManager.sharedInstance.CreateNewDoctor(doctor, newDoctor.UserId);

                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("DoctorPatients");
            }

        }
    }

    private void OnDestroy()
    {
        authManager.authCallBack -= HandleAuthCallBack;
    }

    //Utilities
    private void ToggleButtonStates(bool toState)
    {
        signUpButton.interactable = toState;
        loginButton.interactable = toState;
    }

    public void RememberLogin()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/userCredentials.dat");

        UserCredentials user = new UserCredentials();
        //user.email = emailInput.text;
        //user.password = passwordInput.text;
        user.emailLogin = emailInputLogin.text;
        user.passwordLogin = passwordInputLogin.text;

        bf.Serialize(file, user);
        file.Close();

    }

    public void LoadCredentials()
    {
        if (File.Exists(Application.persistentDataPath + "/userCredentials.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userCredentials.dat", FileMode.Open);
            UserCredentials user = (UserCredentials)bf.Deserialize(file);
            file.Close();

            emailInputLogin.text = user.emailLogin;
            passwordInputLogin.text = user.passwordLogin;
        }
    }

    public void GoToLogin()
    {
        registerPanel.SetActive(!registerPanel.activeSelf);
        loginPanel.SetActive(!loginPanel.activeSelf);
    }

    public void LoginBack()
    {
        registerPanel.SetActive(!registerPanel.activeSelf);
        loginPanel.SetActive(!loginPanel.activeSelf);
    }

    private void UpdateStatus(string message)
    {
        statusText.text = message;
    }
}

[Serializable]
class UserCredentials
{
    // public string email;
    //public string password;
    public string emailLogin;
    public string passwordLogin;
}
