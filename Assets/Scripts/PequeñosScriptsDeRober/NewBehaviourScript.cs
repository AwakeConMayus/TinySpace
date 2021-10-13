using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    void Start()
    {
        Invoke("AutoDestruccion", 3f);
    }
   void AutoDestruccion()
    {
        Destroy(this.gameObject);
    }
}
