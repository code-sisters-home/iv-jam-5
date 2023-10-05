using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLaps : MonoBehaviour
{
    public Collider point;
    public AudioClip mushroomAudioClip;
    public AudioClip boxAudioClip;
    public AudioClip lightningAudioClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (point == null)
            return;
        if(other == point)
            GameMaster.Instance.Statistics.IncreaseLaps();

        if (other.tag.Equals("drop"))
        {
            GameMaster.Instance.Statistics.GetCollectionItem();

            audioSource.clip = mushroomAudioClip;
            audioSource.Play();

            Destroy(other.gameObject);
        }

        if (other.tag.Equals("sky_item"))
        {
            // GameMaster.Instance.Statistics.GetCollectionItem();
            Debug.Log("SKY ITEM");

            audioSource.clip = boxAudioClip;
            audioSource.Play();

            Destroy(other.gameObject);
        }

        if (other.tag.Equals("lightning"))
        {
            GameMaster.Instance.Statistics.GetDamage();
            Destroy(other.gameObject);

            audioSource.clip = lightningAudioClip;
            audioSource.Play();
        }
    }
}
