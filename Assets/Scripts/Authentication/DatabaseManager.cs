using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class DatabaseManager : MonoBehaviour {

    public static DatabaseManager sharedInstance = null;
    public Player player;
    public Doctor doctor;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if(sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yorkuhacks18.firebaseio.com/");

        Debug.Log(Router.Players());

        Debug.Log(Router.Doctors());

        //GetPlayers(result =>
        //{
        //    Debug.Log("GET PLAYERs");
        //    foreach (Player player in result)
        //    {
        //        Debug.Log(player.name);
        //    }
        //});

    }

    public void CreateNewPlayer(Player player, string uid)
    {
        string playerJSON = JsonUtility.ToJson(player);
        Router.PlayerWithUID(uid).SetRawJsonValueAsync(playerJSON);
    }

    public void GetPlayers(Action<List<Player>> completionBlock)
    {
        List<Player> tmpList = new List<Player>();

        Router.Players().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot players = task.Result;

            foreach (DataSnapshot playerNode in players.Children)
            {

                var playerDict = (IDictionary<string, object>)playerNode.Value;
                Player newPlayer = new Player(playerDict);
                tmpList.Add(newPlayer);
            }

            completionBlock(tmpList);
        });
    }


    public void CreateNewDoctor(Doctor doctor, string uid)
    {
        string doctorJSON = JsonUtility.ToJson(doctor);
        Router.DoctorWithUID(uid).SetRawJsonValueAsync(doctorJSON);
    }

    public void GetDoctors(Action<List<Doctor>> completionBlock)
    {

        List<Doctor> tmpList = new List<Doctor>();

        Router.Players().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot doctors = task.Result;

            foreach (DataSnapshot doctorNode in doctors.Children)
            {

                var doctorDict = (IDictionary<string, object>)doctorNode.Value;
                Doctor newDoctor = new Doctor(doctorDict);
                tmpList.Add(newDoctor);
            }

            completionBlock(tmpList);
        });
    }
}
