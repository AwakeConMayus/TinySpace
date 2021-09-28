using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

/// <summary>
/// Etiqueta con el nombre y numero de jugadores de una habitacion
/// </summary>
public class RoomListing : MonoBehaviour
{

    [SerializeField] Text _text;

    public RoomInfo roomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        _text.text = _roomInfo.MaxPlayers + " , " + _roomInfo.Name; 
    }
}
