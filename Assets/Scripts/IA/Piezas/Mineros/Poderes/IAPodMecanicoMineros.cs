using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMecanicoMineros : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        Pieza[] piezas = new Pieza[4];

        piezas[0] = Resources.Load<Pieza>("Explorador Mineros");
        piezas[1] = Resources.Load<Pieza>("Combate Mineros");
        piezas[2] = Resources.Load<Pieza>("Laboratorio Mineros");
        piezas[3] = Resources.Load<Pieza>("Estratega Mineros");

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in FiltroCasillas.CasillasLibres(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            foreach(Pieza p in piezas)
            {
                c.pieza = p;
                opciones.Add(new InfoTablero(IATablero.instance.mapa));
            }
        }

        return opciones;
    }
}
