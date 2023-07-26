using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NpcController : NetworkBehaviour
{
    public Vector3 startPosition;
    public float deathRadius = 20;
    public float speed = 1;

    Vector3 direction;
    // Start is called before the first frame update
    
    void Start()
    {
        if (!isServer) return;
        transform.position = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        startPosition = transform.position;
        speed = Random.Range(5, 10);
        direction = transform.right;

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
        var newPostion = transform.position + (direction * (Time.deltaTime * speed));
        float distance = Vector3.Distance(newPostion, startPosition);
        if (distance > deathRadius)
        {
            if (direction == transform.right) {
                direction = -transform.right;
            } else {
                direction = transform.right;
            }
        } else {
            transform.position = newPostion;
        }
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
