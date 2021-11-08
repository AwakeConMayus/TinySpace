using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComodinMineros : PiezaIA
{
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        PiezaIA piezaReferencia;
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        //Explorador
        piezaReferencia = Resources.Load<PiezaIA>("Explorador Mineros");
        nuevosEstados.Add(piezaReferencia.BestInmediateOpcion(tabBase));

        
        //Combate
        piezaReferencia = Resources.Load<PiezaIA>("Combate Mineros");
        nuevosEstados.Add(piezaReferencia.BestInmediateOpcion(tabBase));


        //Laboratorio
        piezaReferencia = Resources.Load<PiezaIA>("Laboratorio Mineros");
        nuevosEstados.Add(piezaReferencia.BestInmediateOpcion(tabBase));


        //Estratega
        piezaReferencia = Resources.Load<PiezaIA>("Estratega Mineros");
        nuevosEstados.Add(piezaReferencia.BestInmediateOpcion(tabBase));


        return nuevosEstados;
    }
}
