using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PoderColono : PoderPlanetas
{
    GameObject planetaSagrado;

    bool SetPlanetaSagrado = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", CrearPiezaCrearPlanetaSagrado);
    }


    public override void FirstActionPersonal()
    {
        if (padre)
        {
            padre.SeleccionForzada(0);
        }
        else
        {
            Debug.Log("No disponible en modo Debug :(");
        }
    }

    public override void SecondAction() 
    {
        print("secondAction");
        planetaSagrado = Resources.Load<GameObject>("Planeta Sagrado Planetarios");
        planetaSagrado.GetComponent<Pieza>().Set_Jugador(jugador);
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planetaSagrado.GetComponent<Pieza>().CasillasDisponibles();
        print(casillasPosibles.Count);

     
        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        SetPlanetaSagrado = true;

    }  


    
    void CrearPiezaCrearPlanetaSagrado()
    {
        if (!SetPlanetaSagrado) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planetaSagrado.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                base.photonView.RPC("RPC_InstanciarPlanetaSagrado", RpcTarget.All, Tablero.instance.Get_Numero_Casilla(c.gameObject), jugador);

            }
            else
            {
                c.SetState(States.holy);
                c.Clear();
                GameObject thisPieza = Instantiate(planetaSagrado);
                thisPieza.transform.position = c.transform.position;
                thisPieza.GetComponent<Pieza>().Colocar(c);
            }


            SetPlanetaSagrado = false;
            Tablero.instance.ResetCasillasEfects();

            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }

    }

    [PunRPC]
    public void RPC_InstanciarPlanetaSagrado(int i, int _jugador)
    {
        Casilla c = Tablero.instance.Get_Casilla_By_Numero(i);
        c.SetState(States.holy);
        c.Clear();
        if (!planetaSagrado) planetaSagrado = Resources.Load<GameObject>("Planeta Sagrado Planetarios");
        GameObject thisPieza = Instantiate(planetaSagrado);
        thisPieza.transform.position = Tablero.instance.mapa[i].transform.position;
        thisPieza.GetComponent<Pieza>().Set_Jugador(_jugador) ;
        thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
        thisPieza.GetComponent<Pieza>().casilla = c;
        Tablero.instance.mapa[i].pieza = thisPieza.GetComponent<Pieza>();
    }
}
