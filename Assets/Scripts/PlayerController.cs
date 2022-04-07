using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    public float jumpVelocity = 400f;
    //Jumping procedure
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;
    // Start is called before the first frame update

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
            jump();
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;
    }


    public void jump()
    {
        rb2d.AddForce(Vector2.up * jumpVelocity);
    }

    public bool isGrounded()
    {
        RaycastHit2D rch2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .5f, platformsLayerMask);
        return (rch2D.collider != null);
    }
}
