using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEArbitro : MonoBehaviour
{
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


    private void Start()
    {
        player1 = mySeleccion.faccion;
        player2 = seleccioRival.faccion;

        jugador2 = opcionesIA[(int)player2 - 1];
        jugador1 = opciones[(int)player1 - 1];

        jugador1.gameObject.SetActive(true);

        jugador1.Preparacion();
        jugador2.Preparacion();


        InstancePiezas.instance.faccion = mySeleccion.faccion;
        jugador1.gameObject.SetActive(true);
        jugador1.opcionesIniciales = mySeleccion.mis_opciones;
        jugador1.poder = mySeleccion.mi_poder;
        jugador1.GetComponentInChildren<InterfazTurnos>().primero = true;
        jugador1.PrepararPreparacion();

        jugador2.opcionesIniciales = seleccioRival.mis_opciones;
        jugador2.poder = seleccioRival.mi_poder;


        if(Random.Range(0,2) == 0)
        {
            initial = active = specialActive = false;
        }

        NextTurn();
        NextTurn();

        EventManager.StartListening("AccionTerminadaConjunta", NextTurn);
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
    }


    void SpecialTurn()
    {
        SetActiveActive(false);


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
            IASpecialTurn();
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
            SetActiveActive(false);
            IATurn();
        }
        else if (active)
        {
            SetActiveActive(true);
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

    void IASpecialTurn()
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

    void IATurn()
    {
        if (turno == 20)
        {
            EndGame();
        }

        jugador2.Jugar();
    }

    public void EndGame()
    {
        print("Wiiiiiin");
        end = true;
    }

    void SetActiveActive(bool b)
    {
        jugador1.SetActive(b);
    }
}

