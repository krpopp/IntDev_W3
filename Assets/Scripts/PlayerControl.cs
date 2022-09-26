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

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(GetComponent<SpriteRenderer>().size);
    }

    private void FixedUpdate()
    {
        HorizontalMove(horizontalMove);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.name);
        }
    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * Time.fixedDeltaTime * moveMult;
        myBody.velocity = new Vector3(moveX, myBody.velocity.y);
    }
}
