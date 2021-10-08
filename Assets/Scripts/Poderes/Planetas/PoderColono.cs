using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PoderColono : PoderPlanetas
{
    [SerializeField]
    int planetasFase1 = 1;

    int planetasColocados = 0;

    bool SetPlaneta = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }


    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = new List<Casilla>();
        planeta.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();

        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);       

        SetPlaneta = true;
        EventManager.TriggerEvent("AccionTerminada");
    }

    public override void SecondAction() 
    {
        EventManager.TriggerEvent("AccionTerminada");
    }  


    
    void CrearPieza()
    {
        if (!SetPlaneta) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                base.photonView.RPC("RPC_InstanciarPlaneta", RpcTarget.All, Tablero.instance.Get_Numero_Casilla(c.gameObject));

            }
            else
            {
                GameObject thisPieza = Instantiate(planeta);
                thisPieza.transform.position = c.transform.position;
                thisPieza.GetComponent<Pieza>().Colocar(c);
            }

           
            SetPlaneta = false;
        }

        ColorearCasillas.instance.initialColor();

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
