using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Una referencia para la carga del scriptable object que funciona como singleton
/// (Carlos: Esto no me gusta, habria que buscar otra solucion)
/// </summary>
public class SingletonReferences : MonoBehaviour
{
    [SerializeField] PMasterManager _pMasterManager;
}
