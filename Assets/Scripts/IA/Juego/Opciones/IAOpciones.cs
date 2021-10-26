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

    public void ActualizarTablero(List<Casilla> nuevo)
    {
        while (Tablero.instance.mapa.Count < nuevo.Count)
        {
            Tablero.instance.Crear_Casilla_Vacia();
        }

        for (int i = 0; i < nuevo.Count; i++)
        {
            if(Tablero.instance.mapa[i].pieza != nuevo[i].pieza)
            {

            }
        }
    }
}
