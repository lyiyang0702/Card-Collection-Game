using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum InteractState { CanInteract, NoInteract, AlreadyInteracted }
    public enum InteractActionType { Instant, Held, Delayed }
    public enum InteractType { Manual, Automatic };

   
    public InteractState currentInteractState;

    public InteractActionType actionType;

    public float delayInteractionTime;

    public InteractType interactType;


    public UnityEvent actionEvent;

    public GameObject validInteractIndicator;


    public virtual void OnInteract()
    {
        if (currentInteractState == InteractState.NoInteract || currentInteractState == InteractState.AlreadyInteracted)
        {
            return;
        }


        switch (actionType)
        {
            case InteractActionType.Instant:
                InteractAction();
                break;
            case InteractActionType.Delayed:
                currentInteractState = InteractState.AlreadyInteracted;
                Invoke("InteractAction", delayInteractionTime);

                break;
            case InteractActionType.Held:
                currentInteractState = InteractState.AlreadyInteracted;
                PlayerController.Instance.currentHeldInteractable = this;
                break;
        }



    }

    public virtual void InteractAction()
    {
        currentInteractState = InteractState.NoInteract;
        actionEvent.Invoke();

    }

    public virtual void OnTargetInteractable()
    {
        validInteractIndicator.SetActive(true);
    }

    public virtual void OnStopTargetInteractable()
    {
        validInteractIndicator.SetActive(false);
    }
}
