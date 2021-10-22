using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderIA : MonoBehaviour
{
    public List<PoderIABase> Fases;
}

public abstract class PoderIABase : PiezaIA
{
    public Opciones padre;
}
