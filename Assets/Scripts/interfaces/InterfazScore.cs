using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InterfazScore : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreAliado;
    [SerializeField] TextMeshPro scoreEnemigo;
    [SerializeField] GameObject fillEnemigo;
    [SerializeField] GameObject fillAliado;
    [SerializeField] TuSeleccion mi_seleccion;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("Siguiente_turno", Update_Score);
        EventManager.StartListening("UpdateScore", Update_Score);
    }

    public void Update_Score()
    {
        int[] aux_puntuacion = Tablero.instance.RecuentoPuntos();

        int enemigo = 0;
        int aliado = aux_puntuacion[(int)InstancePiezas.instance.faccion - 1];
        for(int i = 0; i < aux_puntuacion.Length; ++i)
        {
            if (aux_puntuacion[i] != 0 && i != ((int)InstancePiezas.instance.faccion - 1))
            {
                enemigo = aux_puntuacion[i];
            }
        }
        scoreAliado.text =aliado.ToString();
        scoreEnemigo.text = enemigo.ToString();

        FillRects(aliado, enemigo);
    }

    public void FillRects(int puntuacion_aliada, int puntuacion_enemiga)
    {
        int colchon = 2;
        if (puntuacion_aliada != 0 && puntuacion_enemiga != 0) colchon = 0;

        puntuacion_aliada += colchon;
        puntuacion_enemiga += colchon;

        float fill = puntuacion_aliada / (puntuacion_aliada + puntuacion_enemiga);
        fillAliado.GetComponent<Image>().fillAmount = fill;
    }
}
