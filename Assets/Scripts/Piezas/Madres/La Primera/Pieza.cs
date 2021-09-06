using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Clase
{
    explorador,
    combate,
    investigador,
    estratega,
    astros,
    comodin
}

public enum Condiciones
{
    casillaVacia,
    AdyacentePlaneta
}

public abstract class Pieza : MonoBehaviour
{

    public Clase clase;
    public List<Condiciones> condiciones;

    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;

    public abstract int Puntos();

    public abstract List<Casilla> CasillasDisponibles();


    public virtual bool CompareClase(Clase compare)
    {
        if (clase == compare || compare == Clase.comodin) return true;
        else return false;
    }

}


