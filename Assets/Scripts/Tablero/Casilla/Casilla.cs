﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{

    [HideInInspector]
    public Casilla[] adyacentes = new Casilla[6];
    [HideInInspector]
    public Pieza pieza;

    public void Clear()
    {
        if (pieza)
        {
            Destroy(pieza.gameObject);
            pieza = null;
        }
    }
}
