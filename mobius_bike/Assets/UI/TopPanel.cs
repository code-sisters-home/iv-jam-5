using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _gemsAmount;
    [SerializeField] private Image[] _lifes;
    
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
        _gemsAmount.SetText("{0}", GameMaster.Instance.Statistics.Gems);

        for (int i = 0; i < _lifes.Length; i++)
        {
            _lifes[i].fillAmount = i <= GameMaster.Instance.Statistics.Lifes - 1 ? 1 : 0;
        } 
    }
}
