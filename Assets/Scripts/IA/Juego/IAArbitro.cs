using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IAArbitro : MonoBehaviour
{
    public IAOpciones[] opciones = new IAOpciones[2];

    IAOpciones jugador1, jugador2;

    public DatosIA datos;

    [SerializeField] OpcionesFaccion minerosPosiblesOpciones, oyentesPosiblesOpciones;
    [SerializeField] TuSeleccion seleccion1, seleccion2;

    [SerializeField] bool SendOnline = true;

    int player1Faccion, player2Faccion;

    bool active = true;
    bool specialActive = true;

    int numeroPoder1 = 0, numeroPoder2 = 0;

    bool specialPhase = true;
    int specialTurno = 0;
    int turno = 0;

    int turnoAbsoluto = -1;

    bool end = false;

    List<int[]> vectores = new List<int[]>();

    [SerializeField] bool indicadoresPuntos;

    private void Start()
    {
        Terraformar.Reset();

        if (indicadoresPuntos && !GetComponent<BotonIndicadorPuntos>().GetActive()) GetComponent<BotonIndicadorPuntos>().Click();


        player1Faccion = Random.Range(0, opciones.Length);
        do
        {
            player2Faccion = Random.Range(0, opciones.Length);
        } while (player1Faccion == player2Faccion);

        PrepararOpcionesIA();

        jugador1 = opciones[player1Faccion];
        jugador2 = opciones[player2Faccion];



        jugador1.PrepararPreparacion();
        jugador2.PrepararPreparacion();
        jugador1.Preparacion();
        jugador2.Preparacion();

        EventManager.StartListening("AccionTerminadaConjunta", TurnoTerminado);

        NextTurn();       
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) NextTurn();
    }

    void PrepararOpcionesIA()
    {
        if(player1Faccion == 0 || player2Faccion == 0)
        {
            if(player1Faccion == 1 || player2Faccion == 1)
            {
                //player 1
                opciones[0].poder = minerosPosiblesOpciones.posibles_Poders[datos.Mineros.vsOyentes.BestHeroe(100)];
                opciones[0].opcionesIniciales[4] = minerosPosiblesOpciones.posibles_Piezas_Especiales[datos.Mineros.vsOyentes.BestEspecial(100)];

                seleccion1.mi_poder = opciones[0].poder;
                seleccion1.mis_opciones = opciones[0].opcionesIniciales;
                

                //player 2
                opciones[1].poder = oyentesPosiblesOpciones.posibles_Poders[datos.Oyentes.vsMineros.BestHeroe(100)];
                opciones[1].opcionesIniciales[4] = oyentesPosiblesOpciones.posibles_Piezas_Especiales[datos.Oyentes.vsMineros.BestEspecial(100)];
                int mejora = datos.Oyentes.vsMineros.BestMejora(100);
                opciones[1].opcionesIniciales[oyentesPosiblesOpciones.huecos_Especializadas[mejora]] = oyentesPosiblesOpciones.posibles_Piezas_Especializadas[mejora];

                seleccion2.mi_poder = opciones[1].poder;
                seleccion2.mis_opciones = opciones[1].opcionesIniciales;
            }
        }        
    }

    public void TurnoTerminado()
    {
        Invoke(nameof(NextTurn), 1f);
    }

    public void NextTurn()
    {
        ++turnoAbsoluto;
        print(turnoAbsoluto);
        if (turnoAbsoluto == 2) Vectorizacion();

        if (turnoAbsoluto == 26) EndGame();
        if (end) return;
        if (specialPhase) SpecialTurn();
        else Turn();
    }

    void Vectorizacion()
    {
        //Vectorizacion
        vectores = Vectorizador.Vectorizar(Tablero.instance.mapa, jugador1, jugador2);

        print(Auxiliar.StringArrayInt(vectores[0]) + " // " + Auxiliar.StringArrayInt(vectores[1]));
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
                    StartCoroutine(jugador1.Jugar(jugador2, turnoAbsoluto));
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
                    StartCoroutine(jugador2.Jugar(jugador1, turnoAbsoluto));
                    break;
            }
            ++numeroPoder2;
        }
    }

    void Turn()
    {

        if (!active)
        {
            StartCoroutine(jugador2.Jugar(jugador1,turnoAbsoluto));
        }
        else
        {
            StartCoroutine(jugador1.Jugar(jugador2,turnoAbsoluto));
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
        int[] puntos = Tablero.instance.RecuentoPuntos();
        print(jugador1.faccion + " / " + Auxiliar.StringArrayInt(puntos) + " / " + jugador2.faccion);

        int bestPuntos = int.MinValue;
        int winner = -1;

        {
            for (int i = 0; i < puntos.Length; i++)
            {
                if (puntos[i] == bestPuntos) winner = -1;
                else if (puntos[i] > bestPuntos)
                {
                    bestPuntos = puntos[i];
                    winner = i;
                }
            }
        }

        bool ganaPlayer2 = jugador2.faccion == (Faccion)winner;

        datos.AddData(ganaPlayer2, true);

        if (SendOnline) SendToGoogle.instance.SendOnline(jugador1.faccion, true, vectores);
        end = true;
        Invoke(nameof(Reset), 3f);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
