using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of UI components like Text.

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;

    public Text uiText;

    void Start()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        uiText.text = score.ToString("#,0");
    }
}
