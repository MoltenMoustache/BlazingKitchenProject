﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 3);
    }

    private void OnMouseDown()
    {

        //GetComponent<Rigidbody>().AddForce(transform.forward * 500);

    }
}

