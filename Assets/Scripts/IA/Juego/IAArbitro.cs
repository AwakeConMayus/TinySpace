using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAArbitro : MonoBehaviour
{
    public IAOpciones[] opciones = new IAOpciones[2];

    public Faccion player1, player2;

    IAOpciones jugador1, jugador2;

    

    bool active = true;
    bool specialActive = true;

    int numeroPoder1 = 0, numeroPoder2 = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    private void Start()
    {
        switch (player1)
        {
            case Faccion.minero:
                jugador1 = opciones[0];
                break;
            case Faccion.oyente:
                jugador1 = opciones[1];
                break;
        }
        switch (player2)
        {
            case Faccion.minero:
                jugador2 = opciones[0];
                break;
            case Faccion.oyente:
                jugador2 = opciones[1];
                break;
        }



        jugador1.jugador = 0;
        jugador2.jugador = 1;
        jugador1.Preparacion();
        jugador2.Preparacion();


        NextTurn();
        NextTurn();



    }

    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextTurn();
        }
    }


    public void NextTurn()
    {
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void SpecialTurn()
    {
        bool estaVezToca = specialActive;



        if (specialTurno % 2 != 0)
        {
            specialPhase = false;
        }
        if (++specialTurno % 2 != 0)
        {
            specialActive = !specialActive;
        }

        if (estaVezToca)
        {
            switch (numeroPoder1)
            {
                case 0:
                    jugador1.poder.GetComponent<Poder>().InitialAction();
                    break;
                case 1:
                    jugador1.JugarPoder(0);
                    break;
                case 2:
                    jugador1.JugarPoder(1);
                    break;
                default:
                    Debug.Log("el turno de poder no esta de acorde");
                    break;
            }
            ++numeroPoder1;
        }
        else
        {
            switch (numeroPoder2)
            {
                case 0:
                    jugador2.poder.GetComponent<Poder>().InitialAction();
                    break;
                case 1:
                    jugador2.JugarPoder(0);
                    break;
                case 2:
                    jugador2.JugarPoder(1);
                    break;
                default:
                    Debug.Log("el turno de poder no esta de acorde");
                    break;
            }
            ++numeroPoder2;
        }
    }

    void Turn()
    {
        if (turno == 20)
        {
            EndGame();
        }

        if (!active)
        {
            jugador2.Jugar();
        }
        else if (active)
        {
            jugador1.Jugar();
        }


        if ((turno + 1) % 10 == 0)
        {
            specialPhase = true;
        }
        if (++turno % 2 != 0)
        {
            active = !active;
        }
    }

    public void EndGame()
    {
        print("Wiiiiiin");
    }
}
