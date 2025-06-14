using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public EleAlma eleAlmaScript;
    public Transform target;
    private NavMeshAgent agent;
    public float followDistance = 10f;
    public float wanderRadius = 20f;
    private float wanderTimer = 5f;
    private float timer;

    private int oncekiRituelSayac = 0;
    public float speedIncreasePerRituel = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        eleAlmaScript = FindObjectOfType<EleAlma>();
        timer = wanderTimer;
    }

    void Update()
    {
        if (eleAlmaScript.rituelSayac > oncekiRituelSayac)
        {
            int fark = eleAlmaScript.rituelSayac - oncekiRituelSayac;
            agent.speed += fark * speedIncreasePerRituel;
            oncekiRituelSayac = eleAlmaScript.rituelSayac;
            Debug.Log("Canavar hýzý arttý. Yeni hýz: " + agent.speed);
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= followDistance)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Canavar oyuncuya temas etti. Ölüm sahnesine geçiliyor.");
            SceneManager.LoadScene(3); // DeadScene sahne index'in
        }      
    }
}
