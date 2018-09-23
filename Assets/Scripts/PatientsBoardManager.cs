using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class PatientsBoardManager : MonoBehaviour {

    public List<Player> playerList = new List<Player>();

    public GameObject rowPrefab;
    public GameObject scrollContainer;
    public GameObject profilePanel;

    Firebase.Auth.FirebaseAuth auth;

    private void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        playerList.Clear();

        DatabaseManager.sharedInstance.GetPlayers(result =>
        {
            playerList = result;

            InitialiseUI();
        });

        profilePanel.GetComponent<ProfileConfig>().Config(auth.CurrentUser);
        //Router.Players().ChildAdded += NewPlayerAdded;
        //Router.Players().OrderByChild("doctorName");
    }

    void InitialiseUI()
    {
        foreach (Player player in playerList)
        {
            //Debug.Log(player.name);
            CreateRow(player);
        }
    }

    void CreateRow(Player player)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<RowConfig>().Initialise(player);
        newRow.transform.SetParent(scrollContainer.transform, false);
    }

    void NewPlayerAdded(object sender, ChildChangedEventArgs args)
    {
        if(args.Snapshot.Value == null)
        {
            Debug.Log("No data at node");
        }
        else
        {
            Debug.Log(args.Snapshot.Value);
        }
    }
}
