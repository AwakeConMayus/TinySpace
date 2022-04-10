using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aviso : MonoBehaviour
{
    [SerializeField] float minTime, maxTime;
    float timer = 0;
    [SerializeField] GameObject pulsaParaAvanzar;
    private void Start()
    {
        if (pulsaParaAvanzar) pulsaParaAvanzar.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > minTime && Input.anyKeyDown || timer > maxTime)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (timer > minTime && pulsaParaAvanzar) pulsaParaAvanzar.SetActive(true);
    }
}
