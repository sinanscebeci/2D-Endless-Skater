using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void FixedUpdate()
    {
        transform.position += new Vector3(speed, Mathf.Lerp(-0.3f, 0.3f, Mathf.PingPong(Time.time, 1)), 0) * Time.fixedDeltaTime;
    }

}
