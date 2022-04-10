using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderChantajista2 : PoderMineros
{
    List<Casilla> casillasDisponibles = new List<Casilla>();

    bool faseSeleccion1 = false, faseSeleccion2 = false;

    GameObject Pieza1, Pieza2;

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", Seleccion1);
        EventManager.StartListening("ClickCasilla", Seleccion2);
    }

    public override void FirstAction()
    {
        casillasDisponibles = FiltroCasillas.CasillasDeOtroJugador(faccion);
        casillasDisponibles = FiltroCasillas.CasillasNoAstro(casillasDisponibles);
        if(casillasDisponibles.Count == 0)
        {
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            Debug.Log("termino al no poder mover  con el cahntajkkista");
            return;
        }
        foreach(Casilla c in casillasDisponibles)
        {
            c.SetState(States.select);
        }

        faseSeleccion1 = true;
    }

    public override void SecondAction()
    {
        FirstAction();
    }

    void Seleccion1()
    {
        if (!faseSeleccion1) return;

        Casilla elegida = ClickCasillas.casillaClick;
        if (!casillasDisponibles.Contains(elegida)) return;

        faseSeleccion1 = false;
        Pieza1 = elegida.pieza.gameObject;
        Tablero.instance.ResetCasillasEfects();

        casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion);
        foreach (Casilla c in casillasDisponibles)
        {
            c.SetState(States.select);
        }

        faseSeleccion2 = true;
    }

    void Seleccion2()
    {
        if (!faseSeleccion2) return;

        Casilla elegida = ClickCasillas.casillaClick;
        if (!casillasDisponibles.Contains(elegida)) return;

        faseSeleccion2 = false;
        Pieza2 = elegida.pieza.gameObject;

        Intercambio();
        Debug.Log("chantajista mueve termina turno");
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }


    void Intercambio()
    {
        Vector3 aux = Pieza1.transform.position;
        if (!PhotonNetwork.InRoom)
        {
            Pieza1.GetComponent<Pieza>().casilla.SetState(States.tpOut);
            Pieza2.GetComponent<Pieza>().casilla.SetState(States.tpOut);
            Pieza1.GetComponent<Pieza>().Set_Pieza_Extra();
            Pieza2.GetComponent<Pieza>().Set_Pieza_Extra();
            Pieza1.transform.position = Pieza2.transform.position;
            Pieza2.transform.position = aux;
        }
        else if(PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            int i = Tablero.instance.Get_Numero_Casilla(Pieza1.GetComponent<Pieza>().casilla.gameObject);
            int j = Tablero.instance.Get_Numero_Casilla(Pieza2.GetComponent<Pieza>().casilla.gameObject);

            base.photonView.RPC("RPC_Move_FromC_ToC2", RpcTarget.Others, i, j, true);
            base.photonView.RPC("RPC_TPEfects2", RpcTarget.All, i, j);
            Pieza2.GetComponent<Pieza>().Set_Pieza_Extra();
            Pieza2.transform.position = aux;
        }
        Tablero.instance.ResetCasillasEfects(); //No se si esto hace falta lo pongo por si acaso
    }

    [PunRPC]
    public void RPC_TPEfects2(int origen, int destino)
    {
        Casilla cOrigen = Tablero.instance.mapa[origen];
        Casilla cDestino = Tablero.instance.mapa[destino];
        cOrigen.SetState(States.tpOut);
        cDestino.SetState(States.tpIn);
    }
}
