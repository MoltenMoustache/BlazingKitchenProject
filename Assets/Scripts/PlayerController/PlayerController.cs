using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Private references
    CharacterController characterController;

    Countertop highlightedCountertop = null;
    GameObject heldItem = null;

    [Header("Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] Vector3 gravity;
    Vector3 forwardVector;
    Vector3 rightVector;

    [Header("Items")]
    [SerializeField] Transform heldItemPos;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Vector3 camRotation = Camera.main.transform.rotation.eulerAngles;
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        forwardVector = Camera.main.transform.forward;
        rightVector = Camera.main.transform.right;
        Camera.main.transform.rotation = Quaternion.Euler(camRotation);
    }

    // Update is called once per frame
    void Update()
    {
        HandleFacing();
        HandleHighlight();

        HandleInteraction();



        #region Debug
        if (Input.GetKeyDown(KeyCode.K))
            KillPlayer();
        #endregion
    }

    void FixedUpdate()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        HandleMovement(xAxis, yAxis);
    }

    void HandleMovement(float a_xInput, float a_yInput)
    {
        Vector3 forwardMotion = forwardVector * a_yInput;
        Vector3 horizontalMotion = rightVector * a_xInput;
        Vector3 motionVector = (forwardMotion + horizontalMotion) * movementSpeed;
        motionVector += gravity;
        characterController.Move(motionVector * Time.deltaTime);
    }

    void HandleFacing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPos);
        }
    }

    void HandleHighlight()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.GetComponent<Countertop>())
            {
                highlightedCountertop = hit.transform.GetComponent<Countertop>();
                //highlightedCountertop.Highlight(true);
            }
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && highlightedCountertop != null)
        {
            highlightedCountertop.Interact(this);
        }
    }

    public void PickupItem(GameObject a_item)
    {
        if (IsHoldingItem())
            DiscardHeldItem();

        heldItem = a_item;
        heldItem.transform.parent = heldItemPos;
        heldItem.transform.position = heldItemPos.position;
    }

    public void DiscardHeldItem()
    {
        Destroy(heldItem);
        // Play 'poof' particles
        heldItem = null;
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }
    public bool IsHoldingIngredient()
    {
        if (IsHoldingItem())
            return heldItem.GetComponent<IngredientObject>() != null;
        else return false;
    }
    public bool IsHoldingDish()
    {
        if (IsHoldingItem())
            return heldItem.GetComponent<DishObject>() != null;
        else return false;
    }

    public void KillPlayer()
    {
        DiscardHeldItem();
        GameManager.instance.KillPlayer();
        gameObject.SetActive(false);
    }

    public void RespawnPlayer(Vector3 a_respawnPos)
    {
        Debug.Log("Respawned!");
        gameObject.SetActive(true);

        characterController.enabled = false;
        transform.position = a_respawnPos;
        characterController.enabled = true;
    }
}
