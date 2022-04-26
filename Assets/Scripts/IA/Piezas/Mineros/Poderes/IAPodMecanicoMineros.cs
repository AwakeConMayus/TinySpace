using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodMecanicoMineros : PoderIABase
{
    [SerializeField] List<Pieza> piezas;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();
        

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in FiltroCasillas.CasillasLibres(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            foreach(Pieza p in piezas)
            {
                c.pieza = p;
                c.pieza.casilla = c;
                if (!simplify || c.pieza.Puntos() > 0) opciones.Add(new InfoTablero(IATablero.instance.mapa));
            }
        }

        return opciones;
    }
}
