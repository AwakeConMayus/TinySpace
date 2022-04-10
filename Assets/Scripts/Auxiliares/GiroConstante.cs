using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroConstante : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool sentido;

    private void Start()
    {
        if (sentido) speed *= -1;
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
