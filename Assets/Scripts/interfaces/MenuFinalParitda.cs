using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuFinalParitda : MonoBehaviour
{

    [SerializeField] Text texto_final_partida;
    // Start is called before the first frame update
  

    public void Final_Partida(int[] puntos)
    {
    
        if(InstancePiezas.instance.jugador == 0)
        {
            if (puntos[0] - puntos[1] > 0) texto_final_partida.text = "Victoria";
            else texto_final_partida.text = "Derrota";
        }
        else
        {
            if (puntos[0] - puntos[1] > 0) texto_final_partida.text = "Derrota";
            else texto_final_partida.text = "Victoria";
        }
    }
    public void Salir()
    {
        SceneManager.LoadScene(0);
    }
}
