using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/PMasterManager")]
public class PMasterManager : SingletonScriptableObject<PMasterManager>
{
    [SerializeField]
    GameSettings _gameSettings;
    public static GameSettings gameSettings { get { return Instance._gameSettings; } }

}
