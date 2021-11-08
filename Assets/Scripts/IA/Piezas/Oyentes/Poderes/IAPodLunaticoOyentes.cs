using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodLunaticoOyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        for (int i = 0; i < 2; i++)
        {
            tabBase = BestLuna(tabBase);
        }
        return OpcionificadorLuna(tabBase);
    }

    InfoTablero BestLuna (InfoTablero tabBase)
    {
        InfoTablero bestTab = new InfoTablero();
        Pieza luna = Resources.Load<Pieza>("Luna");
        int bestPuntos = int.MinValue;
        IATablero.instance.PrintInfoTablero(tabBase);
        foreach(Casilla c in luna.CasillasDisponibles(IATablero.instance.mapa))
        {
            c.pieza = luna;
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
        Pieza luna = Resources.Load<Pieza>("Luna");
        IATablero.instance.PrintInfoTablero(tabBase);
        foreach (Casilla c in luna.CasillasDisponibles(IATablero.instance.mapa))
        {
            c.pieza = luna;
            bestTab.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        return bestTab;
    }
}
