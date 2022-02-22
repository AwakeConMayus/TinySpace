using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATerraformador : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        Pieza nuevoPlaneta = null;

        foreach(int i in tabBase.tablero)
        {
            if (i == (int)IDPieza.PlanetaVolcanico) nuevoPlaneta = Resources.Load<Pieza>("Planeta Sagrado Planetarios");
        }

        if(nuevoPlaneta == null)
        {
            foreach (int i in tabBase.tablero)
            {
                if (i == (int)IDPieza.PlanetaHelado) nuevoPlaneta = Resources.Load<Pieza>("Planeta Volcanico Planetarios");
            }
        }

        if (nuevoPlaneta == null)
        {
            foreach (int i in tabBase.tablero)
            {
                if (i == (int)IDPieza.Sol) nuevoPlaneta = Resources.Load<Pieza>("Planeta Helado Planetarios");
            }
        }

        if (nuevoPlaneta == null) nuevoPlaneta = Resources.Load<Pieza>("Planeta Sol Planetarios");
        

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in IATablero.instance.mapa)
        {
            if (c.pieza.GetComponent<Planetas>())
            {
                c.pieza = nuevoPlaneta;
                opciones.Add(new InfoTablero(IATablero.instance.mapa));
                IATablero.instance.PrintInfoTablero(tabBase);
            }
        }

        return opciones;
    }    
}
