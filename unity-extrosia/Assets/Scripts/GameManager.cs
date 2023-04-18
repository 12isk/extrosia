using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(102.0242f,0.4f,48.54696f),Quaternion.identity,0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    public void OnPlayerEnterRoom(Player other)
    {
        print(other.NickName + " s'est connecté");
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        print(other.NickName + " s'est déconnecté");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MyLauncherScene");
    }
    public void LeaveRooms()
    {
        PhotonNetwork.LeaveRoom();

    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
