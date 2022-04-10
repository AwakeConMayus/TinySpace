//using System.Collections;
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
    GameObject EscojerEjercito;
    Toggle[] btnFacciones;
    int faccionSeleccionada;
    int AuxFaccionSelecionada;
    [SerializeField]
    TuSeleccion mi_Seleccion;
    [SerializeField]
    TuSeleccion eleccion_rival;
    [SerializeField]
    GameObject infoMineros;
    [SerializeField]
    GameObject infoOyentes;
    [SerializeField]
    GameObject FondoM1;
    [SerializeField]
    GameObject FondoM2;
    [SerializeField]
    GameObject FondoO1;
    [SerializeField]
    GameObject FondoO2;
    bool pve = false;

    [SerializeField]
    Text FrameRateText;
    float current;
    private void Awake()
    {

        FondoO1.GetComponent<Animator>().SetInteger("Posicion", 2);
        FondoO2.GetComponent<Animator>().SetInteger("Posicion", 2);
        FondoM1.GetComponent<Animator>().SetInteger("Posicion", 0);
        FondoM2.GetComponent<Animator>().SetInteger("Posicion", 0);
    }
    
    private void Start()
    {
        //* Obtiene todos las referencias a botones presentes en la parte del menú de selección de facción
        //* btnFaccione[0] = mineros, [1] Oyentes, [2] Honorables, [3] Simbiontes, [4] Buscar Partida, [5] Salir del juego
        btnFacciones = menuFaccion.GetComponentsInChildren<Toggle>();
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

        //  menuInicio.SetActive(false);
        if (menuFaccion.activeSelf)
        {
        menuInicio.SetActive(true);
        menuFaccion.SetActive(false);
        }
      else if (!menuFaccion.activeSelf)
        {
        menuFaccion.SetActive(true);
        }
        pve = setPve;
    }

    public void EnterMatchMaking()
    {
        print(pve);
        if (!pve) SceneManager.LoadScene(1);
        else
        {
            // SceneManager.LoadScene(2);
            EscojerEjercito.SetActive(true);
            menuInicio.SetActive(false);
        }
    }
    public void SalirMatchmaking()
    {
        EscojerEjercito.SetActive(false);
       // menuInicio.SetActive(true);
    }

    public void Reload()
    {
        //* Con esta sintaxis se evita cambiar el número de la escena si se quiere recargarla y se cambia su número en el orden de la build
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void selectFaccion(int faccion)
    {
        faccionSeleccionada = faccion;
        //* Asocia la facción seleccionada a la del scriptable object
        mi_Seleccion.faccion = (Faccion)faccion;
        switch (faccion)
        {
            case 1:
                AuxFaccionSelecionada = 1;
                infoMineros.SetActive(true);
                infoOyentes.SetActive(false);
                break;
            case 2:
                AuxFaccionSelecionada = 2;
                infoMineros.SetActive(false);
                infoOyentes.SetActive(true);
                break;
        }
    }

    public void ActivarMenuMineros()
    {
        FondoO1.GetComponent<Animator>().SetInteger("Posicion", -1);
        FondoO2.GetComponent<Animator>().SetInteger("Posicion", -1);
        
    }
    public void ActivarMenuOyentes()
    {
        FondoO1.GetComponent<Animator>().SetInteger("Posicion", 1);
        FondoO2.GetComponent<Animator>().SetInteger("Posicion", 1);
        
    }

}
