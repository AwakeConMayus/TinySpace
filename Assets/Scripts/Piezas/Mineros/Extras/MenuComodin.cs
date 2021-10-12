using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuComodin : MonoBehaviour
{
    public static MenuComodin instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        gameObject.SetActive(false);
    }

    Casilla casilla;

    public void Convocar(Casilla c)
    {
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
        casilla = c;
    }

    public void Seleccion(GameObject prefab)
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.Instantiate(prefab.name, casilla.transform.position, Quaternion.identity);
        }
        else
        {
            GameObject thisPieza = Instantiate(prefab);
            thisPieza.transform.position = casilla.transform.position;
        }
        EventManager.TriggerEvent("DesbloquearInput");
        gameObject.SetActive(false);
    }
}
