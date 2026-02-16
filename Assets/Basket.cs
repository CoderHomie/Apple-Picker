using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    void Start()
    {
        // Find a GameObject named ScoreCounter in the scene Hierarchy
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the ScoreCounter (Script) component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);
            // Increase the score
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
        else if (collidedWith.CompareTag("Branch"))
        {
            Destroy(collidedWith);
            // Branch caught - destroy all apples and branches, then remove this basket
            GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
            foreach (GameObject tempGO in appleArray)
            {
                Destroy(tempGO);
            }
            GameObject[] branchArray = GameObject.FindGameObjectsWithTag("Branch");
            foreach (GameObject tempGO in branchArray)
            {
                Destroy(tempGO);
            }

            // Get reference to ApplePicker and call AppleMissed to remove this basket
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            Destroy(this.gameObject); // Destroy this basket first
            apScript.BasketDestroyed(); // Then notify ApplePicker
        }
    }
}