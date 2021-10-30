using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezaIA : MonoBehaviour
{
    [SerializeField]
    public Faccion faccion;
    public virtual List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        Pieza piezaReferencia = GetComponent<Pieza>();
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            Pieza piezaColocar = piezaReferencia;

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;
            nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
            c.pieza = null;
        }

        return nuevosEstados;
    }   

    public virtual InfoTablero BestInmediateOpcion(InfoTablero tabBase)
    {
        InfoTablero MejorOpcion = tabBase;
        int mejorPuntuacion = -1000;


        foreach (InfoTablero it in Opcionificador(tabBase))
        {
            IATablero.instance.PrintInfoTablero(it);
            int puntosNuevos = Evaluar(IATablero.instance.mapa, faccion);
            if (puntosNuevos > mejorPuntuacion)
            {
                mejorPuntuacion = puntosNuevos;
                MejorOpcion = it;
            }
        }

        return MejorOpcion;
    }

    public static int Evaluar(List<Casilla> mapa, Faccion jugador)
    {
        int puntos = 0;
        foreach(Casilla c in mapa)
        {
            if (c.pieza)
            {
                if (c.pieza.faccion == jugador)
                {
                    puntos += c.pieza.Puntos();
                }
                else puntos -= c.pieza.Puntos();
            }
        }
        return puntos;
    }
}
