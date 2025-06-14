using System.Collections;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;
    private bool isOn;

    public AudioSource audioSource;
    public AudioClip fenerSes;

    private Coroutine flickerRoutine;

    [Header("Titreme Ayarlarý")]
    public bool enableFlicker = true;
    public float flickerMinTime = 4f;  // Inspector’dan ayarlanabilir
    public float flickerMaxTime = 10f;

    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = fenerSes;

        if (enableFlicker)
        {
            flickerRoutine = StartCoroutine(FlickerEffect());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            audioSource.Play();

            if (isOn)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }
            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }

            isOn = !isOn;
        }
    }

    private IEnumerator FlickerEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(flickerMinTime, flickerMaxTime));

            if (isOn)
            {
                int flickerCount = Random.Range(2, 5);
                for (int i = 0; i < flickerCount; i++)
                {
                    ON.SetActive(false);
                    OFF.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

                    ON.SetActive(true);
                    OFF.SetActive(false);
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                }
            }
        }
    }
}
