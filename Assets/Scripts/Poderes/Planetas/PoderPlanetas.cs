using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderPlanetas : Poder
{
    [SerializeField]
    protected GameObject planeta;

    public override void InitialAction()
    {
        for (int i = 0; i < 3; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!planeta.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                base.photonView.RPC("RPC_InstanciarPlaneta", RpcTarget.All, rnd);

            }
            else
            {
                GameObject thisPieza = Instantiate(planeta);
                thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();
            }
        }
        ColorearCasillas.instance.initialColor();
        EventManager.TriggerEvent("AccionTerminada");
    }
}
