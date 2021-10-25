using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] GameObject Info;
  public void ActivarInfo()
    {
        if (Info.activeInHierarchy == true)
        {
            Info.SetActive(false);
        }
        else
            Info.SetActive(true);
    }
}
