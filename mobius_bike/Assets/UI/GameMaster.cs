using CodeSisters.Logger;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Platform;
using InstantGamesBridge.Modules.Player;
using System;
using System.Collections;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }
    
    public Statistics Statistics { get; private set; }
    public DropGenerator DropGenerator { get; private set; }
    public UIMaster UIMaster { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public CameraSwitcher CameraSwitcher { get; private set; }

    public GameState CurrentGameState { get; private set; }
    public Loading Loading { get; private set; }
    public bool IsGameplay => CurrentGameState == GameState.gameplay;
    public bool IsPaused => CurrentGameState == GameState.pause;

    public bool IsReady { get; private set; }

    private IEnumerator Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            yield return null;
        }
        Instance = this;

        Loading = FindObjectOfType<Loading>(true);
        Loading.gameObject.SetActive(true);
        Loading.FadeIn();

        Statistics = GetComponentInChildren<Statistics>();
        Statistics.OnDied += () => ChangeState(GameState.menu);

        DropGenerator = GetComponentInChildren<DropGenerator>();
        UIMaster = GetComponentInChildren<UIMaster>();
        AudioManager = GetComponentInChildren<AudioManager>();
        CameraSwitcher = GetComponentInChildren<CameraSwitcher>();

        AudioManager = GetComponentInChildren<AudioManager>();
        AudioManager.Init();

        
        ChangeState(GameState.menu);
        //yield return new WaitForSeconds(1);
        Loading.FadeOut();

        Bridge.platform.SendMessage(PlatformMessage.GameReady);
        UnityLogger.Log($"GameReadyим п {DateTime.UtcNow}");

        //если будем релизиться не на яндекс, то вставить проверку Bridge.player.isAuthorizationSupported

        //if (Bridge.player.isAuthorizationSupported && !Bridge.player.isAuthorized)
        //{
        //    //откроется новое окно для ввода логина-пароля. если закрыть его, то придет OnAuthorizePlayerCompleted(false)
        //    var yandexScopes = true; 
        //    var yandexOptions = new AuthorizeYandexOptions(yandexScopes);
        //    Bridge.player.Authorize(OnAuthorizePlayerCompleted, yandexOptions);
        //}
        //else
        //{
        //    //UnityLogger.Log($"Player name {Bridge.player.name}");
        //    //if(Bridge.player.photos == null)
        //    //    UnityLogger.Log("Bridge.player.photos == null");
        //    //if (Bridge.player.photos != null)
        //    //    UnityLogger.Log($"Bridge.player.photos Count {Bridge.player.photos.Count}");
        //    //if(Bridge.player.photos.Count > 0)
        //    //    //возвращает путь к аватарке типа https://games-sdk.yandex.ru/games/api/sdk/v1/player/avatar/0/islands-retina-small
        //    //    UnityLogger.Log($"Player photo[0] {Bridge.player.photos[0]}");

        TryGetData();
        //}

    }

    public void Auth()
    {
        var yandexScopes = true;
        var yandexOptions = new AuthorizeYandexOptions(yandexScopes);
        Bridge.player.Authorize(OnAuthorizePlayerCompleted, yandexOptions);
    }

    private void OnAuthorizePlayerCompleted(bool success)
    {
        UnityLogger.Log($"OnAuthorize {success}");

        if (success)
        {
            TryGetData();
        }
        else
        {
           
        }
    }

    private void TryGetData()
    {
        Bridge.storage.Get("gold", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        // Загрузка произошла успешно
        if (success)
        {
            if (data != null)
            {
                UnityLogger.Log($"gold: {Bridge.storage.defaultType}");
                UnityLogger.Log($"gold data: {data}");
                if(Int32.TryParse(data, out int savedGold))
                {
                    GameMaster.Instance.Statistics.Gold = savedGold;
                    UnityLogger.Log($"gold parsed & runtime:{savedGold}, {GameMaster.Instance.Statistics.Gold}");
                }
            }
            else
            {
                UnityLogger.Log($"gold: {Bridge.storage.defaultType}");
                UnityLogger.Log($"gold: no data");
            }
        }
        else
        {
            // Ошибка, что-то пошло не так
            UnityLogger.Log($"OnStorageGetCompleted: Ошибка, что-то пошло не так");
        }

        IsReady = true;
        UnityLogger.Log($"IsReady {IsReady}");
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if(!focus)
    //    {
    //        UnityLogger.Log($"OnApplicationFocus: {focus}");
    //        Bridge.storage.Set("gold", GameMaster.Instance.Statistics.Gold, OnStorageSetCompleted);
    //    }
    //}

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }

    public void ChangeState(GameState state)
    {
        if (CurrentGameState == state)
            return;
        CurrentGameState = state;

        AudioManager.OnGameStateChanged(state);
        UIMaster.OnGameStateChanged(state);

        switch (state)
        {
            case GameState.gameplay:
                CameraSwitcher.SwitchToFirstPerCamera();
                break;
            case GameState.menu:
                CameraSwitcher.SwitchToFirstPerCamera();
                Statistics.GetLife(Statistics.MaxLifes);
                break;
            case GameState.pause:
                CameraSwitcher.SwitchToSelfieCamera();
                break;
        }
    }
}
