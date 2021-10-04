using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Clase
{
    none,
    explorador,
    combate,
    investigador,
    estratega,
    astros,
    comodin
}


public abstract class Pieza : MonoBehaviour
{
    [HideInInspector]
    public Clase clase;

    [HideInInspector]
    public Casilla casilla;
    [HideInInspector]
    public int jugador;

    private void Awake()
    {
        SetClase();
    }

    public abstract int Puntos();

    public abstract List<Casilla> CasillasDisponibles();

    protected abstract void SetClase();

    public virtual void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
    }

    public virtual bool CompareClase(Clase compare)
    {
        if (clase == compare || compare == Clase.comodin) return true;
        else return false;
    }



    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Casilla>())
        {
            Colocar(other.gameObject.GetComponent<Casilla>());
        }
    }
}