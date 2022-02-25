using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermovent : MonoBehaviour
{
    //Carmove
    public float movespeed = 5f;

    public Rigidbody2D rb;
    
    Vector2 movement;

    //isFlash
    [SerializeField] private bool isFlash = false;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Text GeamCounter;

    private int GeamAmount;

    private int collidedGeamValue;

    public static Playermovent instance;
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    /**public void addHP()
    {
        hp++;
        if (hp > GamePlay.instance.maxHp)
            hp = GamePlay.instance.maxHp;
    }
    */
    void getDamage()
    {
        //hp--;
        isFlash = true;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Flash());

        //if Player Dead
       /** if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
       */
    }


    // Start is called before the first frame update
    void Start()
    {
        //Carmove
        rb = GetComponent<Rigidbody2D>();
        movespeed = 5f;
       
    }

    // Update is called once per frame
    void Update()
    {
        SoundManger.PlaySound("demoacceleration");
        //Carmove
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate()
    {
        //Carmove
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
       
    }

 
    
    IEnumerator Flash()
    {
        for (int n = 0; n < 6; n++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
        isFlash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isFlash)
        {
            getDamage();
        }
    }
}
