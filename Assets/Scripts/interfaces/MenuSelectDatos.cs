//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelectDatos : MonoBehaviour
{
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


    private void Start()
    {
        btnHeroes     = new Button[3];
        btnHeroes     = menuHeroes.GetComponentsInChildren<Button>();

        btnEspeciales = new Button[3];
        btnEspeciales = menuEspeciales.GetComponentsInChildren<Button>();

        btnMejoradas  = new Button[3];
        btnMejoradas  = menuMejoradas.GetComponentsInChildren<Button>();

    }

    public void selectHeroe(int heroe)
    {
        for (int i = 0; i < 3; i++)
        {
            btnHeroes[i].interactable = true;
        }

        heroeSel = heroe;
        btnHeroes[heroeSel].interactable = false;

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
