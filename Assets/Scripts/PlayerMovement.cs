using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 3f;
    public float JumpHeight = 5;
    public float GravityScale = 5f;
    public float DownForce = 2f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform[] lanePositions;
    private Rigidbody _rb;
    private int index = 1;
    private bool isGrounded;

    float initalYpos;
    float velocityY;
    void Start()
    {
        initalYpos = transform.position.y;
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        IsGrounded();
        Movement();
        OnInput();
    }
    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && index < lanePositions.Length - 1)
            index++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && index > 0)
            index--;

        Vector3 pos = lanePositions[index].position;

        pos.z = Mathf.Lerp(transform.position.z, pos.z, MoveSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, pos.z);

        transform.Translate(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }
    private void IsGrounded()
    {
        //Check if player is grounded
        if (Physics.OverlapSphere(transform.position, 0.5f, whatIsGround).Length > 0)
            isGrounded = true;
        else
            isGrounded = false;
        //If player is grounded apply gravity ,if not else set velocity to 0
        if (!isGrounded)
            velocityY += Physics.gravity.y * GravityScale * Time.deltaTime;
        else if (isGrounded && velocityY < 0)
            velocityY = 0;
    }
    private void OnInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            Jump();
    }
    private void Jump()
    {
        velocityY = Mathf.Sqrt(JumpHeight * -2 * (Physics.gravity.y * GravityScale));
    }
}
