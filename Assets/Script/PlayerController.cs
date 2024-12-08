using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitySingleton<PlayerController>
{
    public PlayerCombatantController playerCombatant; 
    [SerializeField]
    float speed = 1f;
    [SerializeField] bool isFreeWalk = true;
    [SerializeField] Vector3 moveDirection;
    public PlayerInput input;
    public List<Collider2D> _colliders;
    public Interactable targetInteractable;
    public Interactable currentHeldInteractable;
    public float interactionRadius;
    public ContactFilter2D interactionFilter;
    public bool canInteract = true;
    public Inventory inventory;
    Rigidbody2D rb;
    public LayerMask wallLayer;
    public GameObject cam;
    public GameObject iconObj;
    private Animator anim;
    private SpriteRenderer icon;

    override public void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
        playerCombatant = GetComponent<PlayerCombatantController>();
        anim = iconObj.GetComponent<Animator>();
        icon = iconObj.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();    
        
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        DetectInteractables();
        MoveHeldInteractableToPlayer();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (CombatManager.Instance.battleState != BattleState.None) return;
        var direction = context.ReadValue<Vector2>();
        moveDirection = new Vector2(direction.x, direction.y);
        if (isFreeWalk) return;
        // Grid-Based Movement
        RaycastHit2D hit = Physics2D.Raycast(transform.position,moveDirection,0.5f,wallLayer);
        if (hit.collider != null) return;
        transform.position = transform.position + moveDirection * 0.5f;
    }

    void MoveHeldInteractableToPlayer()
    {
        if (currentHeldInteractable == null)
        {
            return;
        }
        currentHeldInteractable.transform.position = transform.position;
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

                if (_colliders[i].GetComponent<Interactable>().interactType == Interactable.InteractType.Automatic)
                {

                    if (newDist <= _colliders[i].GetComponent<Interactable>().autoPickupInteractRange)
                    {
                        _colliders[i].GetComponent<Interactable>().OnInteract();
                    }

                    _colliders.RemoveAt(i);

                    continue;
                }

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
        if (!isFreeWalk) return;
        rb.velocity = moveDirection * speed;
        if(moveDirection.x ==0 && moveDirection.y ==0){   
            anim.SetBool("walking",false);
        }else{
            anim.SetBool("walking",true);
        } 
        if (moveDirection.x > 0){
            icon.flipX = false;
        }
        else if (moveDirection.x < 0){
            icon.flipX = true;
        }
    }

    public void StopAllMovement()
    {
        rb.velocity = Vector3.zero;
        moveDirection = Vector3.zero;
    }

}
