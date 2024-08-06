using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float rotationRate = 1f;
    private float rotationModifier = 0;
    [SerializeField] private Transform target;
    
    [SerializeField] private Fireball fireball;
    [SerializeField] private float attackInterval = 5f;
    private float timer = 0;

    [SerializeField] private float sleepInterval = 1f;
    [SerializeField] private float searchInterval = 1f;
    private float stateTimer = 0f;

    private bool seesPlayer = false;

    enum State
    {
        Searching,
        Sleeping,
        Attacking
    };

    [SerializeField] private State state;

    void Awake()
    {
        timer = attackInterval;
        state = State.Searching;
        stateTimer = searchInterval;
    }

    void Update()
    {
        transform.Rotate(
            Vector3.up * rotationRate * rotationModifier * Time.deltaTime, Space.Self);

        switch (state)
        {
            case State.Searching:
                rotationModifier = 1f; // Keeps monster rotating in a consistent direction
                stateTimer -= Time.deltaTime;

                if (seesPlayer)
                {
                    state = State.Attacking;
                    Debug.Log("Switching state to Attacking");
                }

                if (stateTimer <= 0)
                {
                    state = State.Sleeping;
                    stateTimer = sleepInterval;
                }
                break;

            case State.Sleeping:
                rotationModifier = 0f; // Stops monster movement
                stateTimer -= Time.deltaTime;
                
                if (stateTimer <= 0)
                {
                    state = State.Searching;
                    stateTimer = searchInterval;
                }
                break;

            case State.Attacking:
                DoAttack();
                if (!seesPlayer)
                {
                    state = State.Searching;
                    Debug.Log("Switching state to Searching");
                }
                break;
        }
    }

    void Shoot()
    {
        Fireball newFireball = Instantiate(fireball, transform.position, transform.rotation);
        timer = attackInterval;
    }

    void DoAttack()
    {
        Vector3 myRight = transform.TransformDirection(Vector3.right);
        Vector3 toTarget = Vector3.Normalize(target.position - transform.position);
        float targetRelative = Vector3.Dot(myRight, toTarget);
        rotationModifier = targetRelative; // Keeps monster locked on player

        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            Shoot();
        }
    }

    void OnTriggerEnter()
    {
        seesPlayer = true;
        Debug.Log("Monster sees player");
    }

    void OnTriggerExit()
    {
        seesPlayer = false;
        Debug.Log("Player ran away");
    }
}