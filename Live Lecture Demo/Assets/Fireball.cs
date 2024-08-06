using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float range = 10f;
    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

        Vector3 distanceFromStart = transform.position - startPosition;
        float distance = distanceFromStart.magnitude;
        if (distance > range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Debug.Log("Collided with: " + other.gameObject);
    }
}
