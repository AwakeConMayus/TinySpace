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

    [SerializeField] float timeToChange;
    [SerializeField] Image imagenFaccion;
    [SerializeField] Sprite[] imagenes;
    [SerializeField] TuSeleccion miSeleccion;
    [SerializeField] GameObject[] brilliRober;

    float timer;

    float fillObjetivo;
    float diferenciaFill;
    void Start()
    {
        timer = timeToChange;
        EventManager.StartListening("Siguiente_turno", Update_Score);
        EventManager.StartListening("UpdateScore", Update_Score);

        switch (miSeleccion.faccion)
        {            
            case Faccion.minero:
                imagenFaccion.sprite = imagenes[0];
                break;
            case Faccion.oyente:
                imagenFaccion.sprite = imagenes[1];
                break;            
        }

        Update_Score();
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

        fillObjetivo = puntuacion_aliada / (float)(puntuacion_aliada + puntuacion_enemiga);
        diferenciaFill = fillAliado.GetComponent<Image>().fillAmount;
        foreach (GameObject g in brilliRober) g.SetActive(false);
        if (diferenciaFill < fillObjetivo) brilliRober[0].SetActive(true);
        else if (diferenciaFill > fillObjetivo) brilliRober[1].SetActive(true);
        timer = 0;
    }

    private void Update()
    {
        if (timer < timeToChange)
        {
            timer += Time.deltaTime;
            fillAliado.GetComponent<Image>().fillAmount = Mathf.Lerp(diferenciaFill, fillObjetivo, timer / timeToChange);
        }
        else foreach (GameObject g in brilliRober) g.SetActive(false);
    }


}
