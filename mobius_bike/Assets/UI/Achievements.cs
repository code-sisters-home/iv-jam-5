using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Achievement _prefab;

    private List<Achievement> achievements = new List<Achievement>();

    private void OnEnable()
    {
        Statistics.OnAchievementsChanged += UpdateScreen;
        UpdateScreen();
    }

    private void OnDisable()
    {
        Statistics.OnAchievementsChanged -= UpdateScreen;
    }

    private void UpdateScreen()
    {
        for (int i = achievements.Count - 1; i >= 0; i--)
        {
            Destroy(achievements[i].gameObject);
        }

        achievements.Clear();

        for (int i = 0; i < GameMaster.Instance.Statistics.achievementDatas.Count; i++)
        {
            var achievement = Instantiate(_prefab, _parent, false);
            achievement.Init(GameMaster.Instance.Statistics.achievementDatas[i]);
            achievements.Add(achievement);
        }
    }
}
