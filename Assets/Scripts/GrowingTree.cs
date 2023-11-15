using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingTree : MonoBehaviour
{
    [SerializeField]
    private float originalSpeed;

    private float speed = 0f;

    [SerializeField]
    private Transform desiredPos;

    [SerializeField]
    private Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.position.y < desiredPos.localPosition.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player" && ClimateManager.Instance.currentState == 0)
        {
            speed = originalSpeed;
        }
    }


    private void OnDecrease()
    {
        if (transform.position != startPos.position)
        {
            speed = -originalSpeed;
        }
    }

}
