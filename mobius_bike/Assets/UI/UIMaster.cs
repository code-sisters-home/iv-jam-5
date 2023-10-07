using UnityEngine;
using UnityEngine.U2D;

public class UIMaster : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _spriteAtlas;

    [Header("Canvases")]
    [SerializeField] private Canvas _menu;
    [SerializeField] private Canvas _hud;

    [Header("Screens")]
    [SerializeField] private GameObject _achievementsScreen;
    [SerializeField] private GameObject _collectionsScreen;

    public void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.gameplay:
                _menu.gameObject.SetActive(false);
                _hud.gameObject.SetActive(true);
                break;
            case GameState.menu:
                _menu.gameObject.SetActive(true);
                _hud.gameObject.SetActive(false);
                OnClose(_achievementsScreen);
                OnClose(_collectionsScreen);
                break;
        }
    }

    public void StartGame() => GameMaster.Instance.ChangeState(GameState.gameplay);
    public void Exit() => GameMaster.Instance.ChangeState(GameState.menu);

    public void OnClose(GameObject obj) => obj.SetActive(false);

    public void OnOpen(GameObject obj) => obj.SetActive(true);

    public Sprite GetSprite(string spriteName) => _spriteAtlas.GetSprite(spriteName);
}
