using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IA
{
    
    public static List<int> InmediateSimple(EstadoIASimple estado)
    {
        List<int> jugada = new List<int>();
        int result = -100;
        for (int i = 0; i < 3; i++)
        {
            Pieza pieza = estado.opciones[i];

            EstadoIASimple referencia = estado;
            referencia.RotarPieza(pieza);

            for (int j = 0; j < pieza.CasillasDisponibles().Count; j++)
            {
                Casilla c = pieza.CasillasDisponibles()[j];

                referencia.PonerPieza(pieza, c);
                int puntos = referencia.Evaluar();
                if (puntos > result)
                {
                    result = puntos;
                    jugada = new List<int> { i, j };
                }
            }
        }
        return jugada;
    }

}

public class EstadoIA
{
    int jugador;
    bool activo;
    int turno;
    List<Pieza> opciones;
    List<Pieza> opcionesEnemigo;
    Poder poder, poderEnemigo;
    List<Casilla> Tablero;

    public EstadoIA (int turno_, List<Casilla> mapa, Opciones propias, Opciones enemigas, int jugador_, int enemigo_)
    {
        activo = true;
        jugador = jugador_;
        turno = turno_;
        Tablero = mapa;

        poder = propias.poder.GetComponent<Poder>();
        poderEnemigo = enemigas.poder.GetComponent<Poder>();

        foreach(int i in propias.opcionesDisponibles)
        {
            Pieza pieza = propias.opcionesIniciales[i].GetComponent<Pieza>();
            pieza.Set_Jugador(jugador);
            opciones.Add(pieza);
        }

        foreach (int i in enemigas.opcionesDisponibles)
        {
            Pieza pieza = enemigas.opcionesIniciales[i].GetComponent<Pieza>();
            pieza.Set_Jugador(enemigo_);
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


public class EstadoIASimple
{
    public int jugador;
    public int turno;
    public List<Pieza> opciones;
    public Poder poder;
    public List<Casilla> Tablero;

    public EstadoIASimple(int turno_, List<Casilla> mapa, Opciones propias, int jugador_)
    {
        jugador = jugador_;
        turno = turno_;
        Tablero = mapa;

        poder = propias.poder.GetComponent<Poder>();

        foreach (int i in propias.opcionesDisponibles)
        {
            Pieza pieza = propias.opcionesIniciales[i].GetComponent<Pieza>();
            pieza.Set_Jugador(jugador);
            opciones.Add(pieza);
        }
    }

    public void RotarPieza (Pieza piezaRotar)
    {
        opciones.Remove(piezaRotar);
        opciones.Add(piezaRotar);
    }

    public void PonerPieza(Pieza piezaColocar, Casilla casillaColocar)
    {
        foreach(Casilla c in Tablero)
        {
            if (c == casillaColocar) c.pieza = piezaColocar;
        }
    }

    public int Evaluar()
    {
        int puntos = 0;
        foreach (Casilla c in Tablero)
        {
            if (c.pieza)
            {
                if (c.pieza.Get_Jugador() == jugador)
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
