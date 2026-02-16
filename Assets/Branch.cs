using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public static float bottomY = -20f;

    void Start()
    {
        Debug.Log("Branch Start() called - resetting velocity");
        // Reset any inherited velocity from the tree
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log("Branch velocity reset to zero");
        }
        else
        {
            Debug.Log("ERROR: Branch has no Rigidbody!");
        }
    }

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
}