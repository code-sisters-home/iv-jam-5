using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite2DClickDetector : MonoBehaviour
{
    void OnMouseDown()
    {
        GameMaster.Instance.UIMaster.OpenCollection();
    }
}
