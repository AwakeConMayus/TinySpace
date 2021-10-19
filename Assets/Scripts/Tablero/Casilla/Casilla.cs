using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum States
{
    normal,
    select,
    holy,
    tpOut,
    tpIn,
    planeta,
    oyente,
    none,
    minero
}

public class Casilla : MonoBehaviour
{

    [HideInInspector]
    public Casilla[] adyacentes = new Casilla[6];

    public Pieza pieza;

    public bool meteorito = false;

    EfectosCasillas efectos;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        efectos = GetComponent<EfectosCasillas>();
    }

    public void Clear()
    {
            if(pieza) Destroy(pieza.gameObject);
    }
    
    public void Limpiar_Pieza(Pieza _pieza)
    {
        if(_pieza == pieza)
        {
            pieza = null;
            SetState(States.normal);
        }
    }
    public void SetState(States s)
    {
        switch (s)
        {
            case States.normal:
                anim.SetBool("Oyentes", false);
                anim.SetBool("Mineros", false);
                break;
            case States.none:
                anim.SetTrigger("Reset");
                break;
            case States.select:
                anim.SetTrigger("Select");
                break;
            case States.holy:
                efectos.Holy();
                break;
            case States.tpOut:
                efectos.TPOut();
                break;
            case States.tpIn:
                efectos.TPIn();
                break;
            case States.planeta:
                anim.SetTrigger("Planeta");
                break;
            case States.oyente:
                anim.SetBool("Mineros", false);
                anim.SetBool("Oyentes", true);
                break;
            case States.minero: 
                anim.SetBool("Oyentes", false);
                anim.SetBool("Mineros", true);
                break;
        }
    }
}
