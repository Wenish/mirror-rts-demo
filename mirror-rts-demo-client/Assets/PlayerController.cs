using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float horizontalInput = 0f;
    public float verticalInput = 0f;
    public float moveSpeed = 5f;

    Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isServer) {
            MovePlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void MovePlayer()
    {

        Vector3 inputs = Vector3.zero;
        inputs.x = horizontalInput;
        inputs.z = verticalInput;
        inputs = Vector3.ClampMagnitude(inputs, 1f);
        Vector3 moveDirection = inputs * moveSpeed;
        _rigidbody.velocity = moveDirection;
    }
}
