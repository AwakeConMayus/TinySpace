using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderColono : Poder
{
    [SerializeField]
    GameObject planeta;
    [SerializeField]
    int planetasFase1 = 1;

    int planetasColocados = 0;

    bool SetPlaneta = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", CrearPieza);
    }
    

    public override void InitialAction()
    {
        for (int i = 0; i < 3; i++)
        {
            int rnd;
            do
            {
                rnd = Random.Range(0, Tablero.instance.mapa.Count);
            } while (!planeta.GetComponent<Pieza>().CasillasDisponibles().Contains(Tablero.instance.mapa[rnd]));

            GameObject thisPieza = Instantiate(planeta);
            thisPieza.transform.position = Tablero.instance.mapa[rnd].transform.position;
            thisPieza.GetComponent<Pieza>().Colocar(Tablero.instance.mapa[rnd]);
        }
    }


    public override void FirstAction()
    {
        List<Casilla> casillasPosibles = new List<Casilla>();
        planeta.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();

        foreach (Casilla casilla in casillasPosibles) ColorearCasillas.instance.reColor("green", casilla);       

        SetPlaneta = true;
    }

    public override void SecondAction() { }  


    
    void CrearPieza()
    {
        if (!SetPlaneta) return;
        Casilla c = ClickCasillas.casillaClick;
        List<Casilla> casillasPosibles = new List<Casilla>();
        casillasPosibles = planeta.GetComponent<Pieza>().CasillasDisponibles();
        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza = Instantiate(planeta);
            thisPieza.transform.position = c.transform.position;
            thisPieza.GetComponent<Pieza>().Colocar(c);

            foreach (Casilla casilla in Tablero.instance.mapa)
            {
                if (casilla.pieza && casilla.pieza.jugador != jugador) ColorearCasillas.instance.reColor("red", casilla);
                else ColorearCasillas.instance.initialColor(casilla);
            }
            SetPlaneta = false;
        }


        if (++planetasColocados < planetasFase1) FirstAction();
    }
}
