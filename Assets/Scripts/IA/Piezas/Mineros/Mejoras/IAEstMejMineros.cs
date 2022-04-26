using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEstMejMineros : PiezaIA
{
    [SerializeField] Pieza estrategaAstro, combateMinero;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            c.pieza = estrategaAstro;
            estrategaAstro.casilla = c;


            foreach (Casilla cc in FiltroCasillas.CasillasEnRango(2, c, false))
            {
                if (cc && cc.pieza && cc.pieza.faccion == faccion && !cc.pieza.CompareClase(Clase.combate) && !cc.pieza.astro)
                {
                    cc.pieza = combateMinero;
                }
            }

            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));

        }
        return nuevosEstados;
    }
}
