using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ray Rulifson, 9/17/24, A controller for the cow

public class CowController : MonoBehaviour
{
    private Rigidbody2D cowRigidbody;
    private float horizontal;
    private float vertical;
    private float speed;

    public float horizontalSpeed;
    public float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        cowRigidbody = GetComponent<Rigidbody2D>();
        horizontal = Random.Range(horizontalSpeed * -1, horizontalSpeed);
        vertical = Random.Range(verticalSpeed * -1, verticalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(horizontal, vertical);
        cowRigidbody.velocity = movement * speed;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("PickUp"))
        {
            horizontal = Random.Range(horizontalSpeed * -1, horizontalSpeed);
            vertical = Random.Range(verticalSpeed * -1, verticalSpeed);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            speed += .4f;
            //following code spawns cow in random pos. And deactivates it
            Vector2 randomPosition = new Vector2(Random.Range(-9.0f,9.0f),Random.Range(-9.0f, 9.0f));
            transform.position = randomPosition;
            gameObject.SetActive(false);
        }
    }
}
