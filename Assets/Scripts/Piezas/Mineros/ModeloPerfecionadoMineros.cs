using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ModeloPerfecionadoMineros : EfectoEspecial
{
    public GameObject pieza_a_mejorar;

    public override void Accion()
    {
        if (!pieza_a_mejorar)
        {
            Debug.LogError("Fallo en la adquisicion de pieza a mejorar del modelo perfeccionado");
            return;
        }
        
        if (PhotonNetwork.InRoom)
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Instantiate(pieza_a_mejorar.name, casilla.transform.position, Quaternion.identity);
            }
                
        }
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        return FiltroCasillas.CasillasLibres(referencia);
    }
}
