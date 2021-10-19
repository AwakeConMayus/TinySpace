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

    
    public void Awake()
    {
        EventManager.StartListening("Siguiente_turno", Siguiente_Turno);

    }
    public void Siguiente_Turno()
    {
        if(turno_actual == 0)
        {
            if(InstancePiezas.instance.jugador == 1)
            {
                Debug.Log("reposicion de lo de los turn0s");
                this.GetComponent<RectTransform>().rotation = new Quaternion(180, 0, 0, 1);
            }
        }
        if(turno_actual > 13)
        {
            turno_actual = 2;
            Reset();
        }
        if(turno_actual < 12 && turno_actual > 1)
        {
            Encender_Bombilla(turno_actual);
        }
        if (turno_actual == 12)
        {
            Bombilla_Poder();
        }
        ++turno_actual;
    }

    public void Encender_Bombilla(int turno_actual)
    {
        if(turno_actual == 2)
        {
            lista_bombillas[turno_actual-2].GetComponent<Animator>().SetTrigger("actual");
        }
        else
        {
            lista_bombillas[turno_actual-2].GetComponent<Animator>().SetTrigger("actual");
            lista_bombillas[turno_actual - 3].GetComponent<Animator>().SetTrigger("pasado");
        }
    }
    public void Bombilla_Poder()
    {
        lista_bombillas[turno_actual-3].GetComponent<Animator>().SetTrigger("pasado");
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
