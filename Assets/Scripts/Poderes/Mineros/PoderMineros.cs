using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PoderMineros : Poder
{
    //AVISO IMPORTANTE: el objeto en el que vaya este script debe tener un componente photonView
    public int mineral = 5;
    GameObject meteorito;

    private void Start()
    {
        EventManager.StartListening("RecogerMineral", RecogerMineral);
    }
    public void RecogerMineral()
    {
        ++mineral;
        EventManager.TriggerEvent("CambioMineral");
    }

    public bool GastarMineral(int i)
    {
        if (mineral >= i)
        {
            mineral -= i;
            EventManager.TriggerEvent("CambioMineral");
            return true;
        }
        else return false;
    }

    public override void InitialAction()
    {
        meteorito = Resources.Load("Meteorito", typeof(GameObject)) as GameObject;
        meteorito.GetComponent<Pieza>().jugador = jugador;


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
    }


    [PunRPC]
    public void RPC_LlenarCasillaConMeteorito(int i)
    {
        Tablero.instance.mapa[i].meteorito = true;
    }
}
