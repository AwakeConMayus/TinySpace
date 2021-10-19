using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNave : Especial
{
    protected override void SetClase()
    {
        clase = Clase.comodin;
    }
    public override List<Casilla> CasillasDisponibles(List<Casilla> referencia = null)
    {
        List<Casilla> casillasDisponibles = FiltroCasillas.CasillasLibres(referencia);
        return casillasDisponibles;
    }
    public override int Puntos()
    {
        int puntos = 0;

        //Explorador
        int numPuntosPorClase = 2;

        List<Clase> clasesExploradas = new List<Clase>();

        foreach (Casilla adyacete in casilla.adyacentes)
        {
            if (!adyacete || !adyacete.pieza) continue;

            if (!clasesExploradas.Contains(adyacete.pieza.clase))
            {
                clasesExploradas.Add(adyacete.pieza.clase);
            }
        }
        puntos += clasesExploradas.Count * numPuntosPorClase;


        //Combate
        int numPuntosCombateAliadas = 3;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.combate) && adyacente.pieza.Get_Jugador() == jugador)
            {
                puntos += numPuntosCombateAliadas;
            }
        }

        //Laboratorio
        int puntosIniciales = 1;
        int incremento = 2;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.explorador))
            {
                puntos += puntosIniciales;
                puntosIniciales += incremento;
            }
        }

        //Estratega
        int puntosCombateCercano = 3;

        foreach (Casilla adyacente in casilla.adyacentes)
        {
            if (!adyacente || !adyacente.pieza) continue;
            if (adyacente.pieza.CompareClase(Clase.combate))
            {
                puntos += puntosCombateCercano;
            }
        }


        return puntos;
    }
}
