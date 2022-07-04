using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float enemyDamage;
    float damageRate = 0.5f;
    public float pushBackforce;
    float nextDamge;
    // Start is called before the first frame update
    void Start()
    {
        nextDamge = 0f;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !PlayerController.instance.playerHealth.isDead())
        {
            PlayerHealth thePlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            thePlayerHealth.addDamge(enemyDamage);
            nextDamge = damageRate + Time.time;
            pushBack(collision.transform);
        }
    }

    void pushBack(Transform pushedObject)
    {
        Vector2 pushDrirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
        pushDrirection *= pushBackforce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDrirection, ForceMode2D.Impulse);
    }
}
