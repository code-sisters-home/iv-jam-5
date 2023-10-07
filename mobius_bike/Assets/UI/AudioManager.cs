using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sounds;
    [SerializeField] private AudioSource _music;

    [SerializeField] private AudioClip menu_background;
    [SerializeField] private AudioClip gameplay_background;
    [SerializeField] private AudioClip button_click;
    [SerializeField] private AudioClip close_click;
    [SerializeField] private AudioClip get_reward;
    [SerializeField] private AudioClip collection_item_click;

    private Dictionary<SoundEvents, AudioClip> _clips = new Dictionary<SoundEvents, AudioClip>();
    public void Init()
    {
        _clips.Add(SoundEvents.menu_background, menu_background);    
        _clips.Add(SoundEvents.gameplay_background, gameplay_background);    
        _clips.Add(SoundEvents.button_click, button_click);    
        _clips.Add(SoundEvents.close_click, close_click);    
        _clips.Add(SoundEvents.get_reward, get_reward);    
        _clips.Add(SoundEvents.collection_item_click, collection_item_click);    
    }

    public void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.gameplay:
                PlayMusic(SoundEvents.gameplay_background);
                break;
            case GameState.menu:
                PlayMusic(SoundEvents.menu_background);
                break;
        }
    }

    public void PlaySound(SoundEvents soundEvent)
    {
        _sounds.clip = _clips[soundEvent];
        _sounds.Play();
    }
    
    public void PlayMusic(SoundEvents soundEvent)
    {
        _music.clip = _clips[soundEvent];
        if (soundEvent == SoundEvents.gameplay_background)
            _music.volume = 0.1f;
        if (soundEvent == SoundEvents.menu_background)
            _music.volume = 0.5f;
        _music.Play();
    }

    public void MuteSounds()
    {
        _sounds.Stop();
    }
}

public enum SoundEvents
{
    menu_background,
    gameplay_background,
    button_click,
    close_click,
    get_reward,
    collection_item_click
}
