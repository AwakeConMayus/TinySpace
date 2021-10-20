using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderChantajista : PoderMineros
{
    [SerializeField] GameObject menu_chantaje;

    [SerializeField] GameObject garrapata_prefab;

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
        if (!WalkitolkiGarrapata.instance)
        {
            Debug.LogError("No se ha creado todavia el walkitolkie");
            return;
        } 
        WalkitolkiGarrapata.instance.Setear_Nivel(i);
        menu_chantaje.SetActive(false);
    }
}
