using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ListRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    RoomListing roomListPrefab;

    [SerializeField]
    Transform content;

    private List<RoomListing> _roomList = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
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
