﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    GameObject menuInicio;
    [SerializeField]
    GameObject menuFaccion;
    [SerializeField]
    GameObject salir;

    Button[] btnFacciones;
    int faccionSeleccionada;

    [SerializeField]
    TuSeleccion mi_Seleccion;
    [SerializeField]
    TuSeleccion eleccion_rival;

    bool pve = false;

    [SerializeField]
    Text FrameRateText;
    float current;

    private void Start()
    {
        //* Obtiene todos las referencias a botones presentes en la parte del menú de selección de facción
        //* btnFaccione[0] = mineros, [1] Oyentes, [2] Honorables, [3] Simbiontes, [4] Buscar Partida, [5] Salir del juego
        btnFacciones = menuFaccion.GetComponentsInChildren<Button>();
        //* Se asegura de que, independientemente del estado de la escena, al ejecutar se muestren bien los botones
        menuInicio.SetActive(true);
        menuFaccion.SetActive(false);

        mi_Seleccion.faccion = Faccion.none;
        mi_Seleccion.mi_poder = null;
        eleccion_rival.faccion = Faccion.none;
        eleccion_rival.mi_poder = null;

        for (int i = 0; i < mi_Seleccion.mis_opciones.Length; ++i)
        {
            mi_Seleccion.mis_opciones[i] = null;
            eleccion_rival.mis_opciones[i] = null;
        }
    }

    private void Update()
    {
        current = (int)(1f / Time.unscaledDeltaTime);
        FrameRateText.text = current.ToString() + " FPS";
    }

    //* Funcción asociada a jugar, cambiar botones
    public void ChangeButtons(bool setPve = false)
    {
        menuInicio.SetActive(false);
        menuFaccion.SetActive(true);
        pve = setPve;
    }

    public void EnterMatchMaking()
    {
        if(!pve) SceneManager.LoadScene(1);
        else
        {

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void selectFaccion(int faccion)
    {
        faccionSeleccionada = faccion;

        //* Activa todos los botones de facción cuando seleccionas una (para desactivar luego el botón en específico pulsado)
        for (int i = 0; i < 3; i++) //* tiene que ser i < 4 pero hay 2 facciones sin implementar, así que esas nunca se activan
        {
            Debug.Log(btnFacciones[i].gameObject.name);
            btnFacciones[i].interactable = true;
        }

        //* Desactiva el botón seleccionado 
        btnFacciones[faccionSeleccionada].interactable = false;
        
        //* Asocia la facción seleccionada a la del scriptable object
        mi_Seleccion.faccion = (Faccion)faccionSeleccionada;

        
        //* Activa el botón de Buscar Partida (match making) una vez se vea que tienes una facción seleccionada
        salir.SetActive(true);
    }

}
