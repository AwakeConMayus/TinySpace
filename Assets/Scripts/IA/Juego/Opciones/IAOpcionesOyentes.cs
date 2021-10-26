using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesOyentes : IAOpciones
{
    public override void Jugar()
    {
        int bestPuntuacion = -100;
        List<Casilla> bestMapa = new List<Casilla>();
        int bestOpcion = 0;

        for (int i = 0; i < 3; i++)
        {
            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            List<Casilla> newMapa = pieza.BestInmediateOpcion(Tablero.instance.mapa);
            int puntuacion = PiezaIA.Evaluar(newMapa, pieza.jugador);

            if(puntuacion > bestPuntuacion)
            {
                bestPuntuacion = puntuacion;
                bestMapa = newMapa;
                bestOpcion = i;
            }
        }

        IARotarOpcion(bestOpcion);
        ActualizarTablero(bestMapa);
    }
}
