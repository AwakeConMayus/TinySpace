using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTurnos : MonoBehaviour
{
    int turno = 1;
    int turnoAux = 1;
    bool player = false;

    //false = Player 1, true = Player 2

    private void Start()
    {
        EventManager.StartListening("Jugada", realizarJugada);
    }

    void realizarJugada()
    {
        if (turnoAux % 2 == 0) player = !player;

        Debug.Log("Ha jugado Player = " + player);
        
        ++turnoAux;

        //Debug.Log("TurnoAux = " + turnoAux);

        Debug.Log("Turno = " + turno);

        if (turnoAux == 3)
        {
            ++turno;
            turnoAux = 1;
        }
    }


}
