using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComodinMineros : PiezaIA
{
    [SerializeField] List<PiezaIA> piezasReferencia;
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        //Explorador
        nuevosEstados.Add(piezasReferencia[0].BestInmediateOpcion(tabBase));

        
        //Combate
        nuevosEstados.Add(piezasReferencia[1].BestInmediateOpcion(tabBase));


        //Laboratorio
        nuevosEstados.Add(piezasReferencia[2].BestInmediateOpcion(tabBase));


        //Estratega
        nuevosEstados.Add(piezasReferencia[3].BestInmediateOpcion(tabBase));


        return nuevosEstados;
    }
}
