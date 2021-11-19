using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesPlanetas : Opciones
{
    private void Start()
    {
        Preparacion();
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
