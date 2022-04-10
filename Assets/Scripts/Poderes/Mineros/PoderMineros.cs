using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderMineros : Poder
{
    //AVISO IMPORTANTE: el objeto en el que vaya este script debe tener un componente photonView
    GameObject meteorito;

    

    public override void InitialAction(bool pasar_turno)
    {
        meteorito = Resources.Load("Meteorito", typeof(GameObject)) as GameObject;


        for (int i = 0; i < 10; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!meteorito.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));


            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.Instantiate(meteorito.name, Tablero.instance.mapa[rnd].transform.position, Quaternion.identity);
                base.photonView.RPC("RPC_LlenarCasillaConMeteorito", RpcTarget.All, rnd);
            }
            else
            {
                GameObject thisPieza = Instantiate(meteorito);
                thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                Tablero.instance.mapa[rnd].meteorito = true;

            }

        }

        if (!pasar_turno)
        {
            Debug.Log("minero no he pasado turno paso");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }


    [PunRPC]
    public void RPC_LlenarCasillaConMeteorito(int i)
    {
        Tablero.instance.mapa[i].meteorito = true;
    }
}
