using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALabMejMineros : PiezaIA
{
    public override List<List<Casilla>> Opcionificador(List<Casilla> listaBase)
    {
        Pieza piezaReferencia;
        List<List<Casilla>> nuevosEstados = new List<List<Casilla>>();
        piezaReferencia = new InvestigadorMinerosMejorado();
        piezaReferencia.Set_Jugador(jugador);

        List<Casilla> lc = new List<Casilla>(listaBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(listaBase))
        {
            listaBase = lc;
            Pieza piezaColocar = new InvestigadorMinerosAstro();
            piezaColocar.Set_Jugador(jugador);

            c.pieza = piezaColocar;
            piezaColocar.casilla = c;

            List<Casilla> rangoDestrucciones = FiltroCasillas.CasillasAdyacentes(FiltroCasillas.CasillasAdyacentes(c, true), false);

            foreach(Casilla cc in rangoDestrucciones)
            {
                if(cc.pieza && cc.pieza.Get_Jugador() != jugador)
                {
                    Pieza pc = cc.pieza;
                    cc.pieza = null;
                    nuevosEstados.Add(new List<Casilla>(listaBase));
                    cc.pieza = pc;
                }
            }

        }

        return nuevosEstados;
    }
}
