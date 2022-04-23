using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATerraformador : PiezaIA
{
    [SerializeField] List<Pieza> formasPlanetas;

    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        Pieza nuevoPlaneta = null;

        foreach(int i in tabBase.tablero)
        {
            if (i == (int)IDPieza.PlanetaVolcanico) nuevoPlaneta = formasPlanetas[3];
        }

        if(nuevoPlaneta == null)
        {
            foreach (int i in tabBase.tablero)
            {
                if (i == (int)IDPieza.PlanetaHelado) nuevoPlaneta = formasPlanetas[2];
            }
        }

        if (nuevoPlaneta == null)
        {
            foreach (int i in tabBase.tablero)
            {
                if (i == (int)IDPieza.Sol) nuevoPlaneta = formasPlanetas[1];
            }
        }

        if (nuevoPlaneta == null) nuevoPlaneta = formasPlanetas[0];
        

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach(Casilla c in IATablero.instance.mapa)
        {
            if (c.pieza && c.pieza.GetComponent<Planetas>())
            {
                c.pieza = nuevoPlaneta;
                opciones.Add(new InfoTablero(IATablero.instance.mapa));
                IATablero.instance.PrintInfoTablero(tabBase);
            }
        }

        return opciones;
    }    
}
