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
    comodin,
    satelite
}
public enum Faccion
{
    none,
    minero,
    oyente
}


public abstract class Pieza : MonoBehaviour
{
    [SerializeField]
    public Faccion faccion;

    [HideInInspector]
    public Clase clase;

    [HideInInspector]
    public Casilla casilla;

    protected int jugador;

    private bool pieza_extra = false;

    private void Awake()
    {
        SetClase();

        if (this.gameObject.GetPhotonView().IsMine)
        {
            jugador = InstancePiezas.instance.jugador;
        }
        else
        {
            jugador = InstancePiezas.instance.jugadorEnemigo;
        }
    }

    public abstract int Puntos();

    public abstract List<Casilla> CasillasDisponibles(List<Casilla> referencia = null);

    protected abstract void SetClase();

    public virtual void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
        EventManager.TriggerEvent("UpdateScore");
        Debug.Log("La pieza es extra: " + (pieza_extra));
        if (this.gameObject.GetPhotonView().IsMine && !pieza_extra) Invoke("TerminarAccionConjunta", 1f);
        Set_Pieza_Extra(false);
    }

    void TerminarAccionConjunta()
    {
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    public virtual bool CompareClase(Clase compare)
    {
        if (clase == compare || clase == Clase.comodin) return true;
        else return false;
    }



    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Casilla>())
        {
            Colocar(other.gameObject.GetComponent<Casilla>());
            switch (faccion)
            {
                case Faccion.minero:
                    other.gameObject.GetComponent<Casilla>().SetState(States.minero);
                    break;
                case Faccion.oyente:
                    other.gameObject.GetComponent<Casilla>().SetState(States.oyente);
                    break;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Casilla>())
        {
            other.gameObject.GetComponent<Casilla>().pieza = null;
            other.gameObject.GetComponent<Casilla>().SetState(States.normal);
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

    public void Set_Pieza_Extra(bool extra = true)
    {
        pieza_extra = extra;
    }

    private void OnDestroy()
    {
        if (casilla)
        {
            casilla.Limpiar_Pieza(this);
        }  
    }
}