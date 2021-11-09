using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAArbitro : MonoBehaviour
{
    public IAOpciones[] opciones = new IAOpciones[2];

    public Faccion player1, player2;

    IAOpciones jugador1, jugador2;

    public DatosIA datos;

    

    bool active = true;
    bool specialActive = true;

    int numeroPoder1 = 0, numeroPoder2 = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    int turnoAbsoluto = 0;

    bool end = false;

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
        if (end) return;
        if (specialPhase) SpecialTurn();
        else Turn();
        ++turnoAbsoluto;
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
                default:
                    jugador1.Jugar(jugador2, turnoAbsoluto);
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
                default:
                    jugador2.Jugar(jugador1, turnoAbsoluto);
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
            jugador2.Jugar(jugador1,turnoAbsoluto);
        }
        else if (active)
        {
            jugador1.Jugar(jugador2,turnoAbsoluto);
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
        end = true;
    }
}
