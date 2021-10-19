using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderChantajista : PoderMineros
{
    [SerializeField] GameObject menu_chantaje;

    [SerializeField] GameObject garrapata_prefab;

    GarrapataMinero mi_garrapata;

    public int seleccion = 0;


    private void Start()
    {
        EventManager.StartListening("ColocarPieza", Espiar);
    }
    public void Espiar()
    {
        if (seleccion == mi_garrapata.nivel)
        {
            base.photonView.RPC("RPC_Avisar1", RpcTarget.All);
            seleccion = 0;
        }
    }

    [PunRPC]
    public void RPC_Avisar1()
    {
        Avisar();
    }
    public void Avisar()
    {
        mi_garrapata.Chantaje();
    }
    public void Setear_Nivel(int i)
    {
        base.photonView.RPC("RPC_SetearNivel1", RpcTarget.All, i);
    }
    [PunRPC]
    public void RPC_SetearNivel1(int i)
    {
        mi_garrapata.nivel = i;
    }

    public override void FirstAction()
    {
        menu_chantaje.SetActive(true);
    }

    public override void SecondAction()
    {
        padre.SetActive(true);

    }

    public void Crear_Chantaje(int i)
    {
        GameObject g =  PhotonNetwork.Instantiate(garrapata_prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        mi_garrapata = g.GetComponent<GarrapataMinero>();
        WalkitolkiGarrapata.instance.Setear_Nivel(i);
        menu_chantaje.SetActive(false);
    }
}
