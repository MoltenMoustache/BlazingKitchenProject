using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Private references
    CharacterController characterController;

    [SerializeField] Countertop highlightedCountertop = null;
    GameObject heldItem = null;

    [Header("Movement")]
    [SerializeField] float movementSpeed;

    [Header("Items")]
    [SerializeField] Transform heldItemPos;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
            return heldItem.GetComponent<Ingredient>() != null;
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
