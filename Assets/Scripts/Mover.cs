using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform[] movePositions;
    [SerializeField]
    private float moveSpeed = 5f;

    private int startIndex = 0;
    private int nextIndex = 1;

    private float position = 0;
    private float distance;

    private void Awake()
    {
        SetMoveDistance();
    }

    private void Update()
    {
        position += Time.deltaTime * moveSpeed / distance;
        target.position = Vector2.Lerp(movePositions[startIndex].position, movePositions[nextIndex].position, position);

        if (position >= 1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        position = 0;
        SetNextDestinationIndex();
        SetMoveDistance();
    }

    private void SetNextDestinationIndex()
    {
        startIndex = nextIndex;
        nextIndex = (startIndex + 1) % movePositions.Length;
    }

    private void SetMoveDistance()
    {
        distance = Vector2.Distance(movePositions[startIndex].position, movePositions[nextIndex].position);
    }
}

