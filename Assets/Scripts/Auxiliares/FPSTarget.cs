//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FPSTarget : MonoBehaviour
{
    //* Mantiene el juego a 30fps, principalmente para reducir el consumo de recursos
    //* Teniendo en cuenta además que el juego no tiene mucho movimiento

    [SerializeField]
    private int targetFPS = 30;
    [SerializeField]
    private int width = 1920;
    [SerializeField]
    private int height = 1080;
    [SerializeField]
    private bool fullscreen = true;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;

        Screen.SetResolution(width, height, fullscreen);
    }

    void Update()
    {
        if (Application.targetFrameRate != targetFPS)
            Application.targetFrameRate = targetFPS;
    }

    void setFPS(int t) { targetFPS = t; }
    int  getFPS() { return targetFPS; }

    void setResolution(int w, int h)
    {
        width = w;
        height = h;

        ChangeScreen();
    }
    int[] getResolution()
    {
        int[] resolution = { width, height };
        return resolution;
    }

    void setFullScreen(bool s) 
    { 
        fullscreen = s;
        ChangeScreen();
    }
    bool getFullScreen() { return fullscreen; }

    void ChangeScreen()
    {
        Screen.SetResolution(width, height, fullscreen);
    }
}
