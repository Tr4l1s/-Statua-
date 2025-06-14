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
            // Canavarý devre dýþý býrak
            canavar.SetActive(false);

            rituelMerkezi.SetActive(false);

            // Anahtarý ortaya çýkar
            arabaAnahtari.SetActive(true);

            // Uyarý mesajýný göster
            uyariText.text = "Canavar merkeze çekildi! Anahtar ortaya çýktý." +
                "artýk taksiyle burdan gidebilirim";
            CancelInvoke("UyariyiKapat");
            Invoke("UyariyiKapat", 4f); 
        }
    }


    void UyariyiKapat()
    {
        uyariText.text = "";
    }
}
