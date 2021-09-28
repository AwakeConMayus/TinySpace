using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] GameObject createRoomCanvas;
    [SerializeField] GameObject onJoinedRoomCanvas;

    [SerializeField] Text roomName;

    public void Hide_CreateRoomCanvas(string _roomName)
    {
        createRoomCanvas.SetActive(false);
        onJoinedRoomCanvas.SetActive(true);
        roomName.text = _roomName;
    }

    public void Hide_OnJoinedRoomCanvas()
    {
        onJoinedRoomCanvas.SetActive(false);
        createRoomCanvas.SetActive(true);
    }
}
