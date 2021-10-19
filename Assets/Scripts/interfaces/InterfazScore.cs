using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazScore : MonoBehaviour
{
    [SerializeField] Text scoreAliado;
    [SerializeField] Text scoreEnemigo;
    [SerializeField] GameObject fillEnemigo;
    [SerializeField] GameObject fillAliado;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening("Siguiente_turno", Update_Score);
        EventManager.StartListening("UpdateScore", Update_Score);
    }

    public void Update_Score()
    {
        int[] aux_puntuacion = Tablero.instance.RecuentoPuntos();

        if(InstancePiezas.instance.jugador == 0)
        {
            scoreAliado.text = aux_puntuacion[0].ToString();
            scoreEnemigo.text = aux_puntuacion[1].ToString();
            FillRects(aux_puntuacion[0], aux_puntuacion[1]);
        }
        else
        {
            scoreAliado.text = aux_puntuacion[1].ToString();
            scoreEnemigo.text = aux_puntuacion[0].ToString();
            FillRects(aux_puntuacion[1], aux_puntuacion[0]);
        }

    }

    public void FillRects(int puntuacion_aliada, int puntuacion_enemiga)
    {
        int colchon = 2;
        if (puntuacion_aliada != 0 && puntuacion_enemiga != 0) colchon = 0;

        puntuacion_aliada += colchon;
        puntuacion_enemiga += colchon;
        int aux_puntuaciones = puntuacion_aliada + puntuacion_enemiga;
        fillAliado.GetComponent<BarraScoreAnim>().SetTargetSize((puntuacion_aliada / (float)aux_puntuaciones) * 500);
    }
}
