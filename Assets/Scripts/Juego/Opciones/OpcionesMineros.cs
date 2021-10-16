using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesMineros : Opciones
{
    public GameObject[] mejoras = new GameObject[4];
    GameObject[] backup = new GameObject[4];
    PoderMineros poderMinero;

    public int mineral = 5;

    public bool especial = false;

    int mineralGastar;
    
    private void Start()
    {
        EventManager.StartListening("RecogerMineral", RecogerMineral);
        EventManager.StartListening("ColocarPieza", EjecutarPago);
        for (int i = 0; i < 4; i++)
        {
            backup[i] = opcionesIniciales[i];
        }
        Preparacion();
    }
    public void RecogerMineral()
    {
        ++mineral;
        EventManager.TriggerEvent("CambioMineral");
    }

    public bool GastarMineral(int i)
    {
        if (mineral >= i)
        {
            mineralGastar = i;
            return true;
        }
        else return false;
    }

    public void EjecutarPago()
    {
        if (mineralGastar == 0) return;
        mineral -= mineralGastar;
        EventManager.TriggerEvent("CambioMineral");
        mineralGastar = 0;
    }

    public override void Seleccion(int i)
    {
        if (!active) return;

        if (opcionesDisponibles[i] == 4)
        {
            if (GastarMineral(opcionesIniciales[4].GetComponent<Especial>().coste))
            {
                base.Seleccion(i);
            }
        }

        else if (especial)
        {
            if (GastarMineral(3))
            {
                base.Seleccion(i);
            }
        }
        else base.Seleccion(i);
    }

    public void EspecialMode()
    {
        InstancePiezas.instance.LimpiarPieza();
        mineralGastar = 0;
        especial = !especial;
        if (especial)
        {
            for (int i = 0; i < 4; i++)
            {
                opcionesIniciales[i] = mejoras[i];
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                opcionesIniciales[i] = backup[i];
            }
        }
        EventManager.TriggerEvent("RotacionOpciones");
    }
}
