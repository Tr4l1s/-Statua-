using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlitchEffectController : MonoBehaviour
{
    public PostProcessVolume volume;

    private Grain grain;
    private Vignette vignette;

    private PlayerStats playerStats;
    public Transform playerTransform;
    public Transform enemyTransform;
    public float enemyCloseDistance = 10f;



    private void Awake()
    {
        if (volume == null)
        {
            volume = GetComponent<PostProcessVolume>();
            if (volume == null)
            {
                volume = FindObjectOfType<PostProcessVolume>();
            }
        }

        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGetSettings(out grain);
            volume.profile.TryGetSettings(out vignette);
        }

        playerStats = FindObjectOfType<PlayerStats>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        DisableGlitch();
    }


    private void Update()
    {
        if (playerStats == null || grain == null || vignette == null || playerTransform == null || enemyTransform == null)
            return;

        float canOrani = 1f - ((float)playerStats.currentHealth / playerStats.maxHealth); // can azal�nca artan etki

        float mesafe = Vector3.Distance(playerTransform.position, enemyTransform.position);
        float mesafeOrani = Mathf.Clamp01(1f - (mesafe / enemyCloseDistance)); // d��man yakla�t�k�a 1'e yakla��r

        // �ki etkiyi birle�tir (max 1 olacak �ekilde)
        float toplamEtki = Mathf.Clamp01(canOrani + mesafeOrani);
       
        // Efektleri uygula
        grain.intensity.value = Mathf.Lerp(0.5f, 2f, toplamEtki); // daha belirgin kar�ncalanma
        grain.size.value = 1f; // daha k���k tanecikler (yo�un g�r�n�m)
        grain.colored.value = false; // siyah-beyaz grain daha net g�r�n�r

        vignette.intensity.value = Mathf.Lerp(0.2f, 0.5f, toplamEtki);

    }



    public void EnableGlitch()
    {
        if (grain != null) grain.intensity.value = 1f;
        if (vignette != null) vignette.intensity.value = 0.45f;
    }

    public void DisableGlitch()
    {
        if (grain != null) grain.intensity.value = 0f;
        if (vignette != null) vignette.intensity.value = 0.2f;
    }
}

