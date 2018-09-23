using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

    public Text _name;

    public void Initialise(Player player)
    {
        _name.text = player.name.ToString();
    }
}
