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
        //    player = result;
        //    Debug.Log(player);

        //});
    }

    public void CreateNewPlayer(Player player, string uid)
    {
        string playerJSON = JsonUtility.ToJson(player);
        Router.PlayerWithUID(uid).SetRawJsonValueAsync(playerJSON);
    }

    public void GetPlayers(Action<Player> completionBlock)
    {
        //List<Player> tmpList = new List<Player>();

        Router.Players().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot _player = task.Result;

            var playerDict = (IDictionary<string, object>)_player.Value;
            Player newPlayer = new Player(playerDict);

            completionBlock(newPlayer);
        });
    }

    public void CreateNewDoctor(Doctor doctor, string uid)
    {
        string doctorJSON = JsonUtility.ToJson(doctor);
        Router.DoctorWithUID(uid).SetRawJsonValueAsync(doctorJSON);
    }

    public void GetDoctors(Action<Doctor> completionBlock)
    {

        Router.Doctors().GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot _doctor = task.Result;

            var doctorDict = (IDictionary<string, object>)_doctor.Value;
            Doctor newDoctor = new Doctor(doctorDict);

            completionBlock(newDoctor);
        });
    }
}
