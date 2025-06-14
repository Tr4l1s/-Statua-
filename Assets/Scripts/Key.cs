using UnityEngine;

public class Key : MonoBehaviour
{
    public EleAlma eleAlmaScript; // EleAlma script'ine referans

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kapýnýn kilidini aç
            eleAlmaScript.isLocked = false;

            // Anahtarý yok et
            Destroy(gameObject);
        }
    }
}