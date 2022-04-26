using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEArbitro : MonoBehaviour
{
    [SerializeField] bool vectorAleatorio;
    [SerializeField] DatosIA datosIA;
    public IAOpciones[] opcionesIA = new IAOpciones[4];
    public Opciones[] opciones = new Opciones[4];

    [SerializeField] TuSeleccion mySeleccion, seleccioRival;

    [SerializeField] GameObject IconoPensando;

    Faccion player1, player2;

    IAOpciones jugador2;
    Opciones jugador1;

    int ordenPoderInicial = 0;

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
    [SerializeField] GameObject MenuFinalGame;
    [SerializeField] bool SendOnline = true;

    List<int[]> vectores = new List<int[]>();

    int[] vector = { 1, 0, 2, 0, 0 };
    int[] vector2 = { 0, 2, 1 };
    private void Start()
    {
        Terraformar.Reset();
        player1 = mySeleccion.faccion;
        player2 = seleccioRival.faccion;

        jugador2 = opcionesIA[(int)player2 - 1];
        jugador1 = opciones[(int)player1 - 1];

        jugador1.gameObject.SetActive(true);

        if(Random.Range(0,2) == 0)
        {
            initial = active = specialActive = false;
            jugador1.GetComponentInChildren<InterfazTurnos>().primero = false;
        }
        if (vectorAleatorio) 
        {
            RandomizarVectores();
        }
        else
        {
            vector = null;
            vector2 = null;
        }


        InstancePiezas.instance.faccion = mySeleccion.faccion;

        jugador1.gameObject.SetActive(true);
        for(int i = 0; i < mySeleccion.mis_opciones.Length; ++i)
        {
            jugador1.opcionesIniciales[i] = mySeleccion.mis_opciones[i];
        }
        jugador1.poder = mySeleccion.mi_poder;
        jugador1.PrepararPreparacion();


        jugador2.poder = seleccioRival.mi_poder;
        for (int i = 0; i < seleccioRival.mis_opciones.Length; ++i)
        {
            jugador2.opcionesIniciales[i] = seleccioRival.mis_opciones[i];
        }
        jugador2.PrepararPreparacion();

        jugador1.Preparacion();
        jugador2.Preparacion();




        EventManager.StartListening("AccionTerminadaConjunta", NextTurn);

        

        NextTurn();

    }
    public void RandomizarVectores()
    {
        if ((active == true && mySeleccion.faccion == Faccion.oyente) || (!active && seleccioRival.faccion == Faccion.oyente) )
        {
            vector[0] = 0;
            vector2[0] = 1;
        }
        else
        {
            vector[0] = 1;
            vector2[0] = 0;
        }
            vector[1] = Random.Range(0, 2);
        vector[2] = Random.Range(0, 3);
        vector[3] = Random.Range(0, 3);
        vector[4] = Random.Range(0, 2);

        vector2[1] = Random.Range(0, 3);
        vector2[2] = Random.Range(0, 2);

        Debug.Log("vector[" + vector[0] + "," + vector[1] + "," + vector[2] + "," + vector[3] + "," + vector[4] + "]");
        Debug.Log("vectorM[" + vector2[0]+  "," + vector2[1] + "," + vector2[2] + "]");

        if (vector[0] == 0)
        {
            vectores.Add(vector);
            vectores.Add(vector2);
        }
        else
        {
            vectores.Add(vector2);
            vectores.Add(vector);
        }
    }
    public void InicialicacionDePoderes()
    {
        Debug.Log("TURNO ABSOLUTO " + turnoAbsoluto + " orden poder inicial " + ordenPoderInicial);
        //Cuando haya mas de dos facciones se pone aqui un do while que vaya avanzando el orden hasta que encuentra una con un orden igual a orden poderInicial
        //el orden puede ser el mismo enumerador u algo que guarde el script opcciones
        if (ordenPoderInicial == 0)
        {

            ++ordenPoderInicial;

            if (mySeleccion.faccion == Faccion.oyente)
            {
                jugador1.poder.GetComponent<Poder>().InitialAction(false, vector);
                jugador1.HandicapDeMano(vector[3]);
            }
            else if(seleccioRival.faccion == Faccion.oyente)
            {
                jugador2.poder.GetComponent<Poder>().InitialAction(false, vector);
                jugador2.HandicapDeMano(vector[3]);
            }
        }
        else if(ordenPoderInicial == 1) 
        {
            ++ordenPoderInicial;
            if (mySeleccion.faccion == Faccion.minero)
            {
                jugador1.poder.GetComponent<Poder>().InitialAction(false, vector2);
                jugador1.HandicapDeMano(vector2[2]);
            }
            else if (seleccioRival.faccion == Faccion.minero)
            {
                jugador2.poder.GetComponent<Poder>().InitialAction(false, vector2);
                jugador2.HandicapDeMano(vector2[2]);
            }

        }
    }


    public void NextTurn()
    {
        IconoPensando.SetActive(false);
        if (end) return;
        

        
        if (turnoAbsoluto == 25) EndGame();
        else if (specialPhase) SpecialTurn();
        else Turn();
    }

    void Vectorizacion()
    {
        //Vectorizacion
        if (initial) vectores = Vectorizador.Vectorizar(Tablero.instance.mapa, jugador1, jugador2);
        else vectores = Vectorizador.Vectorizar(Tablero.instance.mapa, jugador2, jugador1);

        print(Auxiliar.StringArrayInt(vectores[0]) + " // " + Auxiliar.StringArrayInt(vectores[1]));
    }

    void SpecialTurn()
    {
        
        Debug.Log(turnoAbsoluto + " " + specialActive + " v turno " + turno + " special " + specialTurno);
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

                    //if (mySeleccion.faccion == Faccion.minero) ;
                    InicialicacionDePoderes();
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
        
        Debug.Log(turnoAbsoluto + " turno" + " v turno' " + turno);
        ++turnoAbsoluto;
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
        IconoPensando.SetActive(true);
        switch (numeroPoder2)
        {
            case 0:
                InicialicacionDePoderes();
                break;
            default:
                StartCoroutine( jugador2.Jugar(jugador1, turnoAbsoluto));
                break;
        }
        ++numeroPoder2;       
    }

    void IATurn()
    {
        IconoPensando.SetActive(true);
        StartCoroutine( jugador2.Jugar(jugador1,turnoAbsoluto));
    }

    public void EndGame()
    {
        end = true;
        bool IAWin = false;
        MenuFinalGame.SetActive(true);
        MenuFinalGame.GetComponent<MenuFinalParitda>().Final_Partida(  new int[2]);
        if (Tablero.instance.Winner() == jugador2.faccion) IAWin = true;
        datosIA.AddData(IAWin);
        Faccion initialF = Faccion.none;
        if (initial) initialF = jugador1.faccion;
        else initialF = jugador2.faccion;
        if (SendOnline) SendToGoogle.instance.SendOnline(initialF, "PVE", vectores);
    }

    void SetActiveActive(bool b)
    {
        jugador1.SetActive(b);
    }
}

