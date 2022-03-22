using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MenuFinalParitda : MonoBehaviour
{

    [SerializeField] TextMeshPro texto_final_partida_Enemigo;
    [SerializeField] TextMeshPro texto_final_partida_Jugador;
    [SerializeField]
    TuSeleccion mi_Seleccion;
    [SerializeField]
    TuSeleccion seleccion_rival;
    [SerializeField] GameObject ImagenesRival;
    [SerializeField] GameObject ImagenesJugador;
    [SerializeField] ScriptableObject infoMineros;
    [SerializeField] ScriptableObject infoOyentes;
    [SerializeField] GameObject Activar;
    // Start is called before the first frame update

    private void Start()
    {
       
       /* for (int i = 0; i <3; ++i)
        {
            if(mi_Seleccion.mi_poder.name ==  opcionesFaccion.PosiblesImagenesHeroes[i].name)
            ImagenesJugador.GetComponentInChildren<SpriteRenderer>().sprite = opcionesFaccion.PosiblesImagenesHeroes[i].GetComponentInChildren<Image>().sprite;
        }*/
    }
    
    public void Final_Partida(int[] puntos)
    {
        int[] aux_puntuacion = Tablero.instance.RecuentoPuntos();

        int aliado = 0;
        int enemigo = 0;

        aliado = aux_puntuacion[(int)InstancePiezas.instance.faccion - 1];
        for (int i = 0; i < aux_puntuacion.Length; ++i)
        {
            if (aux_puntuacion[i] != 0 && i != ((int)InstancePiezas.instance.faccion - 1))
            {
                enemigo = aux_puntuacion[i];
            }
        }

        if (aliado - enemigo > 0) {
            texto_final_partida_Enemigo.text = "Derrota";
         texto_final_partida_Jugador.text = "Victoria"; }
        else
        {
            texto_final_partida_Enemigo.text = "Victoria";
            texto_final_partida_Jugador.text = "Derrota";
        }
        Activar.SetActive(true);
        ImagenesJugador.GetComponentInChildren<TextMeshPro>().text = mi_Seleccion.mi_poder.name;
        ImagenesRival.GetComponentInChildren<TextMeshPro>().text = seleccion_rival.mi_poder.name;
    }
    

    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
