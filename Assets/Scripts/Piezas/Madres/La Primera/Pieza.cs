using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

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
    agujeroNegro,
    restos
}
public enum Faccion
{
    none,
    minero,
    oyente
}


public abstract class Pieza : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    public Faccion faccion;

    [HideInInspector]
    public Clase clase;

    [HideInInspector]
    public Casilla casilla;

    [SerializeField]
    public bool astro;


    protected bool pieza_extra = false;

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
        if((this.gameObject.GetPhotonView().IsMine && !pieza_extra) || (!PhotonNetwork.InRoom && !pieza_extra)) Debug.Log(this.gameObject);
        if (this.gameObject.GetPhotonView().IsMine && !pieza_extra) Invoke("TerminarAccionConjunta", 1f);
        else if(!PhotonNetwork.InRoom && !pieza_extra) Invoke("TerminarAccionConjunta", 1f);
    }

    void TerminarAccionConjunta()
    {
        Debug.Log("termino con pieza " + this.gameObject.name);
        EventManager.TriggerEvent("AccionTerminadaConjunta");
    }

    List<Clase> clasesComodin = new List<Clase> { Clase.explorador, Clase.combate, Clase.investigador, Clase.estratega };

    public virtual bool CompareClase(Clase compare)
    {
        if (clase == compare) return true;
        if(clase == Clase.comodin)
        {
            if (clasesComodin.Contains(compare)) return true;
        }
        return false;
    }
    public virtual bool CompareClase(List<Clase> compare)
    {
        if (compare.Contains(clase)) return true;
        if(clase == Clase.comodin)
        {
            foreach(Clase c in compare)
            {
                if (clasesComodin.Contains(c)) return true;
            }
        }
        return false;
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
            Debug.Log("TRIGGER EXIT destruccion movimiento");
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
    public void OnPointerEnter()
    {
        Debug.Log("EL PUNTERO ME APUNTA SEGUNDO INTENTO" );
        this.GetComponent<Renderer>().sharedMaterial.SetInt("_outline", 1);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("EL PUNTERO ME APUNTA");
        this.GetComponent<Renderer>().sharedMaterial.SetInt("_outline", 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Renderer>().sharedMaterial.SetInt("_outline", 0);
    }
}