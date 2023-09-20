using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private TextMeshProUGUI _rewardAmount;
    [SerializeField] private Button _getRewardsBtn;

    private AchievementData _data;

    internal void Init(AchievementData data)
    {
        _data = data;
        _name.SetText(data.Text);
        _counter.SetText("{0}/{1}", data.CurrentAmount, data.MaxAmount);
        _rewardAmount.SetText("Награда\n{0} золота", data.Reward);

        _getRewardsBtn.gameObject.SetActive(data.CurrentAmount >= data.MaxAmount);
    }

    public void GetReward()
    {
        GameMaster.Instance.Statistics.GetReward(_data);
    }
}

public class AchievementData
{
    public string Text;
    public int CurrentAmount;
    public int MaxAmount;
    public int Reward;

    public AchievementData(string text, int maxAmount, int reward)
    {
        Text = text;
        CurrentAmount = 0;
        MaxAmount = maxAmount;
        Reward = reward;
    }
}
