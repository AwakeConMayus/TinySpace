using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicadorPuntos : MonoBehaviour
{
    public static IndicadorPuntos instance;

    Text textoPuntos;
    Canvas myCanvas;
    public float posicionVertical;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        textoPuntos = GetComponentInChildren<Text>();
        myCanvas = GetComponentInParent<Canvas>();

        EventManager.StartListening("ClickCasilla", Active);

        gameObject.SetActive(false);
    }


    public void Active()
    {
        if (!ClickCasillas.casillaClick || !ClickCasillas.casillaClick.pieza) return;
        CancelInvoke();
        Pieza pieza = ClickCasillas.casillaClick.pieza;
        gameObject.SetActive(true);
        transform.localPosition = Camera.main.WorldToScreenPoint(pieza.transform.position) - myCanvas.transform.localPosition + new Vector3(0,posicionVertical,0);
        textoPuntos.text = pieza.Puntos().ToString();
        Invoke("Desactive", 2f);
    }

    public void Desactive()
    {
        gameObject.SetActive(false);
    }
}
