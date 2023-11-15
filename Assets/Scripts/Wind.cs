using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float windForce;

    [SerializeField]
    private Direction windDirection;

    private enum Direction
    {
        Right,
        Left, 
        Up, 
        Down
    }

    private void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {

        Vector2 forceDirection = Vector2.zero;

        // Set the force direction based on the selected enum value
        switch (windDirection)
        {
            case Direction.Right:
                forceDirection = Vector2.right;
                break;
            case Direction.Left:
                forceDirection = Vector2.left;
                break;
            case Direction.Up:
                forceDirection = Vector2.up;
                break;
            case Direction.Down:
                forceDirection = Vector2.down;
                break;
        }
        Debug.Log("Object is in trigger");
        player.GetComponent<Rigidbody2D>().AddForce(forceDirection * windForce * Time.deltaTime);

    }
}
