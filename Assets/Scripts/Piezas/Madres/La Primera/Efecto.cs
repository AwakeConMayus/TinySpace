using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Efecto : MonoBehaviour
{

    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;

    public abstract void Accion();

    public abstract List<Casilla> CasillasDisponibles();

    public void Colocar(Casilla c)
    {
        casilla = c;
        Accion();
        Destroy(this.gameObject);
    }
}
