using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono2Oyentes : PoderIABase
{
    [SerializeField] List<Pieza> planetas;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();        


        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in IATablero.instance.mapa)
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            if (c.pieza && c.pieza.GetComponent<Planetas>())
            {
                foreach(Pieza planeta in planetas)
                {
                    IATablero.instance.PrintInfoTablero(tabBase);

                    c.pieza = planeta;
                    planeta.casilla = c;

                    nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
                }
            }

        }

        return nuevosEstados;
    }
}
