using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum estados
{
    SelectPieza,
    SelectCasilla    
}

public class TestingInstance : MonoBehaviour
{
    public GameObject pieza;
    public Casilla casilla;
    public int jugador;

    estados estado = estados.SelectPieza;

    List<Casilla> casillasPosibles = new List<Casilla>();

    public void SetPieza(GameObject nave)
    {
        pieza = nave;
        pieza.GetComponent<Pieza>().jugador = jugador;
        casillasPosibles = nave.GetComponent<Pieza>().CasillasDisponibles();

        estado = estados.SelectCasilla;
    }

    public void CrearPieza(Casilla c)
    {
        if (estado == estados.SelectPieza) return;

        if (casillasPosibles.Contains(c))
        {
            GameObject thisPieza = Instantiate(pieza);
            thisPieza.transform.position = c.transform.position;
            thisPieza.GetComponent<Pieza>().Colocar(c);

            estado = estados.SelectPieza;
        }
    }
}
