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
    [SerializeField] TuSeleccion mi_Seleccion;
    [SerializeField] GameObject texto;

    private int intervalo = 0;
    private List<RoomInfo> _roomList;
    private bool first_time = true;
    // Start is called before the first frame update
    void Start()
    {
        _roomList = new List<RoomInfo>();   
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Sin conexion con el servidor");
        }
        else
        {

            if (PhotonNetwork.InRoom) PhotonNetwork.Disconnect();
            if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
            else { Buscar_Sala(); }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        //Caso limite de sin conexion al servidor
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Sin conexion con el servidor");
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
        Debug.Log(_roomList.Count);
        foreach (RoomInfo roomInfo in _roomList)
        {
            
            Debug.Log(roomInfo.IsOpen  + "   " + roomInfo.PlayerCount);
            if (roomInfo.IsOpen && roomInfo.PlayerCount == 1)
            {
                string[] name_info = roomInfo.Name.Split(',');
                Debug.Log("gameversion de la sala: " + name_info[1]);
                Debug.Log("gameversion mio: " + PMasterManager.gameSettings.gameVersion);
                Debug.Log("La faccion de mi rival es: " + name_info[2]);
                if (name_info[1] == PMasterManager.gameSettings.gameVersion && (name_info[2] != mi_Seleccion.faccion.ToString() || name_info[2] == Faccion.none.ToString()))
                {
                    PhotonNetwork.JoinRoom(roomInfo.Name);
                    PhotonNetwork.AutomaticallySyncScene = true;
                    return;
                }
            }
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.EmptyRoomTtl = 1;
        string name = PhotonNetwork.NickName + "," + PMasterManager.gameSettings.gameVersion + "," + mi_Seleccion.faccion;
        Debug.Log(name);
        //Si la habitacion ya esta creada te mete en una si no la crea con el nombre y las opciones anteriormente mecionadas
        PhotonNetwork.JoinOrCreateRoom(name , options, TypedLobby.Default);
        intervalo = Random.Range(10, 30); // fuck u lantaron, 2c was here; clanta:one day i will crush ya bitch; 
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
                PhotonNetwork.LoadLevel(2);  // Esto tendrá que ser LoadLevel(2) cuando funcione la selección de unidades de facción
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("entro");
        Debug.Log("numero de jugadores en sala: " + PhotonNetwork.CurrentRoom.PlayerCount);
        intervalo = Random.Range(10, 30);
        StartCoroutine(Tiempo_Hasta_Recarga(intervalo));
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("fallo en entrar ha habitacion");
        intervalo = Random.Range(10, 30);
        StartCoroutine(Tiempo_Hasta_Recarga(intervalo));
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
        SceneManager.LoadScene(1);
    }

}
