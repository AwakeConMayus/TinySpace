using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPropulsorCambioOrbitalOyentes : PiezaIA
{
    [SerializeField] Pieza restos;
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            c.pieza = null;
            InfoTablero newTab = new InfoTablero(IATablero.instance.mapa);

            foreach(Casilla cc in c.adyacentes)
            {
                IATablero.instance.PrintInfoTablero(newTab);
                if(cc && (!cc.pieza || (cc.pieza.faccion != faccion && !cc.pieza.astro)))
                {
                    bool valido = true;
                    foreach(Casilla ccc in cc.adyacentes)
                    {
                        if (ccc && ccc.pieza && ccc.pieza.clase == Clase.planeta) valido = false;
                    }

                    if(valido)
                    {
                        cc.pieza = restos;
                        opciones.Add(new InfoTablero(IATablero.instance.mapa));
                    }
                }
            }
        }

        return opciones;
    }
}
