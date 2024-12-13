using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float WalkSpeed = 4f;
    public float RunSpeed = 14f;
    public float maxVelocityChange = 10f;
    public float Air = 0.5f;

    public float jumpHeight = 5f;


    private Vector2 input;
    private Rigidbody rb;

    private bool IsRunning;
    private bool IsJumping;

    private bool IsGrouded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsJumping = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (IsGrouded)
        {
            if (IsJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
            else if (input.magnitude > 0.5)
            {
                rb.AddForce(CalculateMovement(IsRunning ? RunSpeed : WalkSpeed), ForceMode.VelocityChange);

            }
            else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }
        }
        else
        {
            if (input.magnitude > 0.5)
            {
                rb.AddForce(CalculateMovement(IsRunning ? RunSpeed * Air : WalkSpeed * Air), ForceMode.VelocityChange);

            }
            else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }
        }
        IsGrouded = false;

    }

    public void OnTriggerStay(Collider other)
    {
        IsGrouded = true;
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5)
        {

            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return (velocityChange);

        }
        else
        {
            return new Vector3();
        }


    }
}
