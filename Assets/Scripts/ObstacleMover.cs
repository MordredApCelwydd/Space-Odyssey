using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Obstacle Movement")]
    [SerializeField] private bool isMovable;
    [SerializeField] private Vector3 destinationPosition;
    [SerializeField] private float movementSpeed;
    
    [Header("Obstacle Rotation")]
    [SerializeField] private bool isRotatable;
    [SerializeField] private bool isRotatingByX;
    [SerializeField] private bool isRotatingByY;
    [SerializeField] private bool isRotatingByZ;
    [SerializeField] private float rotationSpeed;
    
    private Vector3 _startingPosition;
    private Vector3 _currentDestination;
    
    private void Start()
    {
        _startingPosition = transform.position;

        _currentDestination = destinationPosition;
    }
    
    private void Update()
    {
        if (isMovable)
        {
            MoveObstacle();
        }

        if (isRotatable)
        {
            RotateObstacle();
        }
    }

    private void MoveObstacle()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentDestination, movementSpeed * Time.deltaTime);
        
        if (transform.position == _startingPosition)
        {
            _currentDestination = destinationPosition;
        }
        else if (transform.position == destinationPosition)
        {
            _currentDestination = _startingPosition;
        }
    }

    private void RotateObstacle()
    {
        transform.Rotate(rotationSpeed * (isRotatingByX ? 1 : 0) * Time.deltaTime, 
            rotationSpeed * (isRotatingByY ? 1 : 0) * Time.deltaTime, 
            rotationSpeed * (isRotatingByZ ? 1 : 0) * Time.deltaTime);
    }
}
