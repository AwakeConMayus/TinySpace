using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    normal,
    select,
    holy,
    tpOut,
    tpIn,
    planeta
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
        if (pieza)
        {
            Destroy(pieza.gameObject);
            pieza = null;
        }
    }

    public void SetState(States s)
    {
        switch (s)
        {
            case States.normal:
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
        }
    }
  

}
