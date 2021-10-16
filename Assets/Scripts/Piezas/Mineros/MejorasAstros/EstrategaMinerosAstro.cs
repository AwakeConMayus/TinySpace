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
                if (adyacente && adyacente.pieza && adyacente.pieza.Get_Jugador() == jugador &&
                    adyacente.pieza.clase != Clase.combate && adyacente.pieza.clase != Clase.astros)
                {
                    OnlineManager.instance.Destroy_This_Pieza(adyacente.pieza);
                    //StartCoroutine(Instanciar_nave_Combate(adyacente));
                    GameObject thisPieza = PhotonNetwork.Instantiate(combateMinero.name, adyacente.transform.position, Quaternion.identity);
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                }
            }
        }
        base.Colocar(c);
    }

    IEnumerator Instanciar_nave_Combate(Casilla adyacente)
    {
        yield return new WaitForSeconds(1);
        GameObject thisPieza = PhotonNetwork.Instantiate(combateMinero.name, adyacente.transform.position, Quaternion.identity);
        thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
    }
}
