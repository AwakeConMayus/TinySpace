using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Poder : MonoBehaviourPunCallbacks
{
    public int jugador;
    public abstract void InitialAction(bool sin_pasar_turno = false);
    public abstract void FirstAction();
    public abstract void SecondAction();

    protected Opciones padre;

    [PunRPC]
    public void RPC_Move_FromC_ToC2(int i, int j, bool extra)
    {
        Casilla c = Tablero.instance.mapa[i];
        if (extra) c.pieza.Set_Pieza_Extra(true);
        c.pieza.transform.position = Tablero.instance.mapa[j].transform.position;
        c.pieza.Colocar(Tablero.instance.mapa[j]);
    }

    public void SetPadre(Opciones o)
    {
        padre = o;
    }
}
