using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodAstrofisicoOyentes : PoderIABase
{
    public bool fase2 = false;

    [SerializeField] AgujeroNegro agujeroNegro;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        if (fase2)
        {
            /*
            IATablero.instance.PrintInfoTablero(tabBase);
            AgujeroNegro.ActivarAgujerosNegros(IATablero.instance.mapa);
            tabBase = new InfoTablero(IATablero.instance.mapa);
            */
        }
        else
        {
            tabBase = PonerMejorPlaneta(tabBase);
        }

        List<InfoTablero> opciones = new List<InfoTablero>();        

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in agujeroNegro.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            AgujeroNegro.ActivarAgujeroNegro(c);
            c.pieza = agujeroNegro;;
            opciones.Add(new InfoTablero(IATablero.instance.mapa));
        }
        if (opciones.Count == 0) opciones.Add(tabBase);
        return opciones;
    }
}
