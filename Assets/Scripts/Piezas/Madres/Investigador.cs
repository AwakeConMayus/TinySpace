using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigador : Pieza
{
    public virtual bool Anulador()
    {
        return true;
    }

    public override int Puntos()
    {
        int limite = 4;
        int puntos = 0;
        int incremento = 0;
        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if(adyacente.pieza.CompareClase(Clase.explorador))
            {
                foreach(Casilla adyacente2 in adyacente.adyacentes)
                {
                    if(adyacente2 && adyacente2 != casilla && adyacente2.pieza && adyacente2.pieza.CompareClase(Clase.investigador) && adyacente2.pieza.jugador == jugador)
                    {
                        Investigador invAdyacente = (Investigador)adyacente2.pieza;
                        if(invAdyacente.Anulador()) return 0;
                    }

                    if(incremento < limite)
                    {
                        ++incremento;
                        puntos += incremento;
                    }
                }
            }
        }
        return puntos;
    }

    public override List<Casilla> CasillasDisponibles()
    {
        throw new System.NotImplementedException();
    }
}
