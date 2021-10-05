﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PoderColono : Poder
{
    [SerializeField]
    GameObject planeta;
    [SerializeField]
    int planetasFase1 = 1;

    int planetasColocados = 0;

    bool SetPlaneta = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }
    

    public override void InitialAction()
    {
        for (int i = 0; i < 3; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!planeta.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));

            if(PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                base.photonView.RPC("RPC_InstanciarPlaneta", RpcTarget.All, rnd);

            }
            GameObject thisPieza = Instantiate(planeta);
            thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
            Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();
        }
    }


    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = new List<Casilla>();
        planeta.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();

        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);       

        SetPlaneta = true;
    }

    public override void SecondAction() { }  


    
    void CrearPieza()
    {
        if (!SetPlaneta) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza = Instantiate(planeta);
            thisPieza.transform.position = c.transform.position;
            thisPieza.GetComponent<Pieza>().Colocar(c);

           
            SetPlaneta = false;
        }


        if (++planetasColocados < planetasFase1) FirstAction();
    }

    [PunRPC]
    public void RPC_InstanciarPlaneta(int i)
    {
        GameObject thisPieza = Instantiate(planeta);
        thisPieza.transform.position = Tablero.instance.mapa[i].transform.position;
        Tablero.instance.mapa[i].pieza = thisPieza.GetComponent<Pieza>();
    }
}
