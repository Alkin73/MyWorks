using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int moveDirection = 1;

    public Vector3 moveDirectionAndSpeed;
    public float moveTime = 2;
    public float timer = 0;
    public LayerMask playerLayer;
    public CharacterController player;
    Vector3 oldPosition;

    private void Start()
    {
        oldPosition = transform.position;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            timer = 0;
            moveDirection *= -1;
        }
        MovePlatformInDirection(moveDirection);
        Vector3 movementAmount = transform.position - oldPosition;
        MovePlayerOnPlatform(movementAmount);

        oldPosition = transform.position;
    }

    void MovePlatformInDirection(int direction)
    {
        transform.position += moveDirectionAndSpeed * Time.deltaTime * direction;
    }

    void MovePlayerOnPlatform(Vector3 platformMovement)
    {
        Bounds platformBounds = GetComponent<MeshRenderer>().bounds;
        bool isPlayerOnPlatform = Physics.CheckBox(platformBounds.center + Vector3.up * platformBounds.extents.y, platformBounds.extents, transform.rotation, playerLayer);
        if (isPlayerOnPlatform)
        {
            player.Move(platformMovement);
        }
    }
}
