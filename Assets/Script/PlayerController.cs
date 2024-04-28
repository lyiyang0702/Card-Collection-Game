using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitySingleton<PlayerController>
{
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    Vector3 moveDirection;
    public List<Collider2D> _colliders;
    public Interactable targetInteractable;
    public Interactable currentHeldInteractable;
    public float interactionRadius;
    public ContactFilter2D interactionFilter;
    public bool canInteract = true;
    public Inventory inventory;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        DetectInteractables();
    }

    public void Move(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        moveDirection = new Vector2(direction.x, direction.y);

    }

    void DetectInteractables()
    {
        if (currentHeldInteractable)
        {
            if (targetInteractable)
            {
                targetInteractable.OnStopTargetInteractable();
            }
            targetInteractable = null;
            return;
        }

        if (Physics2D.OverlapCircle(transform.position, interactionRadius, interactionFilter, _colliders) > 0)
        {
            float shortestDistance = Mathf.Infinity;
            Interactable closestInteractable = null;
            for (int i = _colliders.Count - 1; i >= 0; i--)
            {
                if (_colliders[i].GetComponent<Interactable>().currentInteractState == Interactable.InteractState.NoInteract)
                {
                    _colliders.RemoveAt(i);
                    continue;
                }



                float newDist = Vector2.Distance(transform.position, _colliders[i].transform.position);

                if (closestInteractable == null || newDist < shortestDistance)
                {
                    shortestDistance = newDist;
                    closestInteractable = _colliders[i].GetComponent<Interactable>();
                }
            }


            if (targetInteractable != null && targetInteractable != closestInteractable)
            {
                targetInteractable.OnStopTargetInteractable();
            }

            if (closestInteractable == null)
            {
                return;
            }

            targetInteractable = closestInteractable;
            targetInteractable.OnTargetInteractable();

        }
        else
        {
            if (targetInteractable != null)
            {
                targetInteractable.OnStopTargetInteractable();
            }

            targetInteractable = null;
        }
    }
    public void TryInteract(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (targetInteractable == null && currentHeldInteractable == null)
        {
            return;
        }

        if (!canInteract)
        {
            return;
        }

        Interactable interactableToUse = (currentHeldInteractable != null) ? currentHeldInteractable : targetInteractable;

        if (interactableToUse == currentHeldInteractable)
        {
            interactableToUse.InteractAction();
        }
        else
        {
            interactableToUse.OnInteract();
        }
    }

    public void ApplyMovement()
    {
        rb.velocity = moveDirection * speed;
    }
}
