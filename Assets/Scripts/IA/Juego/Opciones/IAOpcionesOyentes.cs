using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesOyentes : IAOpciones
{
    public override void Jugar()
    {
        print("Nuevo Turno Oyente");

        int bestPuntuacion = -100;
        InfoTablero bestMapa = new InfoTablero();
        int bestOpcion = 0;

        for (int i = 0; i < 3; i++)
        {

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            InfoTablero newMapa = pieza.BestInmediateOpcion(new InfoTablero(Tablero.instance.mapa));
            IATablero.instance.PrintInfoTablero(newMapa);
            int puntuacion = PiezaIA.Evaluar(IATablero.instance.mapa, pieza.faccion);

            if(puntuacion > bestPuntuacion)
            {
                bestPuntuacion = puntuacion;
                bestMapa = newMapa;
                bestOpcion = i;
            }

            print("Oyente Opcion: " + pieza.gameObject.name + "   -> " + puntuacion);
        }

        IARotarOpcion(bestOpcion);
        ActualizarTablero(bestMapa);
    }
}
