﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodChantajistaMineros : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasDeOtroJugador(faccion, IATablero.instance.mapa);
        casillasPosibles = FiltroCasillas.CasillasNoAstro(casillasPosibles);


        foreach (Casilla c in casillasPosibles)
        {
            IATablero.instance.PrintInfoTablero(tabBase);
            foreach (Casilla cc in FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa))
            {
                IATablero.instance.PrintInfoTablero(tabBase);
                Pieza aux = c.pieza;
                c.pieza = cc.pieza;
                cc.pieza = aux;
                opciones.Add(new InfoTablero(IATablero.instance.mapa));
            }
        }

        return opciones;
    }
}
