using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector2 teleportPosition;
    [SerializeField] float teleportDelay = 1.0f;
    [SerializeField] float gizmoRadius = 0.5f; // Adjust this to change gizmo size

    public UnityEvent playerDrowned;

    public UnityEvent playerRespawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            StartCoroutine(TeleportWithDelay(collision.gameObject));
            playerDrowned.Invoke();

        }

    }

    private IEnumerator TeleportWithDelay(GameObject player)
    {
        yield return new WaitForSeconds(teleportDelay);
        player.transform.position = teleportPosition;

        playerRespawn.Invoke();
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo sphere to visualize the teleport position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(teleportPosition, gizmoRadius);
    }
}