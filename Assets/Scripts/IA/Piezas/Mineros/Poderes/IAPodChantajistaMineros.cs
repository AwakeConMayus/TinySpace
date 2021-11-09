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
        casillasPosibles = FiltroCasillas.RestaLista(casillasPosibles, FiltroCasillas.CasillasDeUnTipo(Clase.astros, IATablero.instance.mapa));


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
