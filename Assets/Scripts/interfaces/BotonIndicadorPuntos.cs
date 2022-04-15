using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonIndicadorPuntos : MonoBehaviour
{
    bool active;

    public void Click()
    {
        if (!active) foreach (Casilla c in Tablero.instance.mapa) c.contadorPuntos.GetComponent<IndicadorPuntos>().ActiveInfinite();
        else foreach(Casilla c in Tablero.instance.mapa) c.contadorPuntos.GetComponent<IndicadorPuntos>().Desactive();
        active = !active;
    }
}
