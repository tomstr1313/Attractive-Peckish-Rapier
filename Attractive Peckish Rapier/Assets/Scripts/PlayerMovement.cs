﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Use Move Position to make sure no bounce off walls and stuff
        rigidBody.MovePosition(new Vector2(transform.position.x + movement.x * speed, transform.position.y + movement.y * speed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            while (transform.childCount > 0)
            {
                transform.GetChild(0).transform.SetParent(null);
            }


            Debug.Log("Item Collision");
            collision.gameObject.transform.SetParent(this.transform);
        }
    }
}
