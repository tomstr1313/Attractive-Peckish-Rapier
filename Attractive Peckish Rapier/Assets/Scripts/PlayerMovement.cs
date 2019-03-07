using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Player player;
    public float speed;
    public GameManager gameManager;
    public Text txtLevel;

    private bool win;
    float timerPickup;

    float cooldownPickup = .5f;

    // Initializing values
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        timerPickup = Time.time;
        win = false;
    }

    // Player Movements
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Use Move Position to make sure no bounce off walls and stuff
        rigidBody.MovePosition(new Vector2(transform.position.x + movement.x * speed, transform.position.y + movement.y * speed));

        // Restarting the board
        if (Input.GetKey(KeyCode.E) && win)
        {
            win = false;
            ResetText();
            gameManager.SpawnTargets(5);
        }
    }

    // Trigger checking -> For targets atm
    // Can be extended to audio triggers @ Tom
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            // Cooldown timer so you don't just keep switching pick ups
            if (Time.time - timerPickup > cooldownPickup)
            {
                // Items become child components of the player, to copy movement
                // Remove items from equip by removing the player as the parent
            while (transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.transform.SetParent(null);
            }

            collision.gameObject.transform.SetParent(this.transform);
            player.playerType = transform.GetChild(0).GetComponent<Item>().itemType;
            timerPickup = Time.time;
            }
        }
    }

    // On colliding with physical objects (not triggers)
    // Currently works for targets
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When colliding with a target
        if (collision.gameObject.tag == "Target")
        {
            // If the player is holding an item that corresponds with the target type
            if (collision.gameObject.GetComponent<Target>().npcType == player.playerType)
            {
                // Delete the target from the targets list, set it to inactive
                for (int i = gameManager.targets.Count - 1; i >= 0; i--)
                {
                    if (gameManager.targets[i].id == collision.gameObject.GetComponent<Target>().id)
                    {
                        gameManager.targets.RemoveAt(i);
                    }
                }
                Destroy(collision.gameObject);
            }

            // After a collision, if there are no more targets
            // Display a win message
            // Reset game
            if (gameManager.targets.Count == 0)
            {
                txtLevel.text = "You Win!\nPress E to play Again!";
                txtLevel.gameObject.SetActive(true);
                win = true;
            }

        }
    }

    private void ResetText()
    {
        txtLevel.gameObject.SetActive(false);
    }
}
