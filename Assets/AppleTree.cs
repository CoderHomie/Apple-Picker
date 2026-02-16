using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    // Prefab for instantiating apples
    public GameObject applePrefab;
    public GameObject branchPrefab; // Add this line

    // Speed at which the AppleTree moves in meters/second
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float chanceDirChance = 0.1f;

    // Seconds between Apple instantiations
    public float AppleDropDelay = 1f;

    [Range(0f, 1f)]
    public float branchDropChance = 0.15f; // 15% chance to drop a branch instead of apple

    void Start()
    {
        // Start dropping apples
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        // Randomly decide whether to drop a branch or an apple
        GameObject objectToDrop;

        if (Random.value < branchDropChance)
        {
            // Drop a branch
            objectToDrop = Instantiate<GameObject>(branchPrefab);
        }
        else
        {
            // Drop an apple
            objectToDrop = Instantiate<GameObject>(applePrefab);
        }

        objectToDrop.transform.position = transform.position;

        // CRITICAL: Reset velocity immediately after instantiation
        Rigidbody rb = objectToDrop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Invoke("DropApple", AppleDropDelay);
    }

    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    void FixedUpdate()
    {
        // Random direction changes are now time-based due to FixedUpdate()
        if (Random.value < chanceDirChance)
        {
            speed *= -1; // Change direction
        }
    }
}