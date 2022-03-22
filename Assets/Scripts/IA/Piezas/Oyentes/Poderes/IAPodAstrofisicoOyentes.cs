using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodAstrofisicoOyentes : PoderIABase
{
    public bool fase2 = false;
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        if (fase2)
        {
            IATablero.instance.PrintInfoTablero(tabBase);
            AgujeroNegro.ActivarAgujerosNegros(IATablero.instance.mapa);
            tabBase = new InfoTablero(IATablero.instance.mapa);
        }
        else
        {
            tabBase = PonerMejorPlaneta(tabBase);
        }

        List<InfoTablero> opciones = new List<InfoTablero>();

        Pieza agujeroNegro = Resources.Load<Pieza>("Agujero Negro");

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in agujeroNegro.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            AgujeroNegro.ActivarAgujeroNegro(c);
            c.pieza = agujeroNegro;
            print("post apocaliptic simulator: " + Auxiliar.StringArrayInt(new InfoTablero(IATablero.instance.mapa).tablero));
            opciones.Add(new InfoTablero(IATablero.instance.mapa));
        }
        if (opciones.Count == 0) opciones.Add(tabBase);
        return opciones;
    }
}
