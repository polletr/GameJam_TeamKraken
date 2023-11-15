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
    private Transform currentPos;

    private Vector2 startPos;

    private bool growing;

    private float timer;

    [SerializeField]
    private bool fixedTree;

    private Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        startPos = new Vector2(currentPos.position.x, currentPos.position.y);
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (growing && currentPos.position.y < desiredPos.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
            //anim.SetBool("Moving", true);
        }
        else if (!fixedTree && !growing && currentPos.position.y > startPos.y)
        {
            if (timer > 1f)
            {
                transform.Translate(Vector2.down * speed * Time.fixedDeltaTime);
                //anim.SetBool("Moving", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && ClimateManager.Instance.currentState == 0)
        {
            growing = true;
            timer = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!fixedTree)
        {
            growing = false;
        }

    }


    private void OnDecrease()
    {
        if (transform.position != currentPos.position)
        {
            speed = -originalSpeed;
        }
    }

}
