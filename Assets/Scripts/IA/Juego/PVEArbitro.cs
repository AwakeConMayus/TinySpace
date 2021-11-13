using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEArbitro : MonoBehaviour
{
    [SerializeField] DatosIA datosIA;
    public IAOpciones[] opcionesIA = new IAOpciones[4];
    public Opciones[] opciones = new Opciones[4];

    [SerializeField] TuSeleccion mySeleccion, seleccioRival;

    Faccion player1, player2;

    IAOpciones jugador2;
    Opciones jugador1;



    bool active = true;
    bool specialActive = true;

    int numeroPoder = 0;
    int numeroPoder2 = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    bool end = false;
    bool initial = true;

    int turnoAbsoluto = -1;

    [SerializeField] bool SendOnline = true;


    private void Start()
    {
        player1 = mySeleccion.faccion;
        player2 = seleccioRival.faccion;

        jugador2 = opcionesIA[(int)player2 - 1];
        jugador1 = opciones[(int)player1 - 1];

        jugador1.gameObject.SetActive(true);



        InstancePiezas.instance.faccion = mySeleccion.faccion;

        jugador1.gameObject.SetActive(true);
        jugador1.opcionesIniciales = mySeleccion.mis_opciones;
        jugador1.poder = mySeleccion.mi_poder;
        jugador1.PrepararPreparacion();
        jugador2.poder = seleccioRival.mi_poder;
        jugador2.opcionesIniciales = seleccioRival.mis_opciones;
        jugador2.PrepararPreparacion();

        jugador1.Preparacion();
        jugador2.Preparacion();


        if(Random.Range(0,2) == 0)
        {
            initial = active = specialActive = false;
            jugador1.GetComponentInChildren<InterfazTurnos>().primero = false;
        }


        EventManager.StartListening("AccionTerminadaConjunta", NextTurn);
        NextTurn();

    }

    


    public void NextTurn()
    {
        if (turnoAbsoluto == 25) EndGame();
        if (end) return;
        if (specialPhase) SpecialTurn();
        else Turn();
    }


    void SpecialTurn()
    {
        ++turnoAbsoluto;
        EventManager.TriggerEvent("Siguiente_turno");
        SetActiveActive(false);

        Debug.Log("chambefull " + specialActive);
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
            switch (numeroPoder)
            {
                case 0:
                    jugador1.poder.GetComponent<Poder>().InitialAction();
                    break;
                case 1:
                    jugador1.poder.GetComponent<Poder>().FirstAction();
                    break;
                case 2:
                    jugador1.poder.GetComponent<Poder>().SecondAction();
                    break;
                default:
                    Debug.Log("el turno de poder no esta de acorde");
                    break;
            }
            ++numeroPoder;
        }
        else
        {
            Invoke("IASpecialTurn", 1);
        }
    }

    void Turn()
    {
        ++turnoAbsoluto;
        Debug.Log(turnoAbsoluto);
        EventManager.TriggerEvent("Siguiente_turno");
        

        if (!active && !end)
        {
            SetActiveActive(false);
            if ((turno + 1) % 10 == 0)
            {
                specialPhase = true;
            }
            if (++turno % 2 != 0)
            {
                active = !active;
            }

            Invoke("IATurn", 1);
        }
        else if (active)
        {
            SetActiveActive(true);
            if ((turno + 1) % 10 == 0)
            {
                specialPhase = true;
            }
            if (++turno % 2 != 0)
            {
                active = !active;
            }
        }
    }

    void IASpecialTurn()
    {        
        switch (numeroPoder2)
        {
            case 0:
                jugador2.poder.GetComponent<Poder>().InitialAction();
                break;
            default:
                jugador2.Jugar(jugador1, turnoAbsoluto);
                NextTurn();
                break;
        }
        ++numeroPoder2;       
    }

    void IATurn()
    {
        jugador2.Jugar(jugador1,turnoAbsoluto);

       NextTurn();
    }

    public void EndGame()
    {
        bool IAWin = false;
        if (Tablero.instance.Winner() == jugador2.faccion) IAWin = true;
        datosIA.AddData(IAWin);
        print("Wiiiiiin");
        Faccion initialF = Faccion.none;
        if (initial) initialF = jugador1.faccion;
        else initialF = jugador2.faccion;
        if (SendOnline) SendToGoogle.instance.SendOnline(initialF, true);
        end = true;
    }

    void SetActiveActive(bool b)
    {
        jugador1.SetActive(b);
    }
}

