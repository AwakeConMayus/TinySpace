using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuFinalParitda : MonoBehaviour
{

    [SerializeField] Text texto_final_partida;
    // Start is called before the first frame update
  

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

        if(aliado - enemigo > 0) texto_final_partida.text = "Victoria";
        else texto_final_partida.text = "Derrota";

    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        SceneManager.LoadScene(0);
    }
}
