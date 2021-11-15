using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPropulsorCambioOrbitalOyentes : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        Pieza piezaBase = GetComponent<Pieza>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in piezaBase.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza planetaMover = c.pieza;
            c.pieza = null;
            InfoTablero newTab = new InfoTablero(IATablero.instance.mapa);

            foreach(Casilla cc in c.adyacentes)
            {
                IATablero.instance.PrintInfoTablero(newTab);
                if(cc && (!cc.pieza || (cc.pieza.faccion != faccion && !cc.pieza.astro)))
                {
                    cc.pieza = planetaMover;
                    opciones.Add(new InfoTablero(IATablero.instance.mapa));
                }
            }
        }

        return opciones;
    }
}
