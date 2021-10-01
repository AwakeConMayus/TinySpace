using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTurnos : MonoBehaviour
{
    int turno = 1;
    int turnoAux = 1;

    //* Jugador actual, false = Player 1, true = Player 2
    bool player = false;

    private void Start()
    {
        EventManager.StartListening("Jugada", realizarJugada);
    }

    void realizarJugada()
    {
        //* Si turnoAux es par, se invierte el jugador actual
        if (turnoAux % 2 == 0) player = !player;

        Debug.Log("Ha jugado Player = " + player);
        
        ++turnoAux;

        Debug.Log("Turno = " + turno);

        //* Si turnoAux llega a 3, vuelve a ser 1 y se avanza al siguiente turno
        if (turnoAux == 3)
        {
            ++turno;
            turnoAux = 1;
        }
    }


}
