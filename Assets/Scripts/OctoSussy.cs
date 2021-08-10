using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoSussy : MonoBehaviour
{
    public int RaysToShoot, maxDistance;
    public float moveSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(PickState());
    }

    // Update is called once per frame
   
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Casts();
    }

    void Patrol()
    {
        int patrolTime = Random.Range(1, 3);

        switch (patrolTime)
        {
            case(1):
                rb.velocity = new Vector3(moveSpeed * Time.deltaTime, rb.velocity.y);
                break;

            case (2):
                rb.velocity = new Vector3(-moveSpeed * Time.deltaTime, rb.velocity.y);
                break;
 
            case (3):
                rb.velocity = Vector2.zero;
                break;
        }

        StartCoroutine(PickState());
    }

    void MoveTowardPlayer()
    {
        //Check Distance by finding player

    }

    void Attack()
    {
        //Check Room Trigger to see if player is in room

        //Check Distance by Trigger Radius
        int attackSlot = Random.Range(1,2);

    }

    IEnumerator PickState()
    {
        int wait = Random.Range(1, 3);
        yield return new WaitForSeconds(wait);
        
        float stateSlot = Random.Range(1, 2);
        switch(stateSlot)
        {
            case (1):
                Patrol();
                Debug.Log("PAtrol");
                break;
            case (2):
                Debug.Log("something else");
                StartCoroutine(PickState());
                break;           
        }
        

    }

    void Casts()
    {
        float angle = 0;
        for (int i = 0; i < RaysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += 2 * Mathf.PI / RaysToShoot;

            Vector3 dir = new Vector3(transform.position.x * x, transform.position.y * y, 0);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDistance);
            //Debug.DrawLine(transform.position, dir, Color.red);
            if(hit.collider != null)
            {
             //   Debug.Log("Hit "+i+" : " + hit.point);
              //  Debug.DrawLine(transform.position, hit.point, Color.green);
                //here is how to do your cool stuff ;)
            }
        }
    }
}
