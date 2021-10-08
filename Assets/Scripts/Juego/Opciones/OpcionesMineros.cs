using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesMineros : Opciones
{
    public GameObject[] mejoras = new GameObject[4];
    PoderMineros poderMinero;

    public int mineral = 5;

    bool especial = false;
    
    private void Start()
    {
        EventManager.StartListening("RecogerMineral", RecogerMineral);
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
            mineral -= i;
            EventManager.TriggerEvent("CambioMineral");
            return true;
        }
        else return false;
    }

    public override void Seleccion(int i)
    {
        if(opcionesDisponibles[i] == 4)
        {
            if (GastarMineral(opcionesIniciales[4].GetComponent<Especial>().coste))
            {
                base.Seleccion(i);
            }
        }
        else if (especial)
        {
            if (GastarMineral(2))
            {
                opcionActual = i;
                if(opcionActual < 4) InstancePiezas.instance.SetPieza(mejoras[opcionesDisponibles[i]]);
                else InstancePiezas.instance.SetPieza(opcionesIniciales[opcionesDisponibles[i]]);
                especial = false;
            }
        }
        else base.Seleccion(i);
    }

    public void EspecialMode()
    {
        especial = !especial;
    }
}
