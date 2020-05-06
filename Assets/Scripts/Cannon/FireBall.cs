using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Rigidbody>().AddForce(transform.forward * 3);
        rb.velocity = transform.forward * 20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Destroy(this.gameObject);
            if (collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerController>().KillPlayer();

            }
        }
    }
}
