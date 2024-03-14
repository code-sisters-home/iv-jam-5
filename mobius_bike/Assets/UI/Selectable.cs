using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    [SerializeField] GameObject[] _selectables;

    public void SetSelected(bool isSelected)
    {
        foreach (var go in _selectables)
        {
            go.SetActive(isSelected);
        }
    }

}
