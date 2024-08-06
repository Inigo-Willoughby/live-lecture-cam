using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 moveDirection = new Vector3(0f,0f,0f);

    private PlayerInput actions;
    private InputAction movementAction;
    
    void Awake()
    {
        actions = new PlayerInput();
        movementAction = actions.GameWorld.Movement;
    }

    void OnEnable()
    {
        movementAction.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
    }

    void Update()
    {
        moveDirection.x = movementAction.ReadValue<Vector2>().x;
        moveDirection.z = movementAction.ReadValue<Vector2>().y;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider collider)
    {
        moveDirection = Vector3.forward;
    }
}
