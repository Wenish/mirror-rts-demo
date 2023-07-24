using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public float horizontalInput = 0f;
    [SyncVar]
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
        if (isLocalPlayer) {
            InputAxios();
        }
    }

    private void InputAxios()
    {
        var newHorizontalInput = Input.GetAxisRaw("Horizontal");
        var hasHorizontalInputChanged = newHorizontalInput != horizontalInput;
        var newVerticalInput = Input.GetAxisRaw("Vertical");
        var hasVerticalInputChanged = newVerticalInput != verticalInput;
        if (hasHorizontalInputChanged || hasVerticalInputChanged)
        {
            CmdSetInput(newHorizontalInput, newVerticalInput);
        }
    }

    [Command]
    void CmdSetInput(float horizontal, float vertical)
    {
        horizontalInput = horizontal;
        verticalInput = vertical;
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
