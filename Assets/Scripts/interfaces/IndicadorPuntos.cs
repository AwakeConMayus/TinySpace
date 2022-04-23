using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicadorPuntos : MonoBehaviour
{
    Text textoPuntos;
    public float posicionVertical;
    public Casilla casilla;

    bool active, realActive;

    private void Awake()
    {      
        textoPuntos = GetComponentInChildren<Text>();

        EventManager.StartListening("UpdateScore", UpdateText);

        gameObject.SetActive(false);
    }
    

    public void Active()
    {
        if (!ClickCasillas.casillaClick || !ClickCasillas.casillaClick.pieza || ClickCasillas.casillaClick.transform.position != casilla.transform.position || active) return;
        CancelInvoke();
        active = true;
        gameObject.SetActive(true);
        Invoke("Desactive", 2f);
    }

    public void ActiveInfinite()
    {
        CancelInvoke();
        active = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (active && casilla.pieza && !casilla.pieza.GetComponent<AgujeroNegro>() && casilla.pieza.Puntos() != 0)
        {
            if (realActive && !casilla.pieza)
            {
                realActive = false;
                gameObject.SetActive(false);
            }
            else if (!realActive && casilla.pieza)
            {
                gameObject.SetActive(true);
                transform.position = casilla.pieza.transform.position + new Vector3(0, 12, 0);
                realActive = true;
                UpdateText();
            }
        }
        else
        {
            gameObject.SetActive(false);
            realActive = false;
        }
    }

    public void UpdateText()
    {
        if (!active || !casilla.pieza) return;
        switch (casilla.pieza.faccion)
        {
            case Faccion.minero:
                textoPuntos.color = Color.red;
                break;
            case Faccion.oyente:
                textoPuntos.color = Color.yellow;
                break;
        }
        gameObject.SetActive(true);
        textoPuntos.text = casilla.pieza.Puntos().ToString();
    }

    public void Desactive()
    {
        active = false;
    }
}
