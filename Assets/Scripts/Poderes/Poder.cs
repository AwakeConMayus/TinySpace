using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Poder : MonoBehaviour
{
    public int jugador;
    public abstract void InitialAction();
    public abstract void FirstAction();
    public abstract void SecondAction();
}
