using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public List<GameObject> lowHealthJumpScares;
    private bool lowHealthActive = false;

    private Coroutine autoHealRoutine;
    private float healDelay = 5f; // hasar aldýktan sonra bekleme
    private int healAmountPerTick = 1;
    private float healInterval = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);

        GameObject[] healthJumpscares = GameObject.FindGameObjectsWithTag("HealthJumpscare");
        foreach (GameObject js in healthJumpscares)
        {
            js.SetActive(false);
            lowHealthJumpScares.Add(js);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        CheckHealthTriggers();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(3); // Ölüm sahnesi
        }

        // Hasar alýnca önceki iyileþme iptal edilir, sonra yeniden baþlatýlýr
        if (autoHealRoutine != null)
            StopCoroutine(autoHealRoutine);

        autoHealRoutine = StartCoroutine(AutoHeal());
    }

    public void Heal(int heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        healthBar.SetHealth(currentHealth);
        CheckHealthTriggers();
    }

    private IEnumerator AutoHeal()
    {
        yield return new WaitForSeconds(healDelay);

        while (currentHealth < maxHealth)
        {
            Heal(healAmountPerTick);
            yield return new WaitForSeconds(healInterval);
        }
    }

    private void CheckHealthTriggers()
    {
        float percent = (float)currentHealth / maxHealth;

        if (percent <= 0.8f && !lowHealthActive)
        {
            foreach (GameObject js in lowHealthJumpScares)
            {
                if (js != null) js.SetActive(true);
            }
            lowHealthActive = true;
        }
        else if (percent > 0.8f && lowHealthActive)
        {
            foreach (GameObject js in lowHealthJumpScares)
            {
                if (js != null) js.SetActive(false);
            }
            lowHealthActive = false;
        }
    }
}
