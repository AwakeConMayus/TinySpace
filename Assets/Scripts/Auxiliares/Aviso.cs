using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aviso : MonoBehaviour
{
    float timer = 0;
    [SerializeField] GameObject pulsaParaAvanzar;
    private void Start()
    {
        if (pulsaParaAvanzar) pulsaParaAvanzar.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2 && Input.anyKeyDown || timer > 7)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (timer > 2 && pulsaParaAvanzar) pulsaParaAvanzar.SetActive(true);
    }
}
