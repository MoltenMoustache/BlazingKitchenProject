using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float lifeTime = 10f;
    public float out_of_bounds = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }

        if (lifeTime <= 0)
        {
            Destruction();
        }

        if(this.transform.position.y <= -out_of_bounds)
        {
            Destruction();
        }
        
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
     if(coll.gameObject.name == "Destroy")
        {
            Destruction();
        }
    }
}
