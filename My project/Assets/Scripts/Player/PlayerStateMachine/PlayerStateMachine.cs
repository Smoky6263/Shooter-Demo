using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerStateMachine : MonoBehaviour , IControllable
{
    private CharacterController _characterController;
    private PlayerAnimatorController _playerAnimator;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;


    [SerializeField] private Transform _playerCamera;
    [SerializeField] private MP5_Script _playerMP5;

    [SerializeField] private Rig _rig_aim;
    [SerializeField] private GameObject _backWeapon, _handWeapon;

    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _onAimSpeed = 1.0f;

    [SerializeField] private float _timeToGetWeapon;

    [SerializeField] private float _turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;

    //Player Inputs 
    private Vector2 _playerMoveInput;
    private bool _jumpInput;
    private bool _equipWeaponInputPerformed = false;
    private bool _onAim = false;

    //Player Stats
    private Vector3 _gravityForce = new Vector3(0f, -9.8f, 0f);

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Rig Rig_aim { get { return _rig_aim; } }
    public PlayerAnimatorController PlayerAnimator { get { return _playerAnimator; } }
    public Transform PlayerCamera { get { return _playerCamera; } }
    public GameObject BackWeapon { get { return _backWeapon; } }
    public GameObject HandWeapon { get { return _handWeapon; } }
    public MP5_Script PlayerMP5 { get { return _playerMP5; } }
    public float TurnSmoothVelocity { get { return _turnSmoothVelocity; } }
    public float TurnSmoothTime { get { return _turnSmoothTime; } }
    public float RunSpeed { get { return _runSpeed; } }
    public float OnAimSpeed { get { return _onAimSpeed; } }
    public float TimeToGetWeapon { get { return _timeToGetWeapon; } }


    //Player Inputs getters and setters
    public Vector2 PlayerMoveInput { get { return _playerMoveInput; } }
    public bool EquipWeaponInputPerformed { get { return _equipWeaponInputPerformed; } }
    public bool OnAim { get { return _onAim; } set { _onAim = value; } }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimatorController>();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();
    }

    public void Jump()
    {
        
    }

    //Player Inputs Read
    #region
    public void Move(Vector2 direction)
    {
        _playerMoveInput = direction;
    }
    public void EquipWeaponPerformed(bool isEquipPerformed)
    {
        _equipWeaponInputPerformed = isEquipPerformed;
    }

    public void OnAimPerformed(bool isAimPerformed)
    {
        if (_equipWeaponInputPerformed == false) return;
        else _onAim = !_onAim;

    }
    #endregion

    private void Update()
    {
        _currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        
    }
    private void TakeInWeapon()
    {
        _backWeapon.SetActive(false);
        _handWeapon.SetActive(true);

    }
    private void TakeOutWeapon()
    {
        _backWeapon.SetActive(true);
        _handWeapon.SetActive(false);

    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(20f ,15f, 300, 20), "Curerent Player State: " + _currentState.ToString());
        GUI.TextArea(new Rect(20f, 40f, 200, 20), "Equip Performeed: " + _equipWeaponInputPerformed.ToString());
        GUI.TextArea(new Rect(20f, 65f, 200, 20), "OnAim Performeed: " + _onAim.ToString());
        GUI.TextArea(new Rect(20f, 90f, 200, 20), $"Movement x: {_playerMoveInput.x.ToString()} Movement y: {_playerMoveInput.y}");
    }

}
