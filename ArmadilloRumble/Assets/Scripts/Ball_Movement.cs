using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    // Public properties
    public float initialSpeed = 5.0f;
    public float acceleration = 10.0f;
    public float bounceRatio = 1.5f;

    // Private properties
    private float speed;
    private float targetSpeed;
    private Vector3 direction;

    private Vector3 debugAngle;
    private Vector3 collisionPoint;
    private Vector3 collisionNormal;

    public Ball_Input ballInput;

    // Setter - Getter
    public float Speed
    {
        set { speed = value; }
    }
    public Vector3 Direction
    {
        set { direction = value; }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        HorizontalMovement(dt);

        if (speed.Equals(0.0f))
        {
            ballInput.NumThrows = 0;
        }
    }

    void HorizontalMovement(float time)
    {
        float offset = targetSpeed - speed;
        speed += Mathf.Clamp(offset, -acceleration * time, acceleration * time);
        transform.position += direction * speed * time;
    }

    public void Shoot(Vector2 ballDirection, Vector3 cameraPos)
    {
        Vector3 cameraDirection = transform.position - cameraPos;

        float angle = Mathf.Atan2(ballDirection.y, ballDirection.x);
        angle *= Mathf.Rad2Deg;
        angle += 90;

        debugAngle = new Vector3(cameraDirection.x, 0.0f, cameraDirection.z);
        debugAngle = Quaternion.AngleAxis(-angle, Vector3.up) * debugAngle;

        speed = initialSpeed * ballDirection.magnitude;
        direction = new Vector3(debugAngle.x, 0.0f, debugAngle.z);
    }

    private void HardCollision(Vector3 normal)
    {
        int xNormal = 1;
        int zNormal = 1;

        if (normal.x < -0.5f || normal.x > 0.5f)
        {
            xNormal = -1;
        }
        if (normal.z < -0.5f || normal.z > 0.5f)
        {
            zNormal = -1;
        }

        direction = new Vector3(direction.x * xNormal, 0.0f, direction.z * zNormal);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.GetContact(0);
            HardCollision(contact.normal);
        }
        if (collision.gameObject.CompareTag("Bouncer"))
        {
            ContactPoint contact = collision.GetContact(0);
            HardCollision(contact.normal);
            speed *= bounceRatio;
        }
    }
}
