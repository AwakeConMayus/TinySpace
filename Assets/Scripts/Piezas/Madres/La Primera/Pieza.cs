using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


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

    protected int jugador;

    private void Awake()
    {
        SetClase();

        if (this.gameObject.GetPhotonView().IsMine)
        {
            jugador = InstancePiezas.instance.jugador;
            Debug.Log("se setea una pieza");
        }
        else
        {
            jugador = InstancePiezas.instance.jugadorEnemigo;
        }
    }

    public abstract int Puntos();

    public abstract List<Casilla> CasillasDisponibles();

    protected abstract void SetClase();

    public virtual void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
        if(this.gameObject.GetPhotonView().IsMine) EventManager.TriggerEvent("AccionTerminadaConjunta");
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

    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Casilla>())
        {
            other.gameObject.GetComponent<Casilla>().pieza = null;
        }
    }
    public void Set_Jugador(int _jugador)
    {
        jugador = _jugador;
    }
    public int Get_Jugador()
    {
        return jugador;
    }
}