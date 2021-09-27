using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject thisPieza = Instantiate(prefab);
        thisPieza.transform.position = casilla.transform.position;
        thisPieza.GetComponent<Pieza>().Colocar(casilla);
        gameObject.SetActive(false);
    }
}
