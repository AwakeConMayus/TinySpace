using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALabMejMineros : PiezaIA
{
    [SerializeField] Pieza laboratorioAstro;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            c.pieza = laboratorioAstro;
            laboratorioAstro.casilla = c;

            List<Casilla> rangoDestrucciones = FiltroCasillas.CasillasAdyacentes(FiltroCasillas.CasillasAdyacentes(c, true), false);

            foreach(Casilla cc in rangoDestrucciones)
            {
                if(cc.pieza && cc.pieza.faccion != faccion && !cc.pieza.astro)
                {
                    Pieza pc = cc.pieza;
                    cc.pieza = null;
                    nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
                    cc.pieza = pc;
                }
            }

        }

        return nuevosEstados;
    }
}
