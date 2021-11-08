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

    protected InfoTablero PonerMejorPlaneta(InfoTablero tabBase)
    {
        PiezaIA piezaReferencia = Resources.Load<PiezaIA>("Planeta Planetarios");

        IATablero.instance.PrintInfoTablero(tabBase);

        tabBase = piezaReferencia.BestInmediateOpcion(tabBase);

        return tabBase;
    }
}
