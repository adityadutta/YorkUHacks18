using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

    //class properties
    public string email;

    public Player(string _email)
    {
        email = _email;
    }

    public Player(IDictionary<string, object> dict)
    {
        this.email = dict["email"].ToString(); 
    }
}
