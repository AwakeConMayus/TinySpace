using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Hide_CreateRoomCanvas()
    {
        createRoomCanvas.SetActive(false);
        onJoinedRoomCanvas.SetActive(true);
    }

    public void Hide_OnJoinedRoomCanvas()
    {
        onJoinedRoomCanvas.SetActive(false);
        createRoomCanvas.SetActive(true);
    }
}
