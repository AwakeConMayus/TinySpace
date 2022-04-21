using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodLunaticoOyentes : PoderIABase
{
    public bool fase2 = false;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        if (!fase2) tabBase = PonerMejorPlaneta(tabBase);

        for (int i = 0; i < 1; i++)
        {
            tabBase = BestLuna(tabBase);
        }
        return OpcionificadorLuna(tabBase);
    }

    InfoTablero BestLuna (InfoTablero tabBase)
    {
        InfoTablero bestTab = new InfoTablero();
        GameObject luna = Resources.Load<GameObject>("Luna");
        int bestPuntos = int.MinValue;
        IATablero.instance.PrintInfoTablero(tabBase);
        foreach(Casilla c in luna.GetComponent<Pieza>().CasillasDisponibles(IATablero.instance.mapa))
        {
            GameObject g = Instantiate(luna);
            g.GetComponent<Pieza>().Set_Pieza_Extra();
            g.transform.position = c.transform.position;
            c.pieza = g.GetComponent<Pieza>();
            g.GetComponent<Pieza>().casilla = c;
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if(puntos > bestPuntos)
            {
                bestPuntos = puntos;
                bestTab = new InfoTablero(IATablero.instance.mapa);
            }
            c.pieza = null;
        }

        return bestTab;
    }

    List<InfoTablero> OpcionificadorLuna(InfoTablero tabBase)
    {
        List<InfoTablero> bestTab = new List<InfoTablero>();
        GameObject luna = Resources.Load<GameObject>("Luna");
        IATablero.instance.PrintInfoTablero(tabBase);
        foreach (Casilla c in luna.GetComponent<Pieza>().CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            GameObject g = Instantiate(luna);
            g.GetComponent<Pieza>().Set_Pieza_Extra();
            g.transform.position = c.transform.position;
            c.pieza = g.GetComponent<Pieza>();
            bestTab.Add(new InfoTablero(IATablero.instance.mapa));
        }

        return bestTab;
    }
}
