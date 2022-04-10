using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderLunatico : PoderPlanetas
{
    public GameObject luna;

    bool setLuna = false;
    int numeroLunasPorFase = 1;
    int lunasPuestas = 0;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", PlaceLuna);        
    }

    public override void InitialAction(bool sin_pasar_turno = false)
    {
        base.InitialAction(true);
        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(faccion);

        foreach (Casilla c in planetas)
        {
            List<Casilla> posibles = new List<Casilla>();
            foreach (Casilla cc in c.adyacentes)
            {
                if (cc) posibles.Add(cc);
            }
            posibles = FiltroCasillas.CasillasSinMeteorito(FiltroCasillas.CasillasLibres(posibles));
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
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
            posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
        }

        if (!sin_pasar_turno)
        {
            Debug.Log("sin pasar turno lunatico paso turno");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }

    public override void FirstActionPersonal()
    {
        List<Casilla> casillasPosibles = luna.GetComponent<Pieza>().CasillasDisponibles();
        if(casillasPosibles.Count == 0)
        {
            Debug.Log("no hay hueco en el poder lunatico");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
        Debug.Log(casillasPosibles.Count);
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
        if(casillasPosibles.Count == 0)
        {
            Debug.Log("no hay hueco para poner liuna");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
            return;
        }
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
            thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
            thisPieza.GetComponent<Pieza>().casilla = c;
            c.pieza = thisPieza.GetComponent<Pieza>();
            setLuna = false;
            if (lunasPuestas < numeroLunasPorFase)
            {
                ++lunasPuestas;
                FirstActionPersonal();
                Debug.Log("lunas puestas: " + lunasPuestas);
                Debug.Log("LUNas maximas: " + numeroLunasPorFase);
            }
            else
            {
                lunasPuestas = 0;
                Tablero.instance.ResetCasillasEfects();
                Debug.Log("se acabo poner lunas");
                EventManager.TriggerEvent("AccionTerminadaConjunta");
            }
        }
    }

}
