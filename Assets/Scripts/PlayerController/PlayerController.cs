using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Private references
    CharacterController characterController;

    [Header("Movement")]
    [SerializeField] float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandleFacing();
    }

    void FixedUpdate()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        HandleMovement(xAxis, yAxis);
    }

    void HandleMovement(float a_xInput, float a_yInput)
    {
        Vector3 motionVector = new Vector3(a_xInput, 0, a_yInput) * Time.fixedDeltaTime * movementSpeed;

        characterController.Move(motionVector);
    }

    void HandleFacing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 lookPos = new Vector3(hit.point.x, 1, hit.point.z);
            transform.LookAt(lookPos);
        }
    }

    void HandleInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.GetComponent<Countertop>())
            {

            }
        }
    }
}
