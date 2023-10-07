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

        IsReady = true;
        ChangeState(GameState.menu);
        yield return new WaitForSeconds(1);
        Loading.FadeOut();
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
