using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EspejoMaestro : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TuSeleccion seleccion_rival;
    public List<GameObject> reflejos;

    public OpcionesRival Activar(Faccion f)
    {
        foreach(GameObject g in reflejos)
        {
            g.SetActive(true);
            if(g.GetComponent<OpcionesRival>().faccion == f)
            {
                base.photonView.RPC("RPC_MostrarReflejo", RpcTarget.Others, (int)f);

                foreach (RectTransform gg in g.GetComponentsInChildren<RectTransform>())
                {
                    gg.gameObject.SetActive(false);
                }
                return g.GetComponent<OpcionesRival>();
            }
            g.SetActive(false);
           
        }

        return null;
    }

    [PunRPC]
    public void RPC_MostrarReflejo(int i)
    {
        Faccion f = (Faccion)i;

        foreach (GameObject g in reflejos)
        {
            if (g.GetComponent<OpcionesRival>().faccion == f)
            {
                g.SetActive(true);
                g.GetComponent<OpcionesRival>().opcionesIniciales = seleccion_rival.mis_opciones;
            }
        }
    }
}
