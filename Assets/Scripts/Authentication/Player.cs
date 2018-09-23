using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

    //class properties
    public string email;
    public string name;
    public string doctorName;

    public Player(string email, string name, string doctorName)
    {
        this.email = email;
        this.name = name;
        this.doctorName = doctorName;
    }

    public Player(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString();
        this.name = dict["name"].ToString();
        this.doctorName = dict["doctorName"].ToString();
    }
}
