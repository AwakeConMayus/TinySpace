using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Scriptable con las opciones de juego establecidas
/// </summary>
[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    //La version del juego 
    [SerializeField] string _gameVersion = "0.0.0";
    public string gameVersion { get { return _gameVersion; } }
    //Nickname dek jugador, aleatorio por el momento
    [SerializeField] string _nickName = "Punfish";
    public string nickName
    {
        get
        {
            int value = Random.Range(0, 999);
            return _nickName + value.ToString();
        }
    }
}
