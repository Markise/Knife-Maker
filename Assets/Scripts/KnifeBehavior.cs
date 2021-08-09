using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{
    private bool okToSpin;
    private Rigidbody2D rb;
    public GameObject Crack;

    public BasicMovement Player;
    public float knifeSpinSpeed;
    public GameObject Tip;
    // Start is called before the first frame update
    void Start()
    {
        okToSpin = true;
        Player = FindObjectOfType<BasicMovement>();
        rb = GetComponent<Rigidbody2D>();
       // transform.localScale = Player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (okToSpin)
        {
            transform.Rotate(-Vector3.forward*Player.transform.localScale.x * knifeSpinSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "Player" && collision.collider.name != "knife")
        {
            okToSpin = false;
            Vector2 contact = collision.contacts[0].point;
          
            rb.velocity = Vector3.zero;
            rb.simulated = false;
            transform.position = new Vector2(transform.position.x-.02f,transform.position.y-.02f);
            int anglePick = Random.Range(1,4);

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 15 * -anglePick*Player.transform.localScale.x);

            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.collider);
        }
    }
}
