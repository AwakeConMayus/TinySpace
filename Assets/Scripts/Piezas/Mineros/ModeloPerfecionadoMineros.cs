using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ModeloPerfecionadoMineros : EfectoEspecial
{
    public override void Accion()
    {
        GameObject piezaMejorada = null;

        switch (casilla.pieza.clase)
        {
            case Clase.explorador:
                piezaMejorada = Resources.Load<GameObject>("Explorador Mineros Mejorado");
                break;
            case Clase.combate:
                piezaMejorada = Resources.Load<GameObject>("Combate Mineros Mejorado");
                break;
            case Clase.investigador:
                piezaMejorada = Resources.Load<GameObject>("Laboratorio Mineros Mejorado");
                break;
            case Clase.estratega:
                piezaMejorada = Resources.Load<GameObject>("Estratega Mineros Mejorado");
                break;            
            default:
                break;
        }        

        if (PhotonNetwork.InRoom && GetComponent<PhotonView>().IsMine)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            PhotonNetwork.Instantiate(piezaMejorada.name, casilla.transform.position, Quaternion.identity);
        }
        else
        {
            casilla.Clear();
            Instantiate(piezaMejorada, casilla.transform.position, Quaternion.identity);
        }
    }

    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponiblesBrutas =  FiltroCasillas.CasillasDeUnJugador(faccion, referencia);
        List<Casilla> casillasDisponiblesNetas = new List<Casilla>();
        foreach (Casilla c in casillasDisponiblesBrutas)
        {
            if (!c.pieza.astro && c.pieza.gameObject.name.Split(' ').Length <= 2) casillasDisponiblesNetas.Add(c);
        }
        return casillasDisponiblesNetas;
    }
}
