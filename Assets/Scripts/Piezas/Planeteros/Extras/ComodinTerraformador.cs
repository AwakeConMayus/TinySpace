using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ComodinTerraformador : EfectoEspecial
{
    public override void Accion()
    {
        if(PhotonNetwork.InRoom)
        {
            if(GetComponent<PhotonView>().IsMine) MenuTerraformador.instance.Convocar(casilla);
        }
        else
        {
            MenuTerraformador.instance.Convocar(casilla);
        }

    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillas = FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        casillas = FiltroCasillas.CasillasDeUnTipo(Clase.planeta, casillas);

        return casillas;
    }
}
