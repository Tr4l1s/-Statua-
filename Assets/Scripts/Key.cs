using UnityEngine;

public class Key : MonoBehaviour
{
    public EleAlma eleAlmaScript; // EleAlma script'ine referans

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kap�n�n kilidini a�
            eleAlmaScript.isLocked = false;

            // Anahtar� yok et
            Destroy(gameObject);
        }
    }
}