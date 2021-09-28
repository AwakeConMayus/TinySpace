using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


/// <summary>
/// Script de UI para el listado de habitaciones ya creadas
/// </summary>
public class ListRoom : MonoBehaviourPunCallbacks
{
    //Prefab etiqueta de habitacion
    [SerializeField]
    RoomListing roomListPrefab;

    //GameObject padre de las etiquetas
    [SerializeField]
    Transform content;

    //Lista de las habitaciones en activo
    private List<RoomListing> _roomList = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Por cada informacion de habitacion de la lista que tiene el servidor
        foreach(RoomInfo info in roomList)
        {
            //Primero se comprueba si se ha destruido esta habitacion
            //Si es asi se busca la etiqueta correspondiente y se borra
            if (info.RemovedFromList)
            {
                foreach (RoomListing r in _roomList)
                {
                    if (r.roomInfo.Name == info.Name)
                    {
                        _roomList.Remove(r);
                        Destroy(r);
                    }
                }
            }
            //Si en cambio ha sido creada se crea una nueva etiqueta
            else
            {
                RoomListing listing = Instantiate(roomListPrefab, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    _roomList.Add(listing);
                }
            }
        }
    }
}
