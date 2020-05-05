using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float lifeTime = 5f;
    public float Out_of_Bounds = 5f;
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

        if(lifeTime <= 0)
        {
            Destruction();
        }

        if(this.transform.position.y <= -Out_of_Bounds)
        {
            Destruction();
        }



        void Destruction()
        {
            Destroy(this.gameObject);
        }


         void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name == "Destroy")
            {
                Destruction();
            }
        }

}
}
