using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;


    private bool isIdle, isRunning;

    private string playerIdle, playerRunning;
    private void Awake()
    {
        if (animator == null) 
            animator = GetComponent<Animator>();
        else
            Debug.Log("There is no Animator component on the object: " +  gameObject.name);

        playerIdle = "isIdle";
        playerRunning = "isRunning";
    }

    public void IsRuning(bool state)
    {
        isRunning = state;
        SetState(playerRunning, isRunning);
    }

    public void IsIdle(bool state)
    {
        isIdle = state;
        SetState(playerIdle, isIdle);
    }

    private void SetState(string state, bool newState)
    {
        animator.SetBool(state, newState);
    }

}
