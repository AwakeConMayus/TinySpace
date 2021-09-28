using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object encargado de guardar los game settings
/// </summary>

[CreateAssetMenu(menuName = "Singleton/PMasterManager")]
public class PMasterManager : SingletonScriptableObject<PMasterManager>
{
    [SerializeField]
    GameSettings _gameSettings;
    public static GameSettings gameSettings { get { return Instance._gameSettings; } }

}
