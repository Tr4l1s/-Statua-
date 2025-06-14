using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint!");
        }

        if (other.CompareTag("Button"))
        {
            //other.GetComponent<ButtonLogic>()?.Activate();
        }

        if (other.CompareTag("Item"))
        {
            Debug.Log("Item toplandý!");
            Destroy(other.gameObject);
        }
    }
}
