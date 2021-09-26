using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Efecto : Pieza
{

    public abstract void Accion();

    protected override void SetClase()
    {
        clase = Clase.none;
    }


    public override int Puntos()
    {
        return 0;
    }

    public override void Colocar(Casilla c)
    {
        casilla = c;
        Accion();
        Destroy(this.gameObject);
    }
}
