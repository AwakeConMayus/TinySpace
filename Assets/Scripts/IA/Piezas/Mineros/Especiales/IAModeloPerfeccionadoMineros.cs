using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAModeloPerfeccionadoMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        PiezaIA laboratorioMejorado = Resources.Load<GameObject>("Laboratorio Mineros Mejorado").GetComponent<PiezaIA>();

        opciones.Add(laboratorioMejorado.BestInmediateOpcion(tabBase));

        PiezaIA estrategaMejorado = Resources.Load<GameObject>("Estratega Mineros Mejorado").GetComponent<PiezaIA>();

        opciones.Add(estrategaMejorado.BestInmediateOpcion(tabBase));

        PiezaIA exploradorMejorado = Resources.Load<GameObject>("Mejora Explorador Mineros").GetComponent<PiezaIA>();

        opciones.Add(exploradorMejorado.BestInmediateOpcion(tabBase));

        PiezaIA combateMejorado = Resources.Load<GameObject>("Mejora Combate Mineros").GetComponent<PiezaIA>();

        opciones.Add(combateMejorado.BestInmediateOpcion(tabBase));


        return opciones;
    }
}
