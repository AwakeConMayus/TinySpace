using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono2Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        Pieza piezaReferencia = Resources.Load<Pieza>("Planeta Sagrado Planetarios");


        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in IATablero.instance.mapa)
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            if (c.pieza && c.pieza.GetComponent<Planetas>())
            {
                
                c.pieza = piezaReferencia;
                piezaReferencia.casilla = c;

                nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            }

        }

        return nuevosEstados;
    }
}
