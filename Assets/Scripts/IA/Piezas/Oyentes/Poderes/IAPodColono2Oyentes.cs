using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPodColono2Oyentes : PoderIABase
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        List<Pieza> planetas = new List<Pieza>();
        planetas.Add(Resources.Load<Pieza>("Planeta Sol Planetarios"));
        planetas.Add(Resources.Load<Pieza>("Planeta Helado Planetarios"));
        planetas.Add(Resources.Load<Pieza>("Planeta Volcanico Planetarios"));
        planetas.Add(Resources.Load<Pieza>("Planeta Sagrado Planetarios"));


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
