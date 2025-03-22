using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DvdController : MonoBehaviour
{
    private float movementSpeed;
    public float bounceVariation;
    private Rigidbody2D rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.gameManager = GameObject.FindObjectOfType<GameManager>();

        this.SetMovementSpeed(gameManager.GetDvdSpeed());
        this.rb.AddForce(GetRandomDirection() * (float)movementSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        this.rb.velocity = rb.velocity.normalized * (float)movementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        this.movementSpeed = speed;
        this.bounceVariation = (float)(this.movementSpeed / 25);
    }

    private Vector2 GetRandomDirection()
    {
        float angle;

        // Keep generating an angle until it avoids purely horizontal/vertical directions
        do
        {
            angle = Random.Range(0f, 360f);
        } while (Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)) < 0.5f || Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)) < 0.5f);

        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Apply a small random force to change direction
        Vector2 randomForce = new(
            Random.Range(-bounceVariation, bounceVariation),
            Random.Range(-bounceVariation, bounceVariation)
        );

        // Ensure we don't reverse direction along the Y-axis (for top/bottom collisions)
        if (collision.contacts[0].normal.y != 0) // Top or bottom collision
        {
            randomForce.y = Mathf.Abs(randomForce.y); // Keep vertical movement positive
        }

        // Apply the random force to the velocity
        this.rb.velocity += randomForce;

        // Keep the velocity normalized to the base speed
        this.rb.velocity = this.rb.velocity.normalized * (float)movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollidePoint"))
        {
            this.gameManager.AddPoints();
            this.gameManager.StartCombo();
        }
    }
}
