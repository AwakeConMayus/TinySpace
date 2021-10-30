﻿using System.Collections;
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
        //Hay que adaptar el final de aprtida al nuevo sistema sin jugadores por facciones VVV
        if(InstancePiezas.instance.faccion == 0)
        {
            if (puntos[0] - puntos[1] > 0) texto_final_partida.text = "Victoria";
            else texto_final_partida.text = "Derrota";
        }
        else
        {
            if (puntos[0] - puntos[1] > 0) texto_final_partida.text = "Derrota";
            else texto_final_partida.text = "Victoria";
        }
        //   ^^^
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
