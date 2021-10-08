using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


/// <summary>
/// Script encargado de buscar si hay otros jugadores en una sala o crea una si no hay
/// </summary>
public class Matchmaker : MonoBehaviourPunCallbacks
{


    private List<RoomInfo> _roomList;
    // Start is called before the first frame update
    void Start()
    {
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Sin conexion con el servidor");
            return;
        }
        _roomList = new List<RoomInfo>();   
    }

    public override void OnJoinedLobby()
    {
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Sin conexion con el servidor");
            return;
        }
        StartCoroutine(Tiempo_De_Espera());

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        Debug.Log("Actualizo la lista de salas");
    }

    public void Buscar_Sala()
    {

        Debug.Log("Mi lista contiene esto" + _roomList.Count);
        foreach(RoomInfo roomInfo in _roomList)
        {
            if (roomInfo.IsOpen)
            {
                PhotonNetwork.JoinRoom(roomInfo.Name);
                PhotonNetwork.AutomaticallySyncScene = true;
                Debug.Log("me he unido a una sala");
                return;
            }
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        //Si la habitacion ya esta creada te mete en una si no la crea con el nombre y las opciones anteriormente mecionadas
        PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.NickName , options, TypedLobby.Default);
        Debug.Log("he creado una sala");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Debug.Log("un jugador se ha unido a mi sala");
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.LoadLevel(1);
            }
        }
    }

    IEnumerator Tiempo_De_Espera()
    {
        yield return new WaitForSeconds(2);
        Buscar_Sala();
    }

}
