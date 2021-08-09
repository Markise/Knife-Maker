using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    private Animator anim;
    private bool okToThrow;
    private bool okToSlash;
    public GameObject Knife;
    public Transform Aim;
    public float knifeX, knifeY;


    // Start is called before the first frame update
    void Start()
    {
        okToSlash = true;
        okToThrow = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (okToSlash)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetBool("Slash", true);
                okToSlash = false;
            }
        }
        else { anim.SetBool("Slash", false); }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("Throw", true);
            okToSlash = false;
        }
        else { anim.SetBool("Throw", false); }

        if (!transform.parent.GetComponent<BasicMovement>().Grounded())
        {
            anim.SetBool("inAir", true);
        }
        else { anim.SetBool("inAir", false); }
    }

    void Throw()
    {
        if (okToThrow)
        {
            okToThrow = false;
            Rigidbody2D krb;
            GameObject knife = Instantiate(Knife, Aim.transform.position, Quaternion.identity);
            knife.transform.localScale = transform.parent.localScale;
            krb = knife.GetComponent<Rigidbody2D>();
            krb.velocity = new Vector2(knifeX*transform.parent.localScale.x, knifeY);
        }

        okToThrow = true;
    }

    void OkToAttack()
    {
        okToSlash = true;
    }

   

}
