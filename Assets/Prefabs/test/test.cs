using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("connecting to server");
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("conected to master");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        print("disconected form server for reason " + cause.ToString());
    }
}
