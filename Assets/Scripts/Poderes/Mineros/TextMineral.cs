using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMineral : MonoBehaviour
{
    [SerializeField]
    PoderMineros poder;
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
        print("actualizo");
        texto.text = "Mineral: " + poder.mineral;
    }

}
