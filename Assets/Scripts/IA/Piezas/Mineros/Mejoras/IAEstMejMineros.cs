using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEstMejMineros : PiezaIA
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        Pieza piezaReferencia;
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();
        piezaReferencia = Resources.Load<Pieza>("Estratega Mineros Mejorado");
        piezaReferencia.Set_Jugador(jugador);

        List<Casilla> lc = new List<Casilla>(listaBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(listaBase))
        {
            listaBase = lc;
            Pieza piezaColocar = Resources.Load<Pieza>("EstrategaMinerosAstro");
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;


            foreach (Casilla cc in c.adyacentes)
            {
                if (cc && cc.pieza && cc.pieza.Get_Jugador() == jugador && !cc.pieza.CompareClase(Clase.combate))
                {
                    Pieza nuevaPieza = Resources.Load<Pieza>("Combate Mineros");
                    nuevaPieza.Set_Jugador(jugador);
                    cc.pieza = nuevaPieza;
                }
            }

            nuevosEstados.Add(new List<Casilla>(listaBase));

        }
        return nuevosEstados;
    }
}
