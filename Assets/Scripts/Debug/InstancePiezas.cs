using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public enum estados
{
    SelectPieza,
    SelectCasilla    
}

public class InstancePiezas : MonoBehaviourPunCallbacks
{
    public static InstancePiezas instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
    //AVISO IMPORTANTE: el objeto en el que vaya este script debe tener un componente photonView

    [HideInInspector]
    public GameObject pieza;
    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;
    [HideInInspector]
    public int jugadorEnemigo;
    //* Para el tutorial, mineros 0, planetas 1
    [HideInInspector]
    public GameObject planeta;

    estados estado = estados.SelectPieza;

    List<Casilla> casillasPosibles = new List<Casilla>();

    

    public void SetJugador(int player)
    {
        jugador = player;
        Tablero.instance.ResetCasillasEfects();
    }

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }


    public void SetPieza(GameObject nave)
    {
        pieza = nave;
        casillasPosibles = nave.GetComponent<Pieza>().CasillasDisponibles();

        //* Pinta de verde las casillas sobre las que se puede posicionar una pieza
        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);



        estado = estados.SelectCasilla;
    }

    public void CrearPieza()
    {

        Casilla c = ClickCasillas.casillaClick;
        if (estado == estados.SelectPieza || c == null) return; 

        if (casillasPosibles.Contains(c))
        {

            // Comprobacion de si el game se esta realizando online u offline
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                // Instanciacion que utiliza photon
                PhotonNetwork.Instantiate(pieza.name, c.transform.position, Quaternion.identity);
                
            }
            else
            {
                // Instanciacion de piezas en el offline
                GameObject thisPieza = Instantiate(pieza);
                thisPieza.transform.position = c.transform.position;
                //GestorTurnos.instance.realizarJugada();
            }

    

            EventManager.TriggerEvent("ColocarPieza");
            estado = estados.SelectPieza;

            Tablero.instance.ResetCasillasEfects();
        }
        
        
    }

    public void RecuentoPuntosTest()
    {
        SendToGoogle.instance.SendOnline();
        int[] puntuaciones = Tablero.instance.RecuentoPuntos();
        
        Debug.Log("JugadorInicial: " + puntuaciones[0] + " / " + puntuaciones[1]);
    }

    public void SetInicialTable()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}
