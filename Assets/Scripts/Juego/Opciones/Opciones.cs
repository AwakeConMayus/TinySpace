using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Opciones : MonoBehaviour
{
    public GameObject poder;
    public GameObject[] opcionesIniciales = new GameObject[5];

    [SerializeField]
    public Faccion faccion;

    [HideInInspector]
    public List<int> opcionesDisponibles = new List<int>();

    protected int opcionActual = -1;

    protected bool active = true;

    public OpcionesRival mi_reflejo;

    public virtual void PrepararPreparacion()
    {
        if (PhotonNetwork.InRoom) poder = PhotonNetwork.Instantiate(poder.name, transform.position, Quaternion.identity);
        else poder = Instantiate(poder, transform.position, Quaternion.identity);
        poder.GetComponent<Poder>().SetPadre(this);
        poder.GetComponent<PoderIA>().Fases[0].padre = this;
        poder.GetComponent<PoderIA>().Fases[1].padre = this;
        Debug.Log(poder.name);
    }

    public virtual void Preparacion()
    {
        EventManager.StartListening("ColocarPieza", Rotar);
        EventManager.StartListening("BloquearInput", BloquearInput);
        EventManager.StartListening("DesbloquearInput", DesbloquearInput);
        opcionesDisponibles = new List<int>();

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
        if(mi_reflejo) mi_reflejo.Primera_vez(opcionesDisponibles[0], opcionesDisponibles[1], opcionesDisponibles[2], opcionesDisponibles[3], opcionesDisponibles[4]);
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

    public virtual void Rotar()
    {
        if (opcionActual < 0) return;
        int aux = opcionesDisponibles[opcionActual];
        opcionesDisponibles.Remove(aux);
        opcionesDisponibles.Add(aux);
        if(mi_reflejo) mi_reflejo.Rotar(opcionActual);
        opcionActual = -1;
        EventManager.TriggerEvent("RotacionOpciones");
    }  
    
    public void SetActive(bool b)
    {
        active = b;
    }

    public void BloquearInput()
    {
        active = false;
    }
    public void DesbloquearInput()
    {
        active = true;
    }

    public abstract bool Ahogado();
}
