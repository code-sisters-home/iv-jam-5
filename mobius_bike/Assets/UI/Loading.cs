using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float speed = 10;

    private float _targetAlpha = 1;

    private void Awake()
    {
        _canvasGroup.alpha = 1;
    }

    public void FadeIn() 
    {
        _targetAlpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public void FadeOut() 
    { 
        _targetAlpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Mathf.Approximately(_canvasGroup.alpha, _targetAlpha))
            return;

        if(_targetAlpha == 0)
            _canvasGroup.alpha -= Time.deltaTime * speed;
        else
            _canvasGroup.alpha += Time.deltaTime * speed;
    }
}
