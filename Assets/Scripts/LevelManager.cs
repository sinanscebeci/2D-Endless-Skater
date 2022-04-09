using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform Player;
    public GameObject[] obstacles;
    private float timeBtwSpawns;

    private float timer;


    void Start()
    {
        timer = 0;
        timeBtwSpawns = FloatRandomizer(1f, 2.2f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBtwSpawns)
        {
            timer = 0;
            int random = IntRandomizer(0,6);
            if(random <= 1)
            {
                Debug.Log("0 ya da 1 geldi");
                Debug.Log(random);
                GameObject temp = Instantiate(obstacles[random], new Vector2(Player.position.x + 35, -3.18f), Quaternion.identity);
                Destroy(temp, 10f);
            }
            else if(random <= 3)
            {
                Debug.Log("2 ya da 3 geldi");
                Debug.Log(random);
                GameObject temp = Instantiate(obstacles[random], new Vector2(Player.position.x + 35, FloatRandomizer(-3, 0)), Quaternion.identity);
                Destroy(temp, 10f);
            }
            else
            {
                Debug.Log("4 ya da 5 geldi");
                Debug.Log(random);
                GameObject temp = Instantiate(obstacles[random], new Vector2(Player.position.x + 35, FloatRandomizer(-1, 0)), Quaternion.identity);
                Destroy(temp, 10f);
            }
            timeBtwSpawns = FloatRandomizer(1f, 2.2f);
        }
    }

    private float FloatRandomizer(float minValue, float maxValue)
    {
        float value = Random.Range(minValue, maxValue);
        return value;
    }
    private int IntRandomizer(int minValue, int maxValue)
    {
        int value = Random.Range(minValue, maxValue);
        return value;
    }
}
