using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float relativeSpeed;

    void Update()
    {
        transform.position = new Vector2(cam.position.x * relativeSpeed, transform.position.y);
    }
}
