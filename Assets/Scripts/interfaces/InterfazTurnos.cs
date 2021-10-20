using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazTurnos : MonoBehaviour
{

    [SerializeField]
    List<GameObject> lista_bombillas;

    [SerializeField]
    GameObject bombilla_poder;

    [SerializeField]
    Text texto_informativo;

    private int jugador = 0;

    private int turno_actual = 0;

    bool rival = true;

    
    public void Awake()
    {
        EventManager.StartListening("Siguiente_turno", Siguiente_Turno);
    }
    public void Siguiente_Turno()
    {
        if(turno_actual == 0)
        {
            jugador = InstancePiezas.instance.jugador;
            if (jugador == 1)
            {
                Quaternion free = this.GetComponent<RectTransform>().localRotation;
                free.x += 180;
                this.GetComponent<RectTransform>().localRotation = free;
                rival = false;
                texto_informativo.text = "Turno del Rival";
            }
            
        }
        if(turno_actual > 13)
        {
            turno_actual = 2;
            if (jugador == 1)
            {
                Quaternion free = this.GetComponent<RectTransform>().localRotation;
                free.x -= 180;
                this.GetComponent<RectTransform>().localRotation = free;
            }
            else
            {
                Quaternion free = this.GetComponent<RectTransform>().localRotation;
                free.x += 180;
                this.GetComponent<RectTransform>().localRotation = free;
            }
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
        if (turno_actual % 2 != 0)
        {
            if (rival) texto_informativo.text = "Turno del Rival";
            else texto_informativo.text = "Tu Turno";
            rival = !rival;
        }       

    }
    public void Bombilla_Poder()
    {
        texto_informativo.text = "Poderes";
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
        if (!rival) texto_informativo.text = "Turno del Rival";
        else texto_informativo.text = "Tu Turno";
    }

}
