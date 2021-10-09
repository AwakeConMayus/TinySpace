using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderPlanetas : Poder
{
    GameObject planeta;

    bool SetPlaneta = false;

    

    public override void InitialAction()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
        planeta = Resources.Load<GameObject>("Planeta Planetarios");

        for (int i = 0; i < 3; i++)
        {
            List<Casilla> casillasPosibles = FiltroCasillas.CasillasSinMeteorito(planeta.GetComponent<Pieza>().CasillasDisponibles());
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!casillasPosibles.Contains(Tablero.instance.mapa[rnd]));

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                base.photonView.RPC("RPC_InstanciarPlaneta", RpcTarget.All, rnd, jugador);

            }
            else
            {
                GameObject thisPieza = Instantiate(planeta);
                thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
                Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();
            }
        }
        ColorearCasillas.instance.initialColor();
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = new List<Casilla>();
        planeta.GetComponent<Pieza>().Set_Jugador(jugador);
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();

        //ColorearCasillas.instance.initialColor();
        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);

        SetPlaneta = true;
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
                base.photonView.RPC("RPC_InstanciarPlaneta", RpcTarget.All, Tablero.instance.Get_Numero_Casilla(c.gameObject), jugador);

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

        FirstActionPersonal();
    }

    public abstract void FirstActionPersonal();

    [PunRPC]
    public void RPC_InstanciarPlaneta(int i, int _jugador)
    {
        if(!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
        GameObject thisPieza = Instantiate(planeta);
        thisPieza.transform.position = Tablero.instance.mapa[i].transform.position;
        thisPieza.GetComponent<Pieza>().Set_Jugador(_jugador);
        thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
        Tablero.instance.mapa[i].pieza = thisPieza.GetComponent<Pieza>();
    }
}
