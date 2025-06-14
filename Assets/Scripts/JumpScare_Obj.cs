using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class JumpScare_Obj : MonoBehaviour
{
  public AudioSource soundSource;
    public AudioClip scarySound;
    public bool takedamage;


    public GameObject Plane;

    bool isPlayed;

    private void Start()
    {
        isPlayed = false;
        soundSource.clip = scarySound;
        takedamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null && !takedamage)
            {
                stats.TakeDamage(10); // kaç can azaltmak istiyorsan
                takedamage=true;
            }

            StartCoroutine(scare());
        }
    }


    IEnumerator scare()
    {       
        {
            if (!isPlayed)
            {
                soundSource.Play();
                Plane.SetActive(true);
                isPlayed = true;
            }
        }

        yield return new WaitForSeconds(1);
        Plane.SetActive(false);
        //Destroy(gameObject);
    }

}
