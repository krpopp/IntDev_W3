using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D myBody;
    float horizontalMove;

    public float moveMult;

    bool grounded = false;
    public float castDist = 0.2f;

    public float jumpLimit = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;

    bool jump = false;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(GetComponent<SpriteRenderer>().size);
        if(Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        HorizontalMove(horizontalMove);

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }

        if(myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;
        } else if(myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        if (hit.collider != null && hit.transform.name == "Floor")
        {
            grounded = true;
            //Debug.Log(hit.transform.name);
        }
        else
        {
            grounded = false;
        }

    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * Time.fixedDeltaTime * moveMult;
        myBody.velocity = new Vector3(moveX, myBody.velocity.y);
        if (myBody.velocity.x > 0 || myBody.velocity.x < 0)
        {
            myAnim.SetBool("isWalking", true);
        }
        else {
            myAnim.SetBool("isWalking", false);
        }
    }
}
