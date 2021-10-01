using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTurnos : MonoBehaviour
{
    private int turno = 1;
    private int turnoAux = 1;

    //* Jugador actual, false = Player 1, true = Player 2
    private bool player    = false;
    private int  playerNum = 1;

    public static GestorTurnos instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    //private void Start()
    //{
    //    EventManager.StartListening("realizarJugada", realizarJugada);
    //    EventManager.StartListening("resetTurnos", resetTurnos);
    //}

    public void realizarJugada()
    {
        //* Si turnoAux es par, se invierte el jugador actual
        if (turnoAux % 2 == 0) player = !player;

        //* Obtiene un valor 1 o 2 del bool player para tener un número que darle a pieza.jugador
        if (!player) playerNum = 1;
        else playerNum = 2;

        Debug.Log("Ha jugado Player " + playerNum);
        
        ++turnoAux;
       
        Debug.Log("Turno Actual " + turno + " - Turno Auxiliar " + turnoAux);

        //* Si turnoAux llega a 3, vuelve a ser 1 y se avanza al siguiente turno
        if (turnoAux == 3)
        {
            ++turno;
            turnoAux = 1;
        }
    }

    public void resetTurnos()
    {
        //* Vuelve todo al estado incial
        turno = 1;
        turnoAux = 1;
        player = false;
        playerNum = 1;
    }

    //* Tiene la comprobación de lógica que tiene realizar jugada para obtener el número de player de forma correcta
    public int getPlayer() 
    {
        int turnoAuxCopy = turnoAux;
        bool playerCopy = player;
        if (turnoAuxCopy % 2 == 0) playerCopy = !playerCopy;

        if (!playerCopy) return 1;
        else return 2;
    }
    public int getTurno()  { return turno; }

}
