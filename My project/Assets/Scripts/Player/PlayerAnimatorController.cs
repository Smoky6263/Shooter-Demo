using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private float _turnSmoothVelocity;
    private float _turnSmoothTime = 0.9f;

    private bool b_isIdle, b_isRunning, b_isEquip, b_onAim;
    private string s_playerIdle, s_playerRunning, s_playerEquip, s_onAim, s_onAimVertical, s_onAimHorizontal;

    private float f_onAimVertical = 0, f_onAimHorizontal = 0;

    private void Awake()
    {
        if (animator == null) 
            animator = GetComponent<Animator>();
        else
            Debug.Log("There is no Animator component on the object: " +  gameObject.name);

        s_playerIdle = "isIdle";
        s_playerRunning = "isRunning";
        s_playerEquip = "Equiped";
        s_onAim = "Aimed";
        s_onAimVertical = "Y";
        s_onAimHorizontal = "X";

    }

    public void IsIdle(bool state)
    {
        b_isIdle = state;
        animator.SetBool(s_playerIdle, b_isIdle);
    }

    public void IsRuning(bool state)
    {
        b_isRunning = state;
        animator.SetBool(s_playerRunning, b_isRunning);
    }

    public void IsEquiped(bool state)
    {
        b_isEquip = state;
        animator.SetBool(s_playerEquip, b_isEquip);
    }

    public void OnAim(bool state)
    {
        b_onAim = state;
        animator.SetBool(s_onAim, b_onAim);
    }

    public void OnAimMovement(float vertical, float horizontal)
    {
        f_onAimVertical = Mathf.SmoothStep(f_onAimVertical, vertical, 0.2f);
        f_onAimHorizontal = Mathf.SmoothStep(f_onAimHorizontal, horizontal, 0.2f);
        animator.SetFloat(s_onAimVertical, f_onAimVertical);
        animator.SetFloat(s_onAimHorizontal, f_onAimHorizontal);
    }

}
