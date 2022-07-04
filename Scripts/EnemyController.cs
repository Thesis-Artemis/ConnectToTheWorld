using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D enemy;
    public bool checkMarkLeft;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 1.5f;

        enemy.velocity = new Vector2(transform.localScale.x, -26.4f) * moveX;
        enemy.velocity = new Vector2(transform.localScale.x, -26.4f) * -moveX;


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "markleft")
        {
            checkMarkLeft = false;
        }
        if (collision.gameObject.tag == "markRight")
        {
            checkMarkLeft = true;
        }
    }
    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
