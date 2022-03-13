using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuTerraformador : MonoBehaviour
{
    public static MenuTerraformador instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        gameObject.SetActive(false);
    }

    Casilla casilla;

    public void Convocar(Casilla c)
    {
        
        gameObject.SetActive(true);
        transform.position = c.transform.position;
        casilla = c;
    }

    public void Seleccion(GameObject prefab)
    {
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            OnlineManager.instance.Destroy_This_Pieza(casilla.pieza);
            PhotonNetwork.Instantiate(prefab.name, casilla.transform.position, Quaternion.identity);
        }
        else
        {
            casilla.Clear();
            GameObject thisPieza = Instantiate(prefab);
            thisPieza.transform.position = casilla.transform.position;
        }
        EventManager.TriggerEvent("DesbloquearInput");
        gameObject.SetActive(false);
    }
}
