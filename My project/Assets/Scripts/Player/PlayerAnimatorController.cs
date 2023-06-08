using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;


    private bool b_isIdle, b_isRunning, b_isEquip;
    private string s_playerIdle, s_playerRunning, s_playerEquip;
    private void Awake()
    {
        if (animator == null) 
            animator = GetComponent<Animator>();
        else
            Debug.Log("There is no Animator component on the object: " +  gameObject.name);

        s_playerIdle = "isIdle";
        s_playerRunning = "isRunning";
        s_playerEquip = "Equiped";

    }

    public void IsIdle(bool state)
    {
        b_isIdle = state;
        SetState(s_playerIdle, b_isIdle);
    }

    public void IsRuning(bool state)
    {
        b_isRunning = state;
        SetState(s_playerRunning, b_isRunning);
    }

    public void IsEquiped(bool state)
    {
        b_isEquip = state;
        animator.SetBool(s_playerEquip, b_isEquip);
    }

    private void SetState(string state, bool newState)
    {
        
        animator.SetBool(state, newState);
    }

}
