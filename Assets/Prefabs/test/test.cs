using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Pequeño script de conectar con el servidor
/// (Carlos.TODO: habria que hacer un script serio con esta funcion)
/// </summary>
public class test : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("connecting to server");
        PhotonNetwork.NickName = PMasterManager.gameSettings.nickName;
        PhotonNetwork.GameVersion = PMasterManager.gameSettings.gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("conected to master");
        print(PhotonNetwork.LocalPlayer.NickName);

        PhotonNetwork.JoinLobby();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        print("disconected form server for reason " + cause.ToString());
    }

}
