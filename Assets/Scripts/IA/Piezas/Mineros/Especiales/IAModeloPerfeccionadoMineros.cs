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

        
        IATablero.instance.PrintInfoTablero(tabBase);
        Pieza exploradorMejorado = Resources.Load<Pieza>("Explorador Mineros Mejorado");
        Pieza combateMejorado = Resources.Load<Pieza>("Combate Mineros Mejorado");


        foreach (Casilla c in IATablero.instance.mapa)
        {
            if (c.pieza && c.pieza.faccion == faccion)
            {
                if(c.pieza.clase == Clase.explorador)
                {
                    c.pieza = exploradorMejorado;
                    c.pieza.casilla = c;
                    opciones.Add(new InfoTablero(IATablero.instance.mapa));
                    IATablero.instance.PrintInfoTablero(tabBase);
                }
                else if (c.pieza.clase == Clase.combate)
                {
                    c.pieza = combateMejorado;
                    c.pieza.casilla = c;
                    opciones.Add(new InfoTablero(IATablero.instance.mapa));
                    IATablero.instance.PrintInfoTablero(tabBase);
                }
            }
        }

        return opciones;
    }
}
