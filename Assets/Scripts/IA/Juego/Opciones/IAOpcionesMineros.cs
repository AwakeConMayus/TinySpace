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
        print("Nuevo Turno Minero");
        int bestPuntuacion = -100;
        InfoTablero bestMapa = new InfoTablero();
        int bestMineral = 0;
        int bestOpcion = 0;


        for (int i = 0; i < 3; i++)
        {
            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            int mineralGastar = 0;
            if (opcionesDisponibles[i] == 4) mineralGastar = opcionesIniciales[4].GetComponent<Especial>().coste;

            if (mineral >= mineralGastar)
            {
                InfoTablero newMapa = pieza.BestInmediateOpcion(new InfoTablero(Tablero.instance.mapa));
                IATablero.instance.PrintInfoTablero(newMapa);
                int puntuacion = PiezaIA.Evaluar(IATablero.instance.mapa, pieza.faccion);

                --puntuacion;

                if (puntuacion > bestPuntuacion)
                {
                    bestPuntuacion = puntuacion;
                    bestMapa = newMapa;
                    bestMineral = mineralGastar;
                    bestOpcion = i;
                }

                print("Minero Opcion: " + pieza.gameObject.name + "   -> " + puntuacion);
            }
        }

        if (mineral >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (opcionesDisponibles[i] == 4) continue;

                PiezaIA pieza = mejoras[opcionesDisponibles[i]].GetComponent<PiezaIA>();

                InfoTablero newMapa = pieza.BestInmediateOpcion(new InfoTablero(Tablero.instance.mapa));
                IATablero.instance.PrintInfoTablero(newMapa);
                int puntuacion = PiezaIA.Evaluar(IATablero.instance.mapa, pieza.faccion);

                --puntuacion;

                if (puntuacion > bestPuntuacion)
                {
                    bestPuntuacion = puntuacion;
                    bestMapa = newMapa;
                    bestMineral = 3;
                    bestOpcion = i;
                }

                print("Minero Opcion: " + pieza.gameObject.name + "   -> " + puntuacion);
            }
        }

        mineral -= bestMineral;
        IARotarOpcion(bestOpcion);
        ActualizarTablero(bestMapa);
    }

    public void AddMineral()
    {
        ++mineral;
    }
}
