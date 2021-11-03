using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAtoB : MonoBehaviour
{
    [SerializeField] Vector3 punto1;
    [SerializeField] Vector3 punto2;
    float speed = -0.1f;
    Vector3 direccion;
    // Start is called before the first frame update
    void Start()
    {
        direccion =(punto1- punto2);
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoOnda();
    }
    void MovimientoOnda()
    {
        
            this.transform.Translate(direccion * speed * Time.deltaTime); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.collider.tag == "direccion")
        {
            print("me choque");
            speed = speed * -1;
        }
    }
}
