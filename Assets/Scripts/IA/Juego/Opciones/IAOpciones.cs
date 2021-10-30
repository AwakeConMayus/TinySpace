using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOpciones : Opciones
{
    public override void PrepararPreparacion() { }

    public override void Preparacion()
    {
        foreach(PoderIABase fase in poder.GetComponent<PoderIA>().Fases)
        {
            fase.padre = this;
            fase.padre = this;
        }
        poder.GetComponent<Poder>().jugador = jugador;

        foreach(GameObject pieza in opcionesIniciales)
        {
            pieza.GetComponent<PiezaIA>().jugador = jugador;
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
        InfoTablero mapaPostPoder = poder.GetComponent<PoderIA>().Fases[i].BestInmediateOpcion(new InfoTablero(Tablero.instance.mapa));
        ActualizarTablero(mapaPostPoder);
    }

    public void ActualizarTablero(InfoTablero newTab)
    {
        IATablero.instance.PrintInfoTablero(newTab);
        List<Casilla> nuevo = IATablero.instance.mapa;

        while (Tablero.instance.mapa.Count < nuevo.Count)
        {
            Tablero.instance.Crear_Casilla_Vacia();
        }

        for (int i = 0; i < nuevo.Count; i++)
        {
            if(Tablero.instance.mapa[i].pieza != nuevo[i].pieza)
            {
                if (Tablero.instance.mapa[i].pieza) Destroy(Tablero.instance.mapa[i].pieza.gameObject);

                GameObject pieza = GetPiezabyIAPieza(nuevo[i].pieza);
                pieza.GetComponent<Pieza>().Set_Pieza_Extra();
                InstancePiezas.instance.CrearPieza(Tablero.instance.mapa[i], pieza);
            }
        }
    }

    protected GameObject GetPiezabyIAPieza(Pieza script)
    {
        GameObject pieza = null;

        foreach(GameObject g in opcionesIniciales)
        {
            if (g.GetComponent<Pieza>() == script) pieza = g;
        }

        return pieza;
    }
}
