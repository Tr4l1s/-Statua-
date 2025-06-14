using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindAudioTrigger : MonoBehaviour
{
    public AudioSource windAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && windAudio != null)
        {
            windAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && windAudio != null)
        {
            windAudio.Stop();
        }
    }
}
