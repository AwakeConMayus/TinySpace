using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesMineros : Opciones
{
    public GameObject[] mejoras = new GameObject[4];
    GameObject[] backup = new GameObject[4];
    PoderMineros poderMinero;

    public int mineral = 5;

    public bool especial = false;

    int mineralGastar;

    List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

    private void Start()
    {
        EventManager.StartListening("RecogerMineral", RecogerMineral);
        EventManager.StartListening("ColocarPieza", EjecutarPago);
        for (int i = 0; i < 4; i++)
        {
            backup[i] = opcionesIniciales[i];
        }
        Preparacion();
        if(mi_reflejo) mi_reflejo.poder = this.gameObject;

        foreach (GameObject g in opcionesIniciales) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
        foreach (GameObject g in mejoras) abanicoOpciones.Add(g.GetComponent<PiezaIA>());
    }
    public void RecogerMineral()
    {
        ++mineral;
        EventManager.TriggerEvent("CambioMineral");
    }

    public bool GastarMineral(int i)
    {
        if (mineral >= i)
        {
            mineralGastar = i;
            return true;
        }
        else return false;
    }

    public void EjecutarPago()
    {
        if (mineralGastar == 0) return;
        mineral -= mineralGastar;
        EventManager.TriggerEvent("CambioMineral");
        mineralGastar = 0;
        if (especial) EspecialMode();
    }

    public override void Seleccion(int i)
    {
        if (!active) return;

        if (opcionesDisponibles[i] == 4)
        {
            if (GastarMineral(opcionesIniciales[4].GetComponent<Especial>().coste))
            {                
                base.Seleccion(i);
            }
        }

        else if (especial)
        {
            if (GastarMineral(3))
            {
                base.Seleccion(i);
            }
        }
        else
        {
            GastarMineral(0);
            base.Seleccion(i);
        }
    }

    public void EspecialMode()
    {
        if (!active) return;
        InstancePiezas.instance.LimpiarPieza();
        mineralGastar = 0;
        especial = !especial;
        if (especial)
        {
            for (int i = 0; i < 4; i++)
            {
                opcionesIniciales[i] = mejoras[i];
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                opcionesIniciales[i] = backup[i];
            }
        }
        EventManager.TriggerEvent("RotacionOpciones");
    }

    public override bool Ahogado()
    {
        for (int i = 0; i < 3; i++)
        {
            if (opcionesIniciales[opcionesDisponibles[i]].GetComponent<Pieza>().CasillasDisponibles().Count > 0 && 
                (opcionesDisponibles[i] != 4 || mineral >= opcionesIniciales[4].GetComponent<Especial>().coste)) 
                return false;
            
            if(mineral >= 3)
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
