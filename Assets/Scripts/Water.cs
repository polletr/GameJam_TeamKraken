using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform teleportPosition;
    [SerializeField] float gizmoRadius = 0.5f; // Adjust this to change gizmo size

    public UnityEvent playerDrowned;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            playerDrowned.Invoke();
        }

    }
    private void OnDrawGizmos()
    {
        // Draw a gizmo sphere to visualize the teleport position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportPosition.position, gizmoRadius);
    }
}