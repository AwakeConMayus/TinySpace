﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum estados
{
    SelectPieza,
    SelectCasilla    
}

public class InstancePiezas : MonoBehaviour
{
    [HideInInspector]
    public GameObject pieza;
    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;
    //* Para el tutorial, mineros 0, planetas 1
    [HideInInspector]

    public GameObject planeta;

    estados estado = estados.SelectPieza;

    List<Casilla> casillasPosibles = new List<Casilla>();



    public void SetJugador(int player)
    {
        jugador = player;


        //Colores
        foreach (Casilla casilla in Tablero.instance.mapa)
        {
            if (casilla.pieza && casilla.pieza.jugador != jugador) ColorearCasillas.instance.reColor("red", casilla);
            else ColorearCasillas.instance.initialColor(casilla);
        }
        //End Colores
    }

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }


    public void SetPieza(GameObject nave)
    {
        pieza = nave;
        pieza.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = nave.GetComponent<Pieza>().CasillasDisponibles();

        //* Pinta de verde las casillas sobre las que se puede posicionar una pieza
        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);
        
        //* Pinta de rojo las casillas sobre las que ha posicionado una pieza el rival
        //foreach (Casilla casilla in Tablero.instance.mapa) 
        //{
        //    if (casilla.pieza && casilla.pieza.jugador != jugador) coloreador.reColor("red", casilla);
        //}

        estado = estados.SelectCasilla;
    }

    public void CrearPieza()
    {

        Casilla c = ClickCasillas.casillaClick;
        if (estado == estados.SelectPieza || c == null) return; 

        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza = Instantiate(pieza);
            thisPieza.transform.position = c.transform.position;
            thisPieza.GetComponent<Pieza>().Colocar(c);

            foreach (Casilla casilla in Tablero.instance.mapa)
            {
                if (casilla.pieza && casilla.pieza.jugador != jugador) ColorearCasillas.instance.reColor("red", casilla);
                else ColorearCasillas.instance.initialColor(casilla);
            }
            estado = estados.SelectPieza;
        }

        foreach(Casilla casilla in Tablero.instance.mapa)
        if (c.pieza && c.pieza.jugador == jugador || !c.pieza) ColorearCasillas.instance.initialColor(casilla);
    }

    public void RecuentoPuntosTest()
    {
        int[] puntuaciones = new int[2];

        foreach(Casilla c in Tablero.instance.mapa)
        {
            if (c.pieza)
            {
                puntuaciones[c.pieza.jugador] += c.pieza.Puntos();
            }
        }

        print("Mineros: " + puntuaciones[0] + " / Planetas: " + puntuaciones[1]);
    }

    public void SetInicialTable()
    {
        foreach(Casilla c in Tablero.instance.mapa)
        {
            ColorearCasillas.instance.initialColor(c);
            c.Clear();
        }
        for (int i = 0; i < 3; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!planeta.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));
            //TODO: Los planetas a veces aparecen en casillas adyacentes cuando no deberian durante la colocacion inicial

            GameObject thisPieza = Instantiate(planeta);
            thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
            thisPieza.GetComponent<Pieza>().jugador = 1;
            thisPieza.GetComponent<Pieza>().Colocar(Tablero.instance.mapa[rnd]);
        }
    }
}
