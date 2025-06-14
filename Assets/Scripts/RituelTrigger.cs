using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RituelTrigger : MonoBehaviour
{
    public GameObject rituelMerkezi;
    public GameObject canavar;
    public GameObject arabaAnahtari;
    public TextMeshProUGUI uyariText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Canavar"))
        {
            // Canavar� devre d��� b�rak
            canavar.SetActive(false);

            rituelMerkezi.SetActive(false);

            // Anahtar� ortaya ��kar
            arabaAnahtari.SetActive(true);

            // Uyar� mesaj�n� g�ster
            uyariText.text = "Canavar merkeze �ekildi! Anahtar ortaya ��kt�." +
                "art�k taksiyle burdan gidebilirim";
            CancelInvoke("UyariyiKapat");
            Invoke("UyariyiKapat", 4f); 
        }
    }


    void UyariyiKapat()
    {
        uyariText.text = "";
    }
}
