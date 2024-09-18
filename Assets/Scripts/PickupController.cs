using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ray Rulifson, 9/17/24, A controller for the pickup objects

public class PickupController : MonoBehaviour
{
    private Rigidbody2D pickup;
    private float horizontal;
    private float vertical;

    public float horizontalSpeed;
    public float verticalSpeed;



    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<Rigidbody2D>();
        horizontal = Random.Range(horizontalSpeed*-1, horizontalSpeed);
        vertical = Random.Range(verticalSpeed*-1, verticalSpeed);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Allows access to PlayerController
        PlayerController playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        //pickup.velocity = new Vector2(horizontal, vertical);
        Vector2 movement = new Vector2(horizontal, vertical);
        pickup.velocity = movement * playerScript.pickupSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("PickUp") || col.gameObject.CompareTag("Cow"))
        {
             horizontal = Random.Range(horizontalSpeed * -1, horizontalSpeed);
             vertical = Random.Range(verticalSpeed * -1, verticalSpeed);
        }
    }
}
