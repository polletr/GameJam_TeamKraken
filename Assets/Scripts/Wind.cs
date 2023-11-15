using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float windForce;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object is in trigger");
        player.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * windForce * Time.deltaTime);

    }
}
