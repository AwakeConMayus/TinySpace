using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    Text texto;
    [SerializeField] GameSettings setings;
    

    private void Start()
    {
        texto = GetComponent<Text>();
        texto.text = "V." + setings.gameVersion;
    }
}
