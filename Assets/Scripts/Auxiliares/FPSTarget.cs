using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTarget : MonoBehaviour
{
    //* Mantiene el juego a 30fps, principalmente para reducir el consumo de recursos
    //* Teniendo en cuenta además que el juego no tiene mucho movimiento

    [SerializeField]
    private int targetFPS = 30;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;

        Screen.SetResolution(1920, 1080, true);
    }

    void Update()
    {
        if (Application.targetFrameRate != targetFPS)
            Application.targetFrameRate = targetFPS;
    }

    void setFPS(int t) { targetFPS = t; }
    int  getFPS() { return targetFPS; }

}
