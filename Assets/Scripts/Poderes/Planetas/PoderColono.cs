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
    }

    public override void SecondAction() 
    {
        print("secondAction");
        planetaSagrado = Resources.Load<GameObject>("Planeta Sagrado Planetarios");
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
                base.photonView.RPC("RPC_InstanciarPlanetaSagrado", RpcTarget.All, Tablero.instance.Get_Numero_Casilla(c.gameObject));

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
    public void RPC_InstanciarPlanetaSagrado(int i)
    {
        Casilla c = Tablero.instance.mapa[i];
        c.SetState(States.holy);
        c.Clear();
        if (!planetaSagrado) planetaSagrado = Resources.Load<GameObject>("Planeta Sagrado Planetarios");
        GameObject thisPieza = Instantiate(planetaSagrado);
        thisPieza.transform.position = Tablero.instance.mapa[i].transform.position;
        thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
        thisPieza.GetComponent<Pieza>().casilla = c;
        Tablero.instance.mapa[i].pieza = thisPieza.GetComponent<Pieza>();
    }
}
