using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Poder : MonoBehaviourPunCallbacks
{
    public int jugador;
    public abstract void InitialAction();
    public abstract void FirstAction();
    public abstract void SecondAction();

    protected Opciones padre;

    [PunRPC]
    public void RPC_Move_FromC_ToC2(int i, int j)
    {
        Casilla c = Tablero.instance.mapa[i];
        c.pieza.transform.position = Tablero.instance.mapa[j].transform.position;
    }

    public void SetPadre(Opciones o)
    {
        padre = o;
    }
}
