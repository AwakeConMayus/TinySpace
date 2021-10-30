using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALabMejMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        Pieza piezaReferencia;
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();
        piezaReferencia = Resources.Load<Pieza>("Laboratorio Mineros Mejorado");
        piezaReferencia.Set_Jugador(jugador);

        IATablero.instance.PrintInfoTablero(tabBase);

        foreach (Casilla c in piezaReferencia.CasillasDisponibles(IATablero.instance.mapa))
        {
            IATablero.instance.PrintInfoTablero(tabBase);

            Pieza piezaColocar = Resources.Load<Pieza>("LaboratorioMinerosAstro");
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
                    nuevosEstados.Add(new InfoTablero(IATablero.instance.mapa));
                    cc.pieza = pc;
                }
            }

        }

        return nuevosEstados;
    }
}
