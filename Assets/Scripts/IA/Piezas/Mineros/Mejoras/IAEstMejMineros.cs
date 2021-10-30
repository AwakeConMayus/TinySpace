using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEstMejMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        Pieza piezaReferencia;
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();
        piezaReferencia = GetComponent<Pieza>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            Pieza piezaColocar = Resources.Load<Pieza>("EstrategaMinerosAstro");

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;


            foreach (Casilla cc in c.adyacentes)
            {
                if (cc && cc.pieza && cc.pieza.faccion == faccion && !cc.pieza.CompareClase(Clase.combate))
                {
                    Pieza nuevaPieza = Resources.Load<Pieza>("Combate Mineros");
                    cc.pieza = nuevaPieza;
                }
            }

            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));

        }
        return nuevosEstados;
    }
}
