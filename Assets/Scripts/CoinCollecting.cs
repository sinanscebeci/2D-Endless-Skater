using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollecting : MonoBehaviour
{
    private GameObject coinCounter;
    private int coinCollected = 0;

    void Start()
    {
        coinCounter = GameObject.Find("CoinCounter");
    }

    void Update()
    {
        transform.position =  new Vector3(transform.position.x, transform.position.y + Mathf.Lerp(-0.75f, 0.75f, Mathf.PingPong(Time.time, 1)) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Girdi");
            coinCollected = int.Parse(coinCounter.GetComponent<TextMeshProUGUI>().text);
            coinCollected++;
            coinCounter.GetComponent<TextMeshProUGUI>().text = coinCollected.ToString();
            Destroy(gameObject);
        }
    }
}
