using System.Collections;
using System.Collections.Generic;  //Clanta: debo empezar a acostumbrarme que DobleC comenta siempre las librerias
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelectDatos : MonoBehaviour
{
    //Clanta: AVISO IMPORTANTE a este script le faltan todas las rpc necesarias. (Esperar al otro jugador para empezar)(Esperar a comprobar la faccion del rival para seleccionar aleatoriamente)(etc)


    [SerializeField]
    GameObject menuHeroes;
    [SerializeField]
    GameObject menuEspeciales;
    [SerializeField]
    GameObject menuMejoradas;
    [SerializeField]
    Button enterMatch;

    Button[] btnHeroes;
    Button[] btnEspeciales;
    Button[] btnMejoradas;

    int heroeSel    = -1;
    int especialSel = -1;
    int mejoradaSel = -1;

    [SerializeField]
    List<OpcionesFaccion> opciones = new List<OpcionesFaccion>();
    [SerializeField]
    TuSeleccion mi_Seleccion;

    OpcionesFaccion faccion_Seleccionada;

    private void Start()
    {
        btnHeroes     = new Button[3];
        btnHeroes     = menuHeroes.GetComponentsInChildren<Button>();

        btnEspeciales = new Button[3];
        btnEspeciales = menuEspeciales.GetComponentsInChildren<Button>();

        btnMejoradas  = new Button[3];
        btnMejoradas  = menuMejoradas.GetComponentsInChildren<Button>();

        Preparar();
    }

    public void Preparar()
    {
        //Eleccion de las opciones correctas basadas en la faccion selecionado previamente
        for (int i = 0; i < opciones.Count; ++i)
        {
            if (mi_Seleccion.faccion == opciones[i].faccion)
            {
                faccion_Seleccionada = opciones[i];
            }
        }
        //Sistema de acceso aleatorio a la faccion 
        if (!faccion_Seleccionada)
        {
            Debug.Log("Selecciono faccion de maneras aleatoria");

            int rnd = Random.Range(0, opciones.Count);

            faccion_Seleccionada = opciones[rnd];

            mi_Seleccion.faccion = faccion_Seleccionada.faccion;
        }
        // Desactivacion del menu de mejoras del minero
        if(faccion_Seleccionada.faccion == Faccion.minero)
        {
            menuMejoradas.SetActive(false);
            mi_Seleccion.mis_mejoras = faccion_Seleccionada.posibles_Piezas_Especializadas;
        }

        for(int j = 0; j < faccion_Seleccionada.piezas_Basicas.Length; ++j)
        {
            mi_Seleccion.mis_opciones[j] = faccion_Seleccionada.piezas_Basicas[j];
        }
    }

    public void selectHeroe(int heroe)
    {
        for (int i = 0; i < 3; i++)
        {
            btnHeroes[i].interactable = true;
        }

        heroeSel = heroe;
        btnHeroes[heroeSel].interactable = false;

        mi_Seleccion.mi_poder = faccion_Seleccionada.posibles_Poders[heroe];

        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void selectEspecial(int especial)
    {
        for (int i = 0; i < 3; i++)
        {
            btnEspeciales[i].interactable = true;
        }

        especialSel = especial;
        btnEspeciales[especialSel].interactable = false;

        mi_Seleccion.mis_opciones[4] = faccion_Seleccionada.posibles_Piezas_Especiales[especial];

        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void selectMejorada(int mejorada)
    {
        for (int i = 0; i < 3; i++)
        {
            btnMejoradas[i].interactable = true;
        }

        mejoradaSel = mejorada;
        btnMejoradas[mejoradaSel].interactable = false;

        for (int z = 0; z < faccion_Seleccionada.piezas_Basicas.Length; ++z)
        {
            mi_Seleccion.mis_opciones[z] = faccion_Seleccionada.piezas_Basicas[z];
        }

        mi_Seleccion.mis_opciones[faccion_Seleccionada.huecos_Especializadas[mejorada]] = faccion_Seleccionada.posibles_Piezas_Especializadas[mejorada];
        if (!enterMatch.interactable && heroeSel != -1 && especialSel != -1 && mejoradaSel != -1) enterMatch.interactable = true;
    }

    public void EnterGame()
    {
        SceneManager.LoadScene(2);
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}
