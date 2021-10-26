using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesMineros : IAOpciones
{
    public GameObject[] mejoras = new GameObject[4];
    public int mineral = 5;

    private void Start()
    {
        EventManager.StartListening("RecogerMineral", AddMineral);
    }

    public override void Jugar()
    {
        int bestPuntuacion = -100;
        List<Casilla> bestMapa = new List<Casilla>();
        int bestMineral = 0;
        int bestOpcion = 0;


        for (int i = 0; i < 3; i++)
        {
            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            int mineralGastar = 0;
            if (opcionesDisponibles[i] == 4) mineralGastar = opcionesIniciales[4].GetComponent<Especial>().coste;

            if (mineral >= mineralGastar)
            {
                List<Casilla> newMapa = pieza.BestInmediateOpcion(Tablero.instance.mapa);
                int puntuacion = PiezaIA.Evaluar(newMapa, pieza.jugador);

                if (puntuacion > bestPuntuacion)
                {
                    bestPuntuacion = puntuacion;
                    bestMapa = newMapa;
                    bestMineral = mineralGastar;
                    bestOpcion = i;
                }
            }
        }

        if (mineral >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (opcionesDisponibles[i] == 4) continue;

                PiezaIA pieza = mejoras[opcionesDisponibles[i]].GetComponent<PiezaIA>();

                List<Casilla> newMapa = pieza.BestInmediateOpcion(Tablero.instance.mapa);
                int puntuacion = PiezaIA.Evaluar(newMapa, pieza.jugador);

                if (puntuacion > bestPuntuacion)
                {
                    bestPuntuacion = puntuacion;
                    bestMapa = newMapa;
                    bestMineral = 3;
                    bestOpcion = i;
                }
            }
        }

        IARotarOpcion(bestOpcion);
        ActualizarTablero(bestMapa);
    }

    public void AddMineral()
    {
        ++mineral;
    }
}
