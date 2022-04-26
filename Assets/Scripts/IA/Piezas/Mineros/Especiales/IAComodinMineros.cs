using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAComodinMineros : PiezaIA
{
    [SerializeField] List<PiezaIA> piezasReferencia;
    public override List<InfoTablero> Opcionificador(InfoTablero tabBase, bool simplify = false)
    {
        List<InfoTablero> nuevosEstados = new List<InfoTablero>();

        //Explorador
        nuevosEstados.Add(piezasReferencia[0].BestInmediateOpcion(tabBase, simplify));

        
        //Combate
        nuevosEstados.Add(piezasReferencia[1].BestInmediateOpcion(tabBase, simplify));


        //Laboratorio
        nuevosEstados.Add(piezasReferencia[2].BestInmediateOpcion(tabBase, simplify));


        //Estratega
        nuevosEstados.Add(piezasReferencia[3].BestInmediateOpcion(tabBase, simplify));


        return nuevosEstados;
    }
}
