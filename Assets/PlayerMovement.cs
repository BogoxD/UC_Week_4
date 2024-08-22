using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 3f;
    public float JumpForce = 5f;
    public float JumpDelay = 2f;
    public float DownForce = 2f;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform[] lanePositions;
    private Rigidbody _rb;
    public int index = 1;
    private bool isGrounded;
    private bool isJumping = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        OnInput();
    }
    private void LateUpdate()
    {
        isGrounded = GroundCheck();
        ApplyDownForce();
    }
    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && index < lanePositions.Length - 1)
            index++;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && index > 0)
            index--;

        transform.position = Vector3.Lerp(transform.position,
            lanePositions[index].position + transform.up * transform.position.y, MoveSpeed * Time.deltaTime);
    }
    private void OnInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !isJumping)
        {
            Jump();
            StartCoroutine(JumpReset());
        }
        else
            Movement();
    }
    private void ApplyDownForce()
    {
        _rb.AddForce(DownForce * Time.deltaTime * -transform.up, ForceMode.Force);
    }
    private void Jump()
    {
        _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }
    private bool GroundCheck()
    {
        if (Physics.OverlapSphere(transform.position, 0.05f, whatIsGround).Length > 0)
            return true;
        else
            return false;
    }
    private IEnumerator JumpReset()
    {
        isJumping = true;
        yield return new WaitForSeconds(JumpDelay);
        isJumping = false;
    }
}
