using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpciones : Opciones
{
    public override void PrepararPreparacion() { }

    public override void Preparacion()
    {
        opcionesDisponibles = new List<int>();
        foreach(PoderIABase fase in poder.GetComponent<PoderIA>().Fases)
        {
            fase.padre = this;
            fase.padre = this;
        }

        


        List<int> disponibles = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            disponibles.Add(i);
        }
        int numero;
        for (int i = 0; i < opcionesIniciales.Length; i++)
        {
            numero = disponibles[Random.Range(0, disponibles.Count)];
            opcionesDisponibles.Add(numero);
            disponibles.Remove(numero);
        }
    }

    public void IARotarOpcion(int i)
    {
        opcionActual = i;
        int aux = opcionesDisponibles[opcionActual];
        opcionesDisponibles.Remove(aux);
        opcionesDisponibles.Add(aux);
    }

    public virtual void Jugar() { }
    public virtual void JugarPoder(int i)        
    {
        Debug.Log("IA PODR");
        InfoTablero mapaPostPoder = poder.GetComponent<PoderIA>().Fases[i].BestInmediateOpcion(new InfoTablero(Tablero.instance.mapa));
        ActualizarTablero(mapaPostPoder);
    }

    public void ActualizarTablero(InfoTablero newTab)
    {
        Tablero.instance.PrintInfoTablero(newTab);
    }
}
