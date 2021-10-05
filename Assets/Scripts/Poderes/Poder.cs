using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Poder : MonoBehaviourPunCallbacks
{
    public int jugador;
    public abstract void InitialAction();
    public abstract void FirstAction();
    public abstract void SecondAction();
}
