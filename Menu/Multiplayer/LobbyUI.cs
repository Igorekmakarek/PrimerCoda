using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public static LobbyUI instance;

    private Image JoinLobbyImage;
    private Image CreateLobbyImage;

    private Image CreateButtonImage;
    private Text CreateButtonText;

    private Text CreateServerName;
    private Image CreatedName;
    private Text CreatedNameText;
    private Text NumberOfPlayers;
    private Image Players;
    private Text PlayersNumber;

    private bool ShouldOpen;
    private bool JoinLobby;
    private bool CreateLobby;

    private void Start()
    {
        if (instance == null)
            instance = this;

        CreateButtonImage = GameObject.Find("CreateButton").GetComponent<Image>();
        CreateButtonText = CreateButtonImage.transform.GetChild(0).GetComponent<Text>();

        CreateServerName = GameObject.Find("CreateServerName").GetComponent<Text>();
        CreatedName = GameObject.Find("CreatedName").GetComponent<Image>();
        CreatedNameText = CreatedName.transform.GetChild(0).GetComponent<Text>();
        NumberOfPlayers = GameObject.Find("NumberOfPlayers").GetComponent<Text>();
        Players = GameObject.Find("Players").GetComponent<Image>();
        PlayersNumber = Players.transform.GetChild(0).GetComponent<Text>();



        JoinLobbyImage = GameObject.Find("JoinWindow").GetComponent<Image>();
        CreateLobbyImage = GameObject.Find("CreateWindow").GetComponent<Image>();
        GameObject.Find("JoinLobby").GetComponent<Button>().onClick.AddListener(OpenOrCloseJoinLobby);
        GameObject.Find("CreateLobby").GetComponent<Button>().onClick.AddListener(OpenOrCloseCreateLobby);
        ShouldOpen = true;
        JoinLobby = false;
        CreateLobby = false;

        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OpenOrCloseWindow);
    }

    public void OpenOrCloseWindow()
    {
        transform.GetComponent<Image>().enabled = ShouldOpen;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Text>() != null)
                transform.GetChild(i).GetComponent<Text>().enabled = ShouldOpen;
            if (transform.GetChild(i).GetComponent<Image>() != null)
                transform.GetChild(i).GetComponent<Image>().enabled = ShouldOpen;

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                if (transform.GetChild(i).GetChild(j).GetComponent<Text>() != null)
                    transform.GetChild(i).GetChild(j).GetComponent<Text>().enabled = ShouldOpen;
                if (transform.GetChild(i).GetChild(j).GetComponent<Image>() != null)
                    transform.GetChild(i).GetChild(j).GetComponent<Image>().enabled = ShouldOpen;

                for (int k = 0; k < transform.GetChild(i).GetChild(j).childCount; k++)
                {
                    if (transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<Text>() != null)
                        transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<Text>().enabled = ShouldOpen;
                    if (transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<Image>() != null)
                        transform.GetChild(i).GetChild(j).GetChild(k).GetComponent<Image>().enabled = ShouldOpen;
                }
            }
        }

        JoinLobby = false;
        CreateLobby = false;
        OpenOrCloseCreateLobby();
        OpenOrCloseJoinLobby();

        
        ShouldOpen = !ShouldOpen;
    }

    private void OpenOrCloseJoinLobby()
    {
        JoinLobbyImage.enabled = JoinLobby;

        for (int i = 0; i < JoinLobbyImage.transform.childCount; i++)
        {
            if (JoinLobbyImage.transform.GetChild(i).GetComponent<Text>() != null)
                JoinLobbyImage.transform.GetChild(i).GetComponent<Text>().enabled = JoinLobby;
            if (JoinLobbyImage.transform.GetChild(i).GetComponent<Image>() != null)
                JoinLobbyImage.transform.GetChild(i).GetComponent<Image>().enabled = JoinLobby;

            for (int j = 0; j < JoinLobbyImage.transform.GetChild(i).childCount; j++)
            {
                if (JoinLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Text>() != null)
                    JoinLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Text>().enabled = JoinLobby;
                if (JoinLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Image>() != null)
                    JoinLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Image>().enabled = JoinLobby;
            }
        }
        JoinLobby = !JoinLobby;
    }

    private void OpenOrCloseCreateLobby()
    {
        CreateLobbyImage.enabled = CreateLobby;

        for (int i = 0; i < CreateLobbyImage.transform.childCount; i++)
        {
            if (CreateLobbyImage.transform.GetChild(i).GetComponent<Text>() != null)
                CreateLobbyImage.transform.GetChild(i).GetComponent<Text>().enabled = CreateLobby;
            if (CreateLobbyImage.transform.GetChild(i).GetComponent<Image>() != null)
                CreateLobbyImage.transform.GetChild(i).GetComponent<Image>().enabled = CreateLobby;

            for (int j = 0; j < CreateLobbyImage.transform.GetChild(i).childCount; j++)
            {
                if (CreateLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Text>() != null)
                    CreateLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Text>().enabled = CreateLobby;
                if (CreateLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Image>() != null)
                    CreateLobbyImage.transform.GetChild(i).GetChild(j).GetComponent<Image>().enabled = CreateLobby;
            }
        }
        CreateLobby = !CreateLobby;
    }

    public void LobbyCreated (string RoomName)
    {
        CreateButtonImage.enabled = false;
        CreateButtonText.enabled = false;
        CreateServerName.text = "Название комнаты: " + RoomName;
        CreatedName.enabled = false;
        CreatedNameText.enabled = false;
        NumberOfPlayers.enabled = false;
        Players.enabled = false;
        PlayersNumber.enabled = false;

    }

}
