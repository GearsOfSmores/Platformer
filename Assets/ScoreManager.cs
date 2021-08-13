using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public float scoreTime;
    public float pointIncreasedPerSecond;

    void Start()
    {
        //scoreTime = 0f;
        //pointIncreasedPerSecond = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Final Time = " + (int)scoreTime;
        //scoreTime = +pointIncreasedPerSecond + Time.deltaTime;
    }
}
