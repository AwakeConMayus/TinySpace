using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class Arbitro : MonoBehaviourPunCallbacks
{

    //AVISO: El gameobject en el que vaya este script tiene que tener un photon view o dara error
    public List<Opciones> opciones;
    public Opciones player;
    bool active;
    bool specialActive;

    int numeroPoder = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    bool inputActive = true;

    [SerializeField]
    MenuFinalParitda mi_menu;

    [SerializeField]
    TuSeleccion mi_seleccion;
    [SerializeField]
    TuSeleccion seleccion_del_rival;
    [SerializeField]
    EspejoMaestro espejo_Maestro;
    private void Start()
    {
        EventManager.StartListening("AccionTerminadaConjunta", NextTurnDoble);

        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
        {
            SetInitial();
        }
    }

    public void SetInitial()
    {
        active = specialActive = true;
        switch (mi_seleccion.faccion)
        {
            case Faccion.minero:
                player = opciones[0];              
                base.photonView.RPC("RPC_SetNotInitial", RpcTarget.Others);
                break;
            case Faccion.oyente:
                player = opciones[1];
                base.photonView.RPC("RPC_SetNotInitial", RpcTarget.Others);
                break;
        }
        InstancePiezas.instance.faccion = mi_seleccion.faccion;
        player.gameObject.SetActive(true);
        player.opcionesIniciales = mi_seleccion.mis_opciones;
        player.poder = mi_seleccion.mi_poder;
        player.mi_reflejo = espejo_Maestro.Activar(mi_seleccion.faccion) ;
        player.mi_reflejo.opcionesIniciales = mi_seleccion.mis_opciones;
        player.GetComponentInChildren<InterfazTurnos>().primero = true;
        player.PrepararPreparacion();
    }

    [PunRPC]
    public void RPC_SetNotInitial()
    {
        active = specialActive = false;
        switch (mi_seleccion.faccion)
        {
            case Faccion.minero:
                player = opciones[0];
                break;
            case Faccion.oyente:
                player = opciones[1];
                break;
        }
        InstancePiezas.instance.faccion = mi_seleccion.faccion;
        player.gameObject.SetActive(true);
        player.opcionesIniciales = mi_seleccion.mis_opciones;
        player.poder = mi_seleccion.mi_poder;
        player.mi_reflejo = espejo_Maestro.Activar(mi_seleccion.faccion); 
        player.mi_reflejo.opcionesIniciales = seleccion_del_rival.mis_opciones;
        player.GetComponentInChildren<InterfazTurnos>().primero = false;
        player.PrepararPreparacion();
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public void NextTurnDoble()
    {
        Debug.Log("paso turno");
        base.photonView.RPC("RPC_NextTurn", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_NextTurn()
    {
        EventManager.TriggerEvent("Siguiente_turno");
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void SpecialTurn()
    {
       SetActiveActive(false);

        bool estaVezToca = specialActive;

        

        if (specialTurno % 2 != 0)
        {
            specialPhase = false;
        }
        if (++specialTurno % 2 != 0)
        {
            specialActive = !specialActive;
        }

        if (estaVezToca) 
        {
            switch (numeroPoder)
            {
                case 0:
                    player.poder.GetComponent<Poder>().InitialAction();
                    break;
                case 1:
                    player.poder.GetComponent<Poder>().FirstAction();
                    break;
                case 2:
                    player.poder.GetComponent<Poder>().SecondAction();
                    break;
                default:
                    Debug.Log("el turno de poder no esta de acorde");
                    break;
            }
            ++numeroPoder; 
        }
    }

    void Turn()
    {
        if (turno == 20)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            EndGame();
        }

        if (!active)
        {
            SetActiveActive(false);
        }
        else if (active)
        {
            SetActiveActive(true);
        }


        if ((turno+1) % 10 == 0)
        {
            specialPhase = true;
        }
        if (++turno % 2 != 0)
        {
            active = !active;
        }
    }

    void SetActiveActive(bool b)
    {
        inputActive = b;
        player.SetActive(b);
    }

    void EndGame()
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
            SendToGoogle.instance.SendOnline();

        mi_seleccion.mi_poder = null;
        mi_seleccion.mis_opciones = new GameObject[5];
        mi_seleccion.faccion = Faccion.none;

        mi_menu.gameObject.SetActive(true);
        mi_menu.Final_Partida(Tablero.instance.RecuentoPuntos());

        Debug.Log("NO HAY FINAL, EMPIEZAN LOS ERRORES :)");
    }
}
