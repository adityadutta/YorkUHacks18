using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor{

    //class properties
    public string email;

    public Doctor(string _email)
    {
        email = _email;
    }

    public Doctor(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString();
    }
}
