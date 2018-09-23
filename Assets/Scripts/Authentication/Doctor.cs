using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor{

    //class properties
    public string email;
    public string name;

    public Doctor(string email, string name)
    {
        this.email = email;
        this.name = name;
    }

    public Doctor(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString();
        this.name = dict["name"].ToString();
    }
}
