using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2L2 : MonoBehaviour
{
    [SerializeField]
    private int health = 30;
    private Animator animator;
    private bool haveShield = false;

    void Start()
    {

        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "PlayerKnife" || other.gameObject.tag == "KnifeAttack") && !haveShield)
        {
            health -= 10;
            if (health > 0) animator.SetTrigger("damage");
            if (other.gameObject.tag != "KnifeAttack") Destroy(other.gameObject);
        }
        if (other.gameObject.name == "FireBall(Clone)")
        {
            health -= 30;
            if (health > 0) animator.SetTrigger("damage");
            Destroy(other.gameObject);
        }

        if (health <= 0)
        {
            animator.SetTrigger("die");
            Destroy(gameObject, 1f);
        }
    }
}
