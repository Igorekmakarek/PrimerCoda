
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;

    public Text LogText;
    public Text JoinLogText;

    private int numberOfHostMessages;
    private int numberOfPlayerMessages;


    public InputField nickName;
    public InputField numberOfPlayers;
    public InputField createInput;
    public InputField joinInput;


    public void Start()
    {
        if (instance == null)
            instance = this;

        nickName.text = PlayerPrefs.GetString("NickName");
        PhotonNetwork.NickName = nickName.text;
        HostLog("Ну привет, " + PhotonNetwork.NickName + "!");
        PlayerLog("Ну привет, " + PhotonNetwork.NickName + "!");


        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        HostLog("Все в порядке, можно играть!");
        PlayerLog("Все в порядке, можно играть!");

    }

    public void ChangeNickName()
    {
        PhotonNetwork.NickName = nickName.text;
        HostLog("Ваш никнейм теперь " + PhotonNetwork.NickName);
        PlayerLog("Ваш никнейм теперь " + PhotonNetwork.NickName);
        PlayerPrefs.SetString("NickName", nickName.text);
    }


    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();


        int number;

        bool success = int.TryParse(numberOfPlayers.text, out number);
        if (success)
        {
            if (number >= 2 && number <= 4)
            {
                roomOptions.MaxPlayers = (byte)number;
                HostLog("Создана комната на " + number + " игроков");
            }
            else
            {
                HostLog("Вы ввели неверное количество игроков.");
                return;
            }
        }
        else
        {
            HostLog("Вы ввели неверное количество игроков.");
            return;
        }

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);        //создание комнаты с названием которое выбрал игрок

        LobbyUI.instance.LobbyCreated(createInput.text);

        HostLog("Вы создали комнату с именем " + createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public void LeaveRoom()
    {
        
        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom()
    {
        HostLog(PhotonNetwork.NickName + " покинул нас");
        PlayerLog(PhotonNetwork.NickName + " покинул нас");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PlayerLog("Комната не найдена. Проверьте стабильность интернет- соединения.");
    }

    public override void OnJoinedRoom()
    {
        HostLog(PhotonNetwork.NickName + " подключился к комнате");
        PlayerLog(PhotonNetwork.NickName + " подключился к комнате");

        // SceneManager.LoadScene("FeastWorld");

    }

    

    private void HostLog(string message)
    {
        numberOfHostMessages++;
        if (numberOfHostMessages > 5)
            JoinLogText.text = "";

        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;

    }

    private void PlayerLog(string message)
    {
        numberOfPlayerMessages++;
        if (numberOfPlayerMessages > 5)
            JoinLogText.text = "";

        Debug.Log(message);
        JoinLogText.text += message;
        JoinLogText.text += "\n";
        
    }


}
