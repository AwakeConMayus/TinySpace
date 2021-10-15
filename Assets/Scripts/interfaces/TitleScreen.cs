using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    GameObject menuInicio;
    [SerializeField]
    GameObject menuFaccion;

    Button[] btnFacciones;


    int faccionSeleccionada;

    private void Start()
    {
        btnFacciones = new Button[5];
        btnFacciones = menuFaccion.GetComponentsInChildren<Button>();

        menuInicio.SetActive(true);
        menuFaccion.SetActive(false);
    }

    public void ChangeButtons()
    {
        menuInicio.SetActive(false);
        menuFaccion.SetActive(true);
    }

    public void EnterMatchMaking()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void selectFaccion(int faccion)
    {
        for (int i = 0; i < 4; i++)
        {
            btnFacciones[i].interactable = true;
        }

        faccionSeleccionada = faccion;
        btnFacciones[faccionSeleccionada].interactable = false;

        if (!btnFacciones[4].interactable) btnFacciones[4].interactable = true;
    }
}
