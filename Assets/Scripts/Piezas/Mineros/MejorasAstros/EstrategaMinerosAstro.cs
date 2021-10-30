using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EstrategaMinerosAstro : EstrategaMineros
{
    [SerializeField] GameObject combateMinero;

    protected override void SetClase()
    {
        clase = Clase.astros;
    }

    public override void Colocar(Casilla c)
    {
        if (gameObject.GetPhotonView().IsMine)
        {
            foreach (Casilla adyacente in c.adyacentes)
            {
                if (adyacente && adyacente.pieza && adyacente.pieza.faccion == faccion &&
                    adyacente.pieza.clase != Clase.combate && adyacente.pieza.clase != Clase.astros)
                {
                    OnlineManager.instance.Destroy_This_Pieza(adyacente.pieza);
                    GameObject thisPieza = PhotonNetwork.Instantiate(combateMinero.name, adyacente.transform.position, Quaternion.identity);
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                }
            }
        }
        base.Colocar(c);
    }
    public override int Puntos()
    {
        int puntosCombateCercano = 4;

        int puntos = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.combate))
            {
                puntos += puntosCombateCercano;
            }
        }
        return puntos;
    }
}
