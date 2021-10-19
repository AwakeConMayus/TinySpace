﻿using System.Collections;
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
        if (especial) EspecialMode();
    }

    public override void Seleccion(int i)
    {
        if (!active) return;

        if (opcionesDisponibles[i] == 4)
        {
            if (GastarMineral(opcionesIniciales[4].GetComponent<Especial>().coste))
            {
                //Clanta: Este codigo no gustar a clanta. Clanta no saber programar. If (clanta == mejor programador) --clanta if's;
                if (opcionesIniciales[4].GetComponent<ModeloPerfecionadoMineros>())
                {
                    if(opcionesDisponibles[0] != 4)
                    {
                        opcionesIniciales[4].GetComponent<ModeloPerfecionadoMineros>().pieza_a_mejorar = mejoras[opcionesDisponibles[0]];
                    }
                    else
                    {
                        opcionesIniciales[4].GetComponent<ModeloPerfecionadoMineros>().pieza_a_mejorar = mejoras[opcionesDisponibles[1]];

                    }
                }
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
        else
        {
            GastarMineral(0);
            base.Seleccion(i);
        }
    }

    public void EspecialMode()
    {
        if (!active) return;
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
