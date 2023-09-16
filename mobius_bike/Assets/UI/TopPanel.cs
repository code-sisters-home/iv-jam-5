using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private GameObject _achievementsScreen;

    private void OnEnable()
    {
        Statistics.OnStatsChanged += UpdateScreen;
        UpdateScreen();
        OnClose();
    }

    private void OnDisable()
    {
        Statistics.OnStatsChanged -= UpdateScreen;
    }

    private void UpdateScreen()
    {
        _amount.SetText("{0}", GameMaster.Instance.Statistics.Gold);
    }

    public void OnClose()
    {
        _achievementsScreen.SetActive(false);
    }

    public void OpenAchievements()
    {
        _achievementsScreen.SetActive(true);
    }
}
