using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    enum AIState { Idle, Alert};

    public GameObject jugador;
    public float distanciaVisio = 30f;
    public float velocitatAtac = 9f;
    public float velocitatIdle = 3f;
    public float health = 100f;
    public Animator animator;
    public LayerMask lm;
    public GameObject DeadParticle;

    Rigidbody2D Rb;
    AIState estatActual;
    Vector2 dir;
    Vector2 IdleDir;
    float coolDown = 5f;
    float coolDownCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
            Debug.LogError("No hi ha un Rigidbody2D lligat a l'enemic!");

        Rb = gameObject.GetComponent<Rigidbody2D>();
        estatActual = AIState.Idle;
        Rb.gravityScale = 0f;
        animator = gameObject.GetComponent<Animator>();
    }

    public void AddDamage(float damage)
    {
        health -= damage;
        if (health < 0f)
            Die();
    }

    void Die()
    {
        GameObject.Instantiate(DeadParticle, transform.position, DeadParticle.transform.rotation);
        Destroy(this.gameObject);
    }


    void IdleState()
    {
        animator.Play("Idle");
        animator.speed = 1f;
        coolDownCounter += Time.deltaTime;
        if (coolDownCounter > coolDown)
        {
            PickRandomDirection();
            coolDownCounter = 0f;
        }
        Rb.AddForce(IdleDir * velocitatIdle);
    }

    void AlertState()
    {
        animator.speed = 2f;
        animator.Play("Alert");
        coolDownCounter = 0f;
        Debug.Log("Alert!");
        Rb.AddForce(dir * velocitatAtac);
    }

    void PickRandomDirection()
    {
        IdleDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        dir = (jugador.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaVisio, lm);
        Debug.DrawRay(transform.position, dir * distanciaVisio);

        if (hit && hit.transform.tag == "Player")
            estatActual = AIState.Alert;
        else
            estatActual = AIState.Idle;

        switch (estatActual)
        {
            case AIState.Idle:
                IdleState();
                break;
            case AIState.Alert:
                AlertState();
                break;
        }
    }
}
