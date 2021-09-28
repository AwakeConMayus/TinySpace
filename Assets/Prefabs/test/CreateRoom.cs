using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


/// <summary>
/// Script de UI para la creacion de habitaciones
/// </summary>
public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] Text _roomName;


    public void OnClick_CreateRoom()
    {
        
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Sin conexion con el servidor");
            return;
        }
        //Caso limite de sin input en el nombre
        if(_roomName.text == "")
        {
            Debug.Log("No se ha introducido nombre");
            return;
        }
        //Creacion de las rooms options
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        //Si la habitacion ya esta creada te mete en una si no la crea con el nombre y las opciones anteriormente mecionadas
        PhotonNetwork.JoinOrCreateRoom( _roomName.text, options, TypedLobby.Default);

        CanvasManager.instance.Hide_CreateRoomCanvas(_roomName.text);

    }

    //Info del intento de creación
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room succesfully");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed " + message);
    }
}
