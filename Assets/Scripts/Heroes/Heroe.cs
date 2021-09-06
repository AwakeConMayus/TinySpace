using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faccion
{
    minero,
    planetario,
    honorable,
    simbionte
}

public abstract class Heroe
{
    public Faccion faccion;
    public int turno;

    public abstract void Praparación();
    public abstract void Poder();
}
