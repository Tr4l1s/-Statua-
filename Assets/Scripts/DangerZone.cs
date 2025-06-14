using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float damageAmount = 5f;
    public float damageInterval = 1f;
    public float healAmount = 3f;
    public float healInterval = 1f;
    public float enemyCloseDistance = 10f;

    private PlayerController playerController;
    private PlayerStats playerStats;
    private float originalSpeed;
    private bool playerInside = false;
    private Coroutine healCoroutine;
    private float nextDamageTime = 0f;

    public Transform enemyTransform;
    public GlitchEffectController glitchEffectController;

    private Transform playerTransform;


    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    private void Update()
    {
        if (playerTransform == null || enemyTransform == null || glitchEffectController == null)
            return;

        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyTransform.position);
        bool isEnemyClose = distanceToEnemy <= enemyCloseDistance;

        if (playerInside || isEnemyClose)
        {
            glitchEffectController.EnableGlitch();
        }
        else
        {
            glitchEffectController.DisableGlitch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            playerController = other.GetComponent<PlayerController>();
            playerStats = other.GetComponent<PlayerStats>();
            playerTransform = other.transform;

            if (playerController != null)
            {
                playerController.Speed = playerController.speedSlow;
                playerController.canRun = false;
                playerController.isSlowedByZone = true; // buraya ekle
            }

            if (healCoroutine != null)
            {
                StopCoroutine(healCoroutine);
                healCoroutine = null;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerInside)
        {
            if (Time.time >= nextDamageTime)
            {
                if (playerStats != null)
                {
                    playerStats.TakeDamage((int)damageAmount);
                }
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (playerController != null)
            {
                playerController.Speed = playerController.speedMore;
                playerController.canRun = true;
                playerController.isSlowedByZone = false; // buraya ekle
            }

            if (playerStats != null)
            {
                healCoroutine = StartCoroutine(HealOverTime());
            }
        }
    }


    private IEnumerator HealOverTime()
    {
        while (!playerInside && playerStats.currentHealth < playerStats.maxHealth)
        {
            playerStats.Heal((int)healAmount);
            yield return new WaitForSeconds(3f); // her 3 saniyede bir iyileştir

        }
    }
}
