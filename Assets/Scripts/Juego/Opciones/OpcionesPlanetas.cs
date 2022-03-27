using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesPlanetas : Opciones
{
    private void Start()
    {
        Preparacion();
        if (opcionesIniciales[4].GetComponent<Terraformar>())
        {
            int index = opcionesDisponibles.IndexOf(4);
            opcionesDisponibles[index] = opcionesDisponibles[0];
            opcionesDisponibles[0] = 4;
            EventManager.TriggerEvent("RotacionOpciones");
        }
    }

    public override bool Ahogado()
    {
        for (int i = 0; i < 3; i++)
        {
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0) return false;            
        }
        return true;
    }
}
