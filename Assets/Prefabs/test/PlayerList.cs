using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class PlayerList : MonoBehaviourPunCallbacks
{

    //Prefab etiqueta de habitacion
    [SerializeField]
    PlayerListing playerListPrefab;

    //GameObject padre de las etiquetas
    [SerializeField]
    Transform content;

    //Lista de las habitaciones en activo
    private List<PlayerListing> _playerList = new List<PlayerListing>();

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
       
         PlayerListing listing =  Instantiate(playerListPrefab, content);

        if(listing != null)
        {
            listing.AddName(newPlayer.NickName);
            _playerList.Add(listing);
        }

        

    }

    public override void OnJoinedRoom()
    {
        foreach(KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerListing listing = Instantiate(playerListPrefab, content);

            if (listing != null)
            {
                listing.AddName(playerInfo.Value.NickName);
                _playerList.Add(listing);
            }
        }
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
       foreach(PlayerListing l in _playerList)
        {
            if( l.GetName() == otherPlayer.NickName)
            {
                _playerList.Remove(l);
                Destroy(l);
            }
        }
    }

  
}
