using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emptyable : MonoBehaviour
{
    [SerializeField] GameObject[] _emptyable;

    public void SetEmpty(bool isEmpty)
    {
        foreach (var go in _emptyable)
        {
            go.SetActive(isEmpty);
        }
    }
}
