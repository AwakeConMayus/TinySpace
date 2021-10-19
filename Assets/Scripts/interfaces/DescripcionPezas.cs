using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescripcionPezas : MonoBehaviour
{
    public GameObject puntosPieza;
    Text textoExplicatvo;
    TextoExplicativo explicaciones;

    private void Awake()
    {
        textoExplicatvo = GetComponentInChildren<Text>();
        explicaciones = Resources.Load<TextoExplicativo>("Textos");

        EventManager.StartListening("ClickCasilla", Active);

        gameObject.SetActive(false);
    }


    public void Active()
    {
        if (!ClickCasillas.casillaClick || !ClickCasillas.casillaClick.pieza || !puntosPieza.activeSelf) return;
        CancelInvoke();
        Pieza pieza = ClickCasillas.casillaClick.pieza;
        gameObject.SetActive(true);
        textoExplicatvo.text = explicaciones.GetTexto(pieza.gameObject);
        Invoke("Desactive", 3f);
    }

    public void Desactive()
    {
        gameObject.SetActive(false);
    }
}
