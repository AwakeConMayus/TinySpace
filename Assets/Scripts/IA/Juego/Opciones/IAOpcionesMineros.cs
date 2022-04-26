using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpcionesMineros : IAOpciones
{
    public GameObject[] mejoras = new GameObject[4];
    public int mineral = 5;

    List<InfoTablero>[] tablerosPorCostes = new List<InfoTablero>[10];
    List<InfoTablero>[] tablerosOrden = new List<InfoTablero>[5];

    List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

    private void Start()
    {
        EventManager.StartListening("RecogerMineral", AddMineral);
        ResetTableros();

        foreach (GameObject g in opcionesIniciales) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
        foreach (GameObject g in mejoras) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
    }

    public override List<InfoTablero> JugadaSimpleOpciones()
    {
        List<InfoTablero> opciones = new List<InfoTablero>();

        InfoTablero tabBase = new InfoTablero(Tablero.instance.mapa);

        ResetTableros();


        for (int i = 0; i < 3; i++)
        {
            if (opcionesDisponibles[i] == 4) continue;

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            int mineralGastar = 0;
            if (opcionesDisponibles[i] == 4) mineralGastar = opcionesIniciales[4].GetComponent<Especial>().coste;

            if (mineral >= mineralGastar)
            {
                foreach(InfoTablero newTab in pieza.Opcionificador(tabBase))
                {
                    opciones.Add(newTab);
                    tablerosPorCostes[mineralGastar].Add(newTab);
                    tablerosOrden[i].Add(newTab);
                }
                
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (opcionesDisponibles[i] != 4) continue;

            PiezaIA pieza = opcionesIniciales[opcionesDisponibles[i]].GetComponent<PiezaIA>();

            int mineralGastar = 0;
            if (opcionesDisponibles[i] == 4) mineralGastar = opcionesIniciales[4].GetComponent<Especial>().coste;

            if (mineral >= mineralGastar)
            {
                foreach (InfoTablero newTab in pieza.Opcionificador(tabBase))
                {
                    opciones.Add(newTab);
                    tablerosPorCostes[mineralGastar].Add(newTab);
                    tablerosOrden[i].Add(newTab);
                }

            }
        }

        if (mineral >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (opcionesDisponibles[i] == 4) continue;

                PiezaIA pieza = mejoras[opcionesDisponibles[i]].GetComponent<PiezaIA>();

                foreach (InfoTablero newTab in pieza.Opcionificador(tabBase))
                {
                    opciones.Add(newTab);
                    tablerosPorCostes[3].Add(newTab);
                    tablerosOrden[i].Add(newTab);
                }
            }
        }

        return opciones;
    }

    public void AddMineral()
    {
        ++mineral;
    }

    void ResetTableros()
    {
        for (int i = 0; i < tablerosPorCostes.Length; i++)
        {
            tablerosPorCostes[i] = new List<InfoTablero>();
        }
        for (int i = 0; i < tablerosOrden.Length; i++)
        {
            tablerosOrden[i] = new List<InfoTablero>();
        }
    }

    public override void EjecutarJugada(InfoTablero newTab)
    {
        base.EjecutarJugada(newTab);

        for (int i = 0; i < tablerosPorCostes.Length; i++)
        {
            if (tablerosPorCostes[i].Contains(newTab))
            {
                mineral -= i;
            }
        }
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
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0 &&
                (opcionesDisponibles[i] != 4 || mineral >= opcionesIniciales[4].GetComponent<Especial>().coste))
                return false;

            if (mineral >= 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j != 4 && mejoras[opcionesDisponibles[j]].GetComponent<Pieza>().CasillasDisponibles().Count > 0) return false;
                }
            }
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
        int indexLab = opcionesDisponibles.IndexOf(2);
        int indexEstratega = opcionesDisponibles.IndexOf(3);

        if (i == 1)
        {
            int aux1 = opcionesDisponibles[0];
            int aux2 = opcionesDisponibles[1];

            opcionesDisponibles[0] = 2;
            opcionesDisponibles[1] = 3;

            opcionesDisponibles[indexLab] = aux1;
            opcionesDisponibles[indexEstratega] = aux2;
            EventManager.TriggerEvent("RotacionOpciones");
        }
        else
        {
            if (indexLab < 3 && indexEstratega < 3)
            {
                int nuevaPos = Random.Range(3, opcionesDisponibles.Count);
                int aux = opcionesDisponibles[nuevaPos];

                if (Random.Range(0, 2) == 0)
                {

                    opcionesDisponibles[nuevaPos] = 2;
                    opcionesDisponibles[indexLab] = aux;
                }
                else
                {
                    opcionesDisponibles[nuevaPos] = 3;
                    opcionesDisponibles[indexEstratega] = aux;
                }
            }
            EventManager.TriggerEvent("RotacionOpciones");
        }
    }

}
