using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesOyentes : IAOpciones
{

    List<InfoTablero>[] tablerosOrden = new List<InfoTablero>[5];

    List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

    private void Start()
    {       
        foreach (GameObject g in opcionesIniciales) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
    }


    public override List<InfoTablero> JugadaSimpleOpciones()
    {
        ResetTableros();

        List<InfoTablero> jugadas = new List<InfoTablero>();

        InfoTablero tabBase = new InfoTablero(Tablero.instance.mapa);


        for (int i = 0; i < 3; i++)
        {

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();
            foreach (InfoTablero newTab in pieza.Opcionificador(tabBase))
            {
                jugadas.Add(newTab);
                tablerosOrden[i].Add(newTab);
            }
        }

        return jugadas;
    }

    void ResetTableros()
    {
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            tablerosOrden[i] = new List<InfoTablero>();
        }
    }

    public override void EjecutarJugada(InfoTablero newTab)
    {
        base.EjecutarJugada(newTab);

        
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            if (tablerosOrden[i].Contains(newTab))
            {
                IARotarOpcion(i);
            }
        }

        ResetTableros();
    }

    public override bool Ahogado()
    {
        for (int i = 0; i < 3; i++)
        {
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0) return false;
        }
        return true;
    }

    public override InfoTablero BestRespuesta(InfoTablero tabBase)
    {
        List<InfoTablero> respuestas = new List<InfoTablero>();

        foreach (PiezaIA pia in abanicoOpciones) respuestas.Add(pia.BestInmediateOpcion(tabBase));

        int bestRespuesta = int.MinValue;

        InfoTablero respuesta = new InfoTablero();
        foreach (InfoTablero it in respuestas)
        {
            IATablero.instance.PrintInfoTablero(it);
            int puntos = PiezaIA.Evaluar(IATablero.instance.mapa, faccion);
            if (puntos > bestRespuesta)
            {
                bestRespuesta = puntos;
                respuesta = it;
            }
        }
        return respuesta;
    }

    public override void HandicapDeMano(int i)
    {

        int numero = 0;

        for (int j = 0; j < opcionesIniciales.Length; ++j)
        {
            if (opcionesIniciales[j].name.Contains("+"))
            {
                numero = j;
            }
        }
        int index = opcionesDisponibles.IndexOf(numero);

        //Debug.Log("handicap de mano del heroe planeter.\n Pieza a Cambiar: " + numero + " comparativa " + opcionesDisponibles[index]+ " Su posicion original: " + (index));
        switch (i)
        {
            case 0://tiene que ser el 1 para que no pise al terraformador
                   // Debug.Log(" Numero de pieza sutituta " + opcionesDisponibles[1] + " posicon 1");
                opcionesDisponibles[index] = opcionesDisponibles[1];
                opcionesDisponibles[1] = numero;
                break;
            case 1:
                // Debug.Log(" Numero de pieza sutituta " + opcionesDisponibles[3] + " posicon 3");
                opcionesDisponibles[index] = opcionesDisponibles[3];
                opcionesDisponibles[3] = numero;
                break;
            case 2:
                // Debug.Log(" Numero de pieza sutituta " + opcionesDisponibles[4] + " posicon 4");
                opcionesDisponibles[index] = opcionesDisponibles[4];
                opcionesDisponibles[4] = numero;
                break;
        }

        EventManager.TriggerEvent("RotacionOpciones");
    }

}
