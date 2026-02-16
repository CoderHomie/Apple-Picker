using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab; // Prefab for the baskets
    public int numBaskets = 4; // Number of baskets
    public float basketBottomY = -14f; // Y position of the bottom basket
    public float basketSpacingY = 2f; // Space between baskets
    public List<GameObject> basketList; // List of baskets

    void Start()
    {
        CreateBaskets();
    }

    void CreateBaskets()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleMissed()
    {
        // Destroy all of the falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Destroy one of the baskets
        int basketIndex = basketList.Count - 1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        // If there are no baskets left, check if we can go to next round
        if (basketList.Count == 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                // Ask GameManager if we should continue to next round or game over
                if (gameManager.ShouldContinueToNextRound())
                {
                    // Go to next round - recreate baskets
                    CreateBaskets();
                }
                else
                {
                    // No more rounds - game over
                    gameManager.GameOver();
                }
            }
        }
    }

    // New function: Called when a basket is destroyed by catching a branch
    public void BasketDestroyed()
    {
        // Remove the destroyed basket from the list
        // Find which basket was destroyed (it's already been Destroyed, so find the null reference)
        basketList.RemoveAll(item => item == null);

        // If there are no baskets left, check if we can go to next round
        if (basketList.Count == 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                if (gameManager.ShouldContinueToNextRound())
                {
                    CreateBaskets();
                }
                else
                {
                    gameManager.GameOver();
                }
            }
        }
    }

    void Update()
    {

    }
}