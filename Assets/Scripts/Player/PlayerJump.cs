using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    #region References

    [SerializeField] private BoolReference _isJumping = null;
    [SerializeField] private BoolReference _isGrounded = null;
    [SerializeField] private IntReference _facingDirection = null;
    [SerializeField] private InputActionAsset _controls = null;

    #endregion

    #region Variables

    [SerializeField] private float _jumpForce = 2.5f;
    [SerializeField] private float _maxVelocityY = 2.5f;
    private Rigidbody2D _rigidbody2D = null;
    private int _totalNumberOfJumps = 1;
    private int _jumpsLeft;

    #endregion


    private void Awake()
    {
        // ResetJumps();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isGrounded.Value)
            ResetJumps();

        if (_isGrounded.Value && _controls.FindAction("Jump").ReadValue<float>() > 0)
            Jump();
        else if (!_isGrounded.Value & _controls.FindAction("Jump").ReadValue<float>() > 0 && _jumpsLeft > 0)
            Jump(); //Double Jump: Jump in air if there are jumps left

        if (_rigidbody2D.velocity.y > _maxVelocityY)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _maxVelocityY);
        }
    }

    private void ResetJumps()
    {
        _jumpsLeft = _totalNumberOfJumps;
        _isJumping.Value = false;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        _isJumping.Value = true;
        _jumpsLeft--;
    }
}