using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundEvent : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SoundEvents _soundEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameMaster.Instance.AudioManager.PlaySound(_soundEvent);
    }
}
