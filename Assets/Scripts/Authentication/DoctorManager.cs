using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DoctorManager : MonoBehaviour {

    public Doctor doctor;

    private void Awake()
    {
        Router.Doctors().ValueChanged += ValueChanged;
    }

    private void ValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.Snapshot.Value == null)
        {
            Debug.Log("No data at node");
        }
        else
        {
            Debug.Log("Value Changed");
        }
    }
}
