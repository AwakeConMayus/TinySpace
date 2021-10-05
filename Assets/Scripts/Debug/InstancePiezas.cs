﻿using System.Collections;
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

    //AVISO IMPORTANTE: el objeto en el que vaya este script debe tener un componente photonView

    [HideInInspector]
    public GameObject pieza;
    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;
    //* Para el tutorial, mineros 0, planetas 1
    [HideInInspector]
    public GameObject planeta;

    estados estado = estados.SelectPieza;

    List<Casilla> casillasPosibles = new List<Casilla>();

    public void SetJugador(int player)
    //// esta función debería estar deprecated puesto que setPieza obtiene el player del Gestor de turnos
    //// la dejo de momento porque puede ser util usando un .setPlayer en el Gestor y porque es llamada por
    //// los botones de select facción (que también están deprecated y ocultos en la UI)
    {
        jugador = player;
        ColorearCasillas.instance.initialColor();
    }

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }


    public void SetPieza(GameObject nave)
    {
        pieza = nave;
        pieza.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = nave.GetComponent<Pieza>().CasillasDisponibles();

        //* Pinta de verde las casillas sobre las que se puede posicionar una pieza
        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);
        
        //* Pinta de rojo las casillas sobre las que ha posicionado una pieza el rival
        //foreach (Casilla casilla in Tablero.instance.mapa) 
        //{
        //    if (casilla.pieza && casilla.pieza.jugador != jugador) coloreador.reColor("red", casilla);
        //}

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
                // Callback de paso de turno en los dos clientes
                base.photonView.RPC("RPC_realizarJugada", RpcTarget.All);
            }
            else
            {
                // Instanciacion de piezas en el offline
                GameObject thisPieza = Instantiate(pieza);
                thisPieza.transform.position = c.transform.position;
                GestorTurnos.instance.realizarJugada();
            }

            //foreach (Casilla casilla in Tablero.instance.mapa)
            //{
            //    if (casilla.pieza && casilla.pieza.jugador != jugador) ColorearCasillas.instance.reColor("red", casilla);
            //    else ColorearCasillas.instance.initialColor(casilla);
            //}

            
            estado = estados.SelectPieza;

            ColorearCasillas.instance.initialColor();
        }
        
        //foreach(Casilla casilla in Tablero.instance.mapa)
        //if (c.pieza && c.pieza.jugador == jugador || !c.pieza) ColorearCasillas.instance.initialColor(casilla);
    }

    public void RecuentoPuntosTest()
    {
        int[] puntuaciones = new int[2];

        foreach(Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                puntuaciones[c.pieza.jugador] += c.pieza.Puntos();
            }
        }

        print("Mineros: " + puntuaciones[0] + " / Planetas: " + puntuaciones[1]);
    }

    public void SetInicialTable()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}
