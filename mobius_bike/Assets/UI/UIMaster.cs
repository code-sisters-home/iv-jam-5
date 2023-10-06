using UnityEngine;
using UnityEngine.U2D;

public class UIMaster : MonoBehaviour
{
    public static UIMaster Instance { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public SpriteAtlas SpriteAtlas;
    public Canvas Menu;
    public Canvas Hud;

    public GameState CurrentGameState { get; private set; }
    public bool IsMenu => CurrentGameState == GameState.menu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        AudioManager = GetComponentInChildren<AudioManager>();
        AudioManager.Init();

        ChangeState(GameState.menu);
    }

    public void ChangeState(GameState state)
    {
        if (CurrentGameState == state)
            return;
        CurrentGameState = state;

        switch (state)
        {
            case GameState.gameplay:
                AudioManager.PlayMusic(SoundEvents.gameplay_background);
                Menu.gameObject.SetActive(false);
                Hud.gameObject.SetActive(true);
                break;
            case GameState.menu:
                AudioManager.PlayMusic(SoundEvents.menu_background);
                Menu.gameObject.SetActive(true);
                Hud.gameObject.SetActive(false);
                break;
        }
    }

    public void StartGame() => ChangeState(GameState.gameplay);
    public void Exit() => ChangeState(GameState.menu);
}

public enum GameState
{
    gameplay,
    menu
}