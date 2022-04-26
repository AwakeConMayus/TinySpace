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

    protected PoderIA mypoder;

    public abstract void HandicapDeMano(int i);
    protected List<PiezaIA> abanicoOpciones = new List<PiezaIA>();

    public virtual void PrepararPreparacion()
    {
        if (PhotonNetwork.InRoom) poder = PhotonNetwork.Instantiate(poder.name, transform.position, Quaternion.identity);
        else poder = Instantiate(poder, transform.position, Quaternion.identity);
        Debug.Log(gameObject.name);
        poder.GetComponent<Poder>().SetPadre(this);
        poder.GetComponent<PoderIA>().Fases[0].padre = this;
        poder.GetComponent<PoderIA>().Fases[1].padre = this;
        Debug.Log(poder.name);
        mypoder = poder.GetComponent<PoderIA>();

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

    public virtual InfoTablero BestRespuesta(InfoTablero tabBase)
    {
        List<InfoTablero> respuestas = new List<InfoTablero>();

        foreach (PiezaIA pia in abanicoOpciones) respuestas.Add(pia.BestInmediateOpcion(tabBase, true));

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
    public virtual InfoTablero BestRespuestaPoder(InfoTablero tabBase, int turno)
    {
        List<InfoTablero> respuestas = new List<InfoTablero>();

        if (turno < 15) respuestas = mypoder.Fases[0].Opcionificador(tabBase, true);
        else respuestas = mypoder.Fases[0].Opcionificador(tabBase, true);

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
}
