using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


/// <summary>
/// Script encargado de buscar si hay otros jugadores en una sala o crea una si no hay
/// </summary>
public class Matchmaker : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject texto;

    private int intervalo = 0;
    private List<RoomInfo> _roomList;
    private bool first_time = true;
    // Start is called before the first frame update
    void Start()
    {
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            //Debug.Log("Sin conexion con el servidor");
        }
        _roomList = new List<RoomInfo>();   
    }

    public override void OnJoinedLobby()
    {
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            //Debug.Log("Sin conexion con el servidor");
            return;
        }
        if (first_time)
        {
            StartCoroutine(Tiempo_De_Espera());
            first_time = false;
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
    }

    public void Buscar_Sala()
    {
        foreach(RoomInfo roomInfo in _roomList)
        {
            //Debug.Log(roomInfo.IsOpen);
            if (roomInfo.IsOpen && roomInfo.PlayerCount == 1)
            {
                PhotonNetwork.JoinRoom(roomInfo.Name);
                PhotonNetwork.AutomaticallySyncScene = true;
                return;
            }
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.EmptyRoomTtl = 1;
        //Si la habitacion ya esta creada te mete en una si no la crea con el nombre y las opciones anteriormente mecionadas
        PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.NickName , options, TypedLobby.Default);
        intervalo = Random.Range(15, 50);
        StartCoroutine(Tiempo_Hasta_Recarga(intervalo));
        //Debug.Log("he creado una sala, tiempo hasta desconexion: " + intervalo);
        texto.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
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

    IEnumerator Tiempo_Hasta_Recarga(int i)
    {
        yield return new WaitForSeconds(i);
        /*if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log(PhotonNetwork.CurrentRoom.IsOpen = false);
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LeaveRoom(false);
            StartCoroutine(Tiempo_De_Espera());
        }*/
        PhotonNetwork.Disconnect();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(2);
    }

}
