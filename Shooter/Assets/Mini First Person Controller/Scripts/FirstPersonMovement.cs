using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    [Header("Running")] [SerializeField] private bool canRun = true;
    [field: SerializeField] public bool IsRunning { get; private set; }
    [SerializeField] private float runSpeed = 9;
    [SerializeField] private KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody rigidbody;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsRunning = canRun && Input.GetKey(runningKey);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed,
            Input.GetAxis("Vertical") * targetMovingSpeed);

        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
}