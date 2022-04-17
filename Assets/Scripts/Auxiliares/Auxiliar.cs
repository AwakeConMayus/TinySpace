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

    public static string StringListInt(List<int> list)
    {
        string s = "[";
        for (int i = 0; i < list.Count; i++)
        {
            s += list[i];
            if (i < list.Count - 1) s += ", ";
        }
        s += "]";
        return s;
    }

    public static string StringArrayInt(int[] list)
    {
        string s = "[";
        for (int i = 0; i < list.Length; i++)
        {
            s += list[i];
            if (i < list.Length - 1) s += ", ";
        }
        s += "]";
        return s;
    }
}
