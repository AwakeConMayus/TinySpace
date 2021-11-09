using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderChantajista2 : PoderMineros
{
    List<Casilla> casillasDisponibles = new List<Casilla>();

    bool faseSeleccion1 = false, faseSeleccion2 = false;

    GameObject Pieza1, Pieza2;

    private void Start()
    {
        EventManager.StartListening("ClickCasilla", Seleccion1);
    }

    public override void FirstAction()
    {
        casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion);
        casillasDisponibles = FiltroCasillas.CasillasAdyacentes(casillasDisponibles,true);
        casillasDisponibles = FiltroCasillas.CasillasDeOtroJugador(faccion, casillasDisponibles);

        foreach(Casilla c in casillasDisponibles)
        {
            c.SetState(States.select);
        }

        faseSeleccion1 = true;
    }

    public override void SecondAction()
    {
        FirstAction();
    }

    void Seleccion1()
    {
        if (!faseSeleccion1) return;

        Casilla elegida = ClickCasillas.casillaClick;
        if (!casillasDisponibles.Contains(elegida)) return;

        faseSeleccion1 = false;
        Pieza1 = elegida.pieza.gameObject;
        Tablero.instance.ResetCasillasEfects();

        casillasDisponibles = FiltroCasillas.CasillasDeUnJugador(faccion);
        foreach (Casilla c in casillasDisponibles)
        {
            c.SetState(States.select);
        }

        faseSeleccion2 = true;
    }

    void Seleccion2()
    {
        if (!faseSeleccion2) return;

        Casilla elegida = ClickCasillas.casillaClick;
        if (!casillasDisponibles.Contains(elegida)) return;

        faseSeleccion2 = false;
        Pieza2 = elegida.pieza.gameObject;

        Intercambio();

        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }


    //Esto hay que adaptarlo al online
    void Intercambio()
    {
        Vector3 aux = Pieza1.transform.position;

        Pieza1.GetComponent<Pieza>().casilla.SetState(States.tpOut);
        Pieza2.GetComponent<Pieza>().casilla.SetState(States.tpOut);

        Pieza1.transform.position = Pieza2.transform.position;
        Pieza2.transform.position = aux;

        Tablero.instance.ResetCasillasEfects(); //No se si esto hace falta lo pongo por si acaso
    }
}
