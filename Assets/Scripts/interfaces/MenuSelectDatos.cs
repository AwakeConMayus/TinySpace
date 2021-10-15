//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelectDatos : MonoBehaviour
{

    public void EnterGame()
    {
        SceneManager.LoadScene(2);
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}
