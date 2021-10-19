using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderLunatico : PoderPlanetas
{
    public GameObject luna;

    bool setLuna = false;
    int numeroLunasPorFase = 3;
    int lunasPuestas = 0;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", PlaceLuna);        
    }

    public override void InitialAction()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
        planeta = Resources.Load<GameObject>("Planeta Planetarios");

        Casilla planetaReferencia = null;

        for (int i = 0; i < 3; i++)
        {
            List<Casilla> casillasPosibles = FiltroCasillas.CasillasSinMeteorito(planeta.GetComponent<Pieza>().CasillasDisponibles());
            if (i == 1)
            {
                casillasPosibles = FiltroCasillas.CasillasAdyacentes(planetaReferencia, true);
                casillasPosibles = FiltroCasillas.CasillasAdyacentes(casillasPosibles, true);
                casillasPosibles = FiltroCasillas.CasillasSinMeteorito(casillasPosibles);
                casillasPosibles = FiltroCasillas.Interseccion(casillasPosibles, planeta.GetComponent<Pieza>().CasillasDisponibles());
            }
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!casillasPosibles.Contains(Tablero.instance.mapa[rnd]));

            if (i == 0) planetaReferencia = Tablero.instance.mapa[rnd];

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                if (!planeta) planeta = Resources.Load<GameObject>("Planeta Planetarios");
                GameObject thisPieza = PhotonNetwork.Instantiate(planeta.name, Tablero.instance.mapa[rnd].transform.position, Quaternion.identity);
                thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                thisPieza.GetComponent<Pieza>().casilla = Tablero.instance.mapa[rnd];
                Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();

            }
            else
            {
                GameObject thisPieza = Instantiate(planeta);
                thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
                thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
                Tablero.instance.mapa[rnd].pieza = thisPieza.GetComponent<Pieza>();
            }
        }
        Tablero.instance.ResetCasillasEfects();

        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(jugador);

        foreach (Casilla c in planetas)
        {
            List<Casilla> posibles = new List<Casilla>();
            foreach(Casilla cc in c.adyacentes)
            {
                if (cc) posibles.Add(cc);
            }
            posibles = FiltroCasillas.CasillasSinMeteorito(posibles);
            int rnd = Random.Range(0, posibles.Count);

            GameObject thisPieza = PhotonNetwork.Instantiate(luna.name, posibles[rnd].transform.position, Quaternion.identity);
            thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = posibles[rnd];
            posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
        }

        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public override void FirstActionPersonal()
    {
        List<Casilla> casillasPosibles = luna.GetComponent<Pieza>().CasillasDisponibles();

        Tablero.instance.ResetCasillasEfects();
        foreach (Casilla casilla in casillasPosibles) casilla.SetState(States.select);

        setLuna = true;
    }
    public override void SecondAction()
    {
        lunasPuestas = 0;
        FirstActionPersonal();
    }

    void PlaceLuna()
    {
        if (!setLuna) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = luna.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza = PhotonNetwork.Instantiate(luna.name, c.transform.position, Quaternion.identity);
            thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = c;
            c.pieza = thisPieza.GetComponent<Pieza>();
            setLuna = false;
            if (lunasPuestas < numeroLunasPorFase)
            {
                ++lunasPuestas;
                FirstActionPersonal();
            }
        }
    }

}
