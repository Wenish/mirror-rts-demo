using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NpcController : NetworkBehaviour
{
    public Vector3 startPosition;
    public float deathRadius = 100;
    public float speed = 1;
    // Start is called before the first frame update
    
    void Start()
    {
        if (!isServer) return;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;
        Move();
    }

    [Server]
    void Move()
    {
        Debug.Log("Npc Move");
        transform.position += transform.right * (Time.deltaTime * speed);
        float distance = Vector3.Distance(transform.position, startPosition);
        if (distance > deathRadius)
        {
            if (isServer) {
                DestroySelf();
            }
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
