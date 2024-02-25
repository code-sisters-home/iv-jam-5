using InstantGamesBridge;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _gemsAmount;
    [SerializeField] private Image[] _lifes;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private Image _photo;
    [SerializeField] private GameObject _authBtn;

    private void OnEnable()
    {
        //юнити не гарантирует порядок выполнения методов лайфсайкла,
        //походу это тот случай, когда его надо контролировать
        StartCoroutine(Init());
    }

    private void OnDisable()
    {
        Statistics.OnStatsChanged -= UpdateScreen;
    }

    IEnumerator Init()
    {
        yield return new WaitUntil(() => GameMaster.Instance && GameMaster.Instance.IsReady);
        Statistics.OnStatsChanged += UpdateScreen;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        _amount.SetText("{0}", GameMaster.Instance.Statistics.Gold);
        //_gemsAmount.SetText("{0}", GameMaster.Instance.Statistics.Gems);

        for (int i = 0; i < _lifes.Length; i++)
        {
            _lifes[i].fillAmount = i <= GameMaster.Instance.Statistics.Lifes - 1 ? 1 : 0;
        }

        if(Bridge.player.isAuthorized)
        {
            _name.text = $"{ Bridge.player.name }";
            _id.text = $"{ Bridge.player.id}";

            if (Bridge.player.photos.Count > 0)
            {
                StartCoroutine(LoadPhoto(Bridge.player.photos[0]));
            }

            _authBtn.SetActive(false);
        }
        else
        {
            _authBtn.SetActive(true);
        }
    }
        private IEnumerator LoadPhoto(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                _photo.sprite = sprite;
            }
        }
}
