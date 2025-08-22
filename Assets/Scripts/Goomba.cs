using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float goomSpeed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void TurnAround()
    {
        transform.eulerAngles += new Vector3(0,180,0);
        print("Turn around dumbo");
        goomSpeed *= -1;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D bonk = Physics2D.Raycast(transform.position + transform.right*2, -Vector2.up);
        if(bonk.collider == null)
        {
            TurnAround();
        }

        LayerMask mask = LayerMask.GetMask("obstruction");
        RaycastHit2D stopWall = Physics2D.Raycast(transform.position, transform.right, 2, mask);
        if(stopWall.collider != null)
        {
            TurnAround();
        }

        transform.Translate(transform.right * goomSpeed * Time.deltaTime);
    }
}
