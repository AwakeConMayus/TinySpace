using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Opciones : MonoBehaviour
{
    public GameObject poder;
    public GameObject[] opcionesIniciales = new GameObject[5];

    public int jugador;

    [HideInInspector]
    public List<int> opcionesDisponibles = new List<int>();
    [HideInInspector]
    public List<int> opcionesEnfriamiento = new List<int>();

    protected int opcionActual = -1;

    protected bool active = false;

    public void PrepararPreparacion()
    {
        poder = PhotonNetwork.Instantiate(poder.name, transform.position, Quaternion.identity);
        poder.GetComponent<Poder>().SetPadre(this);
    }

    public void Preparacion()
    {
        EventManager.StartListening("ColocarPieza", Rotar);
        poder.GetComponent<Poder>().jugador = jugador;


        List<int> disponibles = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            disponibles.Add(i);
        }
        int numero;

        for (int i = 0; i < 3; i++)
        {
            numero = disponibles[Random.Range(0, disponibles.Count)];
            opcionesDisponibles.Add(numero);
            disponibles.Remove(numero);
        }

        for (int i = 0; i < 2; i++)
        {
            numero = disponibles[Random.Range(0, disponibles.Count)];
            opcionesEnfriamiento.Add(numero);
            disponibles.Remove(numero);
        }
        EventManager.TriggerEvent("RotacionOpciones");
    }

    public virtual void Seleccion(int i)
    {
        if (!active) return;
        opcionActual = i;
        InstancePiezas.instance.SetPieza(opcionesIniciales[opcionesDisponibles[i]]);
    }

    public virtual void SeleccionForzada(int i)
    {
        opcionActual = i;
        InstancePiezas.instance.SetPieza(opcionesIniciales[opcionesDisponibles[i]]);
    }

    public void Rotar()
    {
        if (opcionActual < 0) return;
        opcionesEnfriamiento.Add(opcionesDisponibles[opcionActual]);
        opcionesDisponibles.Remove(opcionesDisponibles[opcionActual]);
        opcionesDisponibles.Add(opcionesEnfriamiento[0]);
        opcionesEnfriamiento.Remove(opcionesEnfriamiento[0]);
        opcionActual = -1;
        EventManager.TriggerEvent("RotacionOpciones");
    }  
    
    public void SetActive(bool b)
    {
        active = b;
    }
}
