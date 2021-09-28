using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{

    [SerializeField] Text _playerName;

    public void AddName(string _name)
    {
        if (_name == null) return;
        _playerName.text = _name;
    }

    public string GetName()
    {
        return _playerName.text;
    }
}
