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
        Debug.Log(fillAliado.GetComponent<RectTransform>().sizeDelta);
    }

    public void Update_Score()
    {
        int[] aux_puntuacion = Tablero.instance.RecuentoPuntos();

        if(InstancePiezas.instance.jugador == 0)
        {
            scoreAliado.text = aux_puntuacion[0].ToString();
            scoreEnemigo.text = aux_puntuacion[1].ToString();
            Debug.Log("soy el primero");
            FillRects(aux_puntuacion[0], aux_puntuacion[1]);
        }
        else
        {
            scoreAliado.text = aux_puntuacion[1].ToString();
            scoreEnemigo.text = aux_puntuacion[0].ToString();
            Debug.Log("soy el segundo");
            FillRects(aux_puntuacion[1], aux_puntuacion[0]);
        }

    }

    public void FillRects(int puntuacion_aliada, int puntuacion_enemiga)
    {
        puntuacion_aliada += 10;
        puntuacion_enemiga += 10;
         int aux_puntuaciones = puntuacion_aliada + puntuacion_enemiga;
        Debug.Log(aux_puntuaciones);
        Debug.Log(puntuacion_aliada);
        Debug.Log(((puntuacion_aliada / (float)aux_puntuaciones)) * 500);
        fillAliado.GetComponent<RectTransform>().sizeDelta = new Vector2(fillAliado.GetComponent<RectTransform>().sizeDelta.x , ((puntuacion_aliada / (float)aux_puntuaciones) * 500));
        fillEnemigo.GetComponent<RectTransform>().sizeDelta = new Vector2(fillEnemigo.GetComponent<RectTransform>().sizeDelta.x, 500 - ((puntuacion_aliada / (float)aux_puntuaciones) * 500));
    }
}
