using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private GameObject _achievementsScreen;
    [SerializeField] private GameObject _collectionsScreen;

    private void OnEnable()
    {
        Statistics.OnStatsChanged += UpdateScreen;
        UpdateScreen();
        OnClose(_achievementsScreen);
        OnClose(_collectionsScreen);
    }

    private void OnDisable()
    {
        Statistics.OnStatsChanged -= UpdateScreen;
    }

    private void UpdateScreen()
    {
        _amount.SetText("{0}", GameMaster.Instance.Statistics.Gold);
    }

    public void OnClose(GameObject obj) => obj.SetActive(false);

    public void OnOpen(GameObject obj) => obj.SetActive(true);
}
