using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsZ : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float minBounds;
    void Update()
    {
        transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);

        if (transform.position.x < minBounds)
            Destroy(gameObject);
    }
    public void SetSpeed(float ammount)
    {
        moveSpeed = ammount;
    }
}
