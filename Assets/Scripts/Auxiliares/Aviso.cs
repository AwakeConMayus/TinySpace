using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aviso : MonoBehaviour
{
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2 && Input.anyKeyDown || timer > 7)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
