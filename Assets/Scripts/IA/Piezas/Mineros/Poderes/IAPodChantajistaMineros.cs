using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodChantajistaMineros : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);
        List<Casilla> casillasPosibles = FiltroCasillas.CasillasDeUnJugador(faccion, IATablero.instance.mapa);
        casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, true);
        casillasPosibles = FiltroCasillas.CasillasDeOtroJugador(faccion, casillasPosibles);

        return opciones;
    }
}
