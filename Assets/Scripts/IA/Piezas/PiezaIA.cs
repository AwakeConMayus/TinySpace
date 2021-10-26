﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PiezaIA : MonoBehaviour
{
    public int jugador;
    public abstract List<List<Casilla>> Opcionificador(List<Casilla> listaBase);   

    public virtual List<Casilla> BestInmediateOpcion(List<Casilla> listaBase)
    {
        List<Casilla> MejorOpcion = new List<Casilla>();
        int mejorPuntuacion = -1000;


        foreach (List<Casilla> lc in Opcionificador(listaBase))
        {
            if (Evaluar(lc, jugador) > mejorPuntuacion)
            {
                mejorPuntuacion = Evaluar(lc, jugador);
                MejorOpcion = lc;
            }
        }

        return MejorOpcion;
    }

    public static int Evaluar(List<Casilla> mapa, int jugador)
    {
        int puntos = 0;
        foreach(Casilla c in mapa)
        {
            if (c.pieza)
            {
                if (c.pieza.Get_Jugador() == jugador)
                {
                    puntos += c.pieza.Puntos();
                }
                else puntos -= c.pieza.Puntos();
            }
        }
        return puntos;
    }
}
