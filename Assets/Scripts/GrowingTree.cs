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

    private Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        startPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ( transform.position.y <= desiredPos.position.y && transform.position.y >= startPos.position.y )
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ClimateManager.Instance.currentState == 0)
        {
            Debug.Log("enter");
            speed = originalSpeed;
        }
        else
        {
            OnDecrease();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnDecrease();
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
