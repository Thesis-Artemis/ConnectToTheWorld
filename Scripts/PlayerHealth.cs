using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addDamge(float damge)
    {
        if (damge <= 0)
        {
            return;
        }
        currentHealth -= damge;

        if (isDead())
        {
            makeDead();
        }

    }

    
    void makeDead()
    {
        anim.SetTrigger("Die");
        Vector3 positionCharacter = new Vector3(transform.position.x, transform.position.y, -10);
  

    }
    public virtual bool isDead()
    {
        if (currentHealth <= 0)
        {
            return true;
        }
        return false;
    }
    void changeSence()
    { SceneManager.LoadScene("GameOver"); }

}
