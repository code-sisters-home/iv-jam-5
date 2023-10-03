using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour
{
    [SerializeField] private CatmullRomSpline _path;
    [SerializeField] private GameObject _dropItem;

    public void DropSomething()
    {
        //max is exclusive
        var pos = _path.controlPointsList[UnityEngine.Random.Range(0, _path.controlPointsList.Length - 1)];
        var item = Instantiate(_dropItem, pos.position, Quaternion.identity, transform);
    }

    long currentTime;
    float timer = 1;
    int waitTime = 2;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            DropSomething();
            timer = 0;
        }
    }
}
