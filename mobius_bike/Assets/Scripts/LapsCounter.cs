using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapsCounter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Untagged"))
            return;

        if (other.tag.Equals("lap"))
        {
            GameMaster.Instance.Statistics.IncreaseLaps();
            return;
        }

        if (other.tag.Equals("drop"))
        {
            var mushroom = other.gameObject.GetComponent<Mushroom>();
            if(mushroom != null)
            {
                GameMaster.Instance.Statistics.GetMushroom(mushroom);
            }
            StartCoroutine(PlaySoundAndDestroy(other.gameObject));
        }

        if (other.tag.Equals("sky_item"))
        {
            // GameMaster.Instance.Statistics.GetCollectionItem();
            Debug.Log("SKY ITEM");
            StartCoroutine(PlaySoundAndDestroy(other.gameObject));
        }

        if (other.tag.Equals("lightning"))
        {
            GameMaster.Instance.Statistics.GetDamage();
            StartCoroutine(PlaySoundAndDestroy(other.gameObject));
        }

    }

    IEnumerator PlaySoundAndDestroy(GameObject other)
    {
        var audio = other.GetComponent<AudioSource>();
        if (audio == null)
            Debug.LogError($"{other.name} no audio");
        else
        {
            //Debug.Log($"{other.name} {audio.clip.name}");
            audio.Play();
        }
        yield return new WaitForSeconds(1);
        Destroy(other);
    }
}
