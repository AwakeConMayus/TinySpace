using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Auxiliar
{
    public static List<Casilla> Copy(List<Casilla> original)
    {
        List<Casilla> copy = new List<Casilla>();
        foreach(Casilla c in original)
        {
            copy.Add(c);
        }
        return copy;
    }
}
