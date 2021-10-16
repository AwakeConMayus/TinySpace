using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IA
{
   

}

public class EstadoIA
{
    int jugador;
    int turno;
    List<Pieza> opciones;
    List<Pieza> opcionesEnemigo;
    Casilla[] Tablero;

    public EstadoIA (int turno_, List<Casilla> mapa, Opciones propias, Opciones enemigas, int jugador_, int enemigo)
    {
        jugador = jugador_;
        turno = turno_;
        Tablero = mapa.ToArray();

        foreach(int i in propias.opcionesDisponibles)
        {
            Pieza pieza = propias.opcionesIniciales[i].GetComponent<Pieza>();
            pieza.Set_Jugador(jugador);
            opciones.Add(pieza);
        }

        foreach (int i in enemigas.opcionesDisponibles)
        {
            Pieza pieza = enemigas.opcionesIniciales[i].GetComponent<Pieza>();
            pieza.Set_Jugador(enemigo);
            opcionesEnemigo.Add(pieza);
        }
    }
  

    public int Evaluar()
    {
        int puntos = 0;
        foreach(Casilla c in Tablero)
        {
            if (c.pieza)
            {
                if(c.pieza.Get_Jugador() == jugador)
                {
                    puntos += c.pieza.Puntos();
                }
                else
                {
                    puntos -= c.pieza.Puntos();
                }
            }
        }
        return puntos;
    }
}
