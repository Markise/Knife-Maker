using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoSussy : MonoBehaviour
{

    public int RaysToShoot, maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Casts();
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
