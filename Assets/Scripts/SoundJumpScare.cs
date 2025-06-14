using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundJumpScare : MonoBehaviour
{
    public AudioSource source; //ses kaynaðý
    public AudioClip screamSound; //ses

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = screamSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(10);
                StartCoroutine(playSound());
            }


        }
    }


    IEnumerator playSound()
    {
        source.Play();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        print("Ses oynatýldý");
    }
}
