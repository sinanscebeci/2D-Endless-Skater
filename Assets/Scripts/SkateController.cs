using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    // Start is called before the first frame update

    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;
    }

}
