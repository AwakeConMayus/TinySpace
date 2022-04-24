using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesPlanetas : Opciones
{
    List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

   

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

        foreach (GameObject g in opcionesIniciales) abanicoOpciones.Add(g.GetComponent<PiezaIA>());

    }

    public override bool Ahogado()
    {
        for (int i = 0; i < 3; i++)
        {
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0) return false;            
        }
        return true;
    }

    public override InfoTablero BestRespuesta(InfoTablero tabBase)
    {
        List<InfoTablero> respuestas = new List<InfoTablero>();

        foreach (PiezaIA pia in abanicoOpciones) respuestas.Add(pia.BestInmediateOpcion(tabBase));

        int bestRespuesta = int.MinValue;

        InfoTablero respuesta = new InfoTablero();
        foreach (InfoTablero it in respuestas)
        {
            IATablero.instance.PrintInfoTablero(it);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if (puntos > bestRespuesta)
            {
                bestRespuesta = puntos;
                respuesta = it;
            }
        }
        return respuesta;
    }
}
