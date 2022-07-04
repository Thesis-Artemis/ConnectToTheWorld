using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;

    public PlayerHealth playerHealth;
    public float maxSpeed;
    public float jumpHeight;
    Rigidbody2D myBody;
    Animator myAnim;
    bool lookRight;
    bool ground;
    [SerializeField] Rigidbody2D _rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance = this;
        this.playerHealth = GetComponent<PlayerHealth>();
        this.myBody = GetComponent<Rigidbody2D>();
        this.myAnim = GetComponent<Animator>();
        this.myAnim.SetFloat("Jump", -1);
        lookRight = true;
    }


    void Update()
    {
        

 
     
    }
    public void goToTheLeft ()
    {
        if (lookRight)
        { flip(); }    
  
        this.myAnim.SetFloat("Jump", Mathf.Abs(-1));
        transform.Translate(Vector2.left * maxSpeed * Time.deltaTime);
    }
    public void goToTheRight()
    {
        if (!lookRight)
        { flip(); }

        this.myAnim.SetFloat("Jump", Mathf.Abs(-1));
        transform.Translate(Vector2.right * maxSpeed * Time.deltaTime);
    }
    public void Jumpping()
    {
        if (ground)
        {
            this.myAnim.SetFloat("Jump", Mathf.Abs(-1));
            _rigid2D.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            ground = false;
        }

       
    }


    private void flip()
    {
        lookRight = !lookRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            ground = true;
        }

    }

}
