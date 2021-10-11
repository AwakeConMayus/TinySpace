using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfazTurnos : MonoBehaviour
{

    [SerializeField]
    List<GameObject> lista_bombillas;

    [SerializeField]
    GameObject bombilla_poder;

    private int turno_actual = 0;

    public void Start()
    {
        EventManager.StartListening("PasoTurno", Siguiente_Turno);

    }
    public void Siguiente_Turno()
    {
        Debug.Log("turno interfaz" + turno_actual);

        if(turno_actual > 10)
        {
            turno_actual = 0;
            Reset();
        }
        if(turno_actual < 10)
        {
            Encender_Bombilla(turno_actual);
        }
        if (turno_actual == 10)
        {
            Bombilla_Poder();
        }
        ++turno_actual;
    }

    public void Encender_Bombilla(int turno_actual)
    {
        if(turno_actual == 0)
        {
            lista_bombillas[turno_actual].GetComponent<Animator>().SetTrigger("actual");
        }
        else
        {
            lista_bombillas[turno_actual].GetComponent<Animator>().SetTrigger("actual");
            lista_bombillas[turno_actual - 1].GetComponent<Animator>().SetTrigger("pasado");
        }
    }
    public void Bombilla_Poder()
    {
        lista_bombillas[turno_actual-1].GetComponent<Animator>().SetTrigger("pasado");
        bombilla_poder.GetComponent<Animator>().SetTrigger("actual");
    }
    public void Reset()
    {
        bombilla_poder.GetComponent<Animator>().SetTrigger("reset");
        foreach(GameObject g in lista_bombillas)
        {
            g.GetComponent<Animator>().SetTrigger("reset");
        }
    }

}
