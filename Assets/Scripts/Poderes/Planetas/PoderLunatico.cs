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

    public override void InitialAction(bool pasar_turno = true)
    {
        base.InitialAction(false);
        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(jugador);

        foreach (Casilla c in planetas)
        {
            List<Casilla> posibles = new List<Casilla>();
            foreach (Casilla cc in c.adyacentes)
            {
                if (cc) posibles.Add(cc);
            }
            posibles = FiltroCasillas.CasillasSinMeteorito(posibles);
            int rnd = Random.Range(0, posibles.Count);
            GameObject thisPieza;
            if (PhotonNetwork.InRoom)
            {
                thisPieza = PhotonNetwork.Instantiate(luna.name, posibles[rnd].transform.position, Quaternion.identity);
            }
            else
            {
                thisPieza = Instantiate(luna, posibles[rnd].transform.position, Quaternion.identity);
            }
            thisPieza.GetComponent<Pieza>().Set_Jugador(jugador);
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = posibles[rnd];
            posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
        }

        if(pasar_turno) EventManager.TriggerEvent("AccionTerminadaConjunta");
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
            GameObject thisPieza;
            if (PhotonNetwork.InRoom)
            {
                thisPieza = PhotonNetwork.Instantiate(luna.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                thisPieza = Instantiate(luna, c.transform.position, Quaternion.identity);
            }
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
