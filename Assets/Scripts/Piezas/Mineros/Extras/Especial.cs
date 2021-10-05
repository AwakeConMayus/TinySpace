using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Especial : Pieza
{
    public int coste;
}

public abstract class EfectoEspecial : Especial
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
