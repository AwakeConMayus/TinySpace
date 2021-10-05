using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public static class  Auxiliares 
{
    
    public static void Move_FromC_ToC(int i, int j)
    {
        Casilla c = Tablero.instance.Get_Casilla_By_Numero(i);
        c.pieza.transform.position = Tablero.instance.Get_Casilla_By_Numero(j).transform.position;
    }

    [PunRPC]
    public static void RPC_Move_FromC_ToC(int i, int j)
    {
        Casilla c = Tablero.instance.Get_Casilla_By_Numero(i);
        c.pieza.transform.position = Tablero.instance.Get_Casilla_By_Numero(j).transform.position;
    }

}
