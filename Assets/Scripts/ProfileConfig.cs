using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class ProfileConfig : MonoBehaviour
{

    public Text _name;
    public Text emailString;

    public void Config(Firebase.Auth.FirebaseUser user)
    {
        this.emailString.text = string.Format("Signed in as {0}", user.Email);
    }
}
