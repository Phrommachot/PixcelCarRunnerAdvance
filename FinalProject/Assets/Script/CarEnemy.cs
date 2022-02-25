using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int direction = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            FileSprite();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ground") && !collision.CompareTag("Player"))
        {
            FileSprite();
        }
    }


    void FileSprite()
    {
        if (direction == 1)
        {
            direction = -1;
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            direction = 1;
            transform.localScale = new Vector2(1, 1);
        }
    }
}
