using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMineral : MonoBehaviour
{
    [SerializeField]
    OpcionesMineros poder;
    Text texto;

    private void Awake()
    {
        texto = GetComponent<Text>();
    }

    private void Start()
    {
        ActualizarTextoMineral();
        EventManager.StartListening("CambioMineral", ActualizarTextoMineral);
    }

    public void ActualizarTextoMineral()
    {
        texto.text = "Mineral: " + poder.mineral;
    }

}
