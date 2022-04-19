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

    bool extra = false;

    Casilla casilla;

    public void Convocar(Casilla c, bool sinPasarTurno = false)
    {
        extra = sinPasarTurno;
        gameObject.SetActive(true);
        transform.position = c.transform.position;
        casilla = c;
    }

    public void Seleccion(GameObject prefab)
    {


        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            GameObject g = PhotonNetwork.Instantiate(prefab.name, casilla.transform.position, Quaternion.identity);

            if (extra)
            {
                extra = false;
                //colocar pieza extra
                g.GetComponent<Pieza>().Set_Pieza_Extra();

            }
        }
        else
        {
            GameObject thisPieza = Instantiate(prefab);
            thisPieza.transform.position = casilla.transform.position;
            if (extra)
            {
                extra = false;
                //colocar pieza extra
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();

            }
            thisPieza.transform.position = casilla.transform.position;
        }

        EventManager.TriggerEvent("DesbloquearInput");
        gameObject.SetActive(false);
    }
}
