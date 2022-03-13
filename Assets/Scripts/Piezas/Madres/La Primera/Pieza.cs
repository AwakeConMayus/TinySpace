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
    planeta,
    comodin,
    satelite,
    luna,
    agujeroNegro
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

    [SerializeField]
    public bool astro;


    private bool pieza_extra = false;

    private void Awake()
    {
        SetClase();        
    }

    public abstract int Puntos();

    public abstract List<Casilla> CasillasDisponibles(List<Casilla> referencia = null);

    protected abstract void SetClase();

    public virtual void Colocar(Casilla c)
    {
        casilla = c;
        casilla.pieza = this;
        EventManager.TriggerEvent("UpdateScore");
        if (this.gameObject.GetPhotonView().IsMine && !pieza_extra) Invoke("TerminarAccionConjunta", 1f);
        else if(!PhotonNetwork.InRoom && !pieza_extra) Invoke("TerminarAccionConjunta", 1f);
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
    public virtual bool CompareClase(List<Clase> compare)
    {
        if (compare.Contains(clase) || clase == Clase.comodin) return true;
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
            other.gameObject.GetComponent<Casilla>().Limpiar_Pieza(this);
        }
    }
   

    public void Set_Pieza_Extra(bool extra = true)
    {
        pieza_extra = extra;
    }

    public void SelfDestruction()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (casilla)
        {
            casilla.Limpiar_Pieza(this);
        }  
    }
}