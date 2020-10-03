using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region References
    
    [SerializeField] private FloatReference _moveSpeed = null;
    [SerializeField] private TimeDirectionReference _timeDirection;
    [SerializeField] private Animator _animator;

    #endregion

    #region Variables

    private Rigidbody2D _rigidbody2D = null;
    private float _horizontalInput;
    
    private static readonly int PlayerVelocity = Animator.StringToHash("Horizontal Velocity");

    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _animator.SetFloat(PlayerVelocity, _horizontalInput);
        Movement();
    }

    private void Movement()
    {
        if (_timeDirection.Value != TimeDirection.Forward)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }


        _rigidbody2D.velocity = new Vector2(_horizontalInput * _moveSpeed.Value, _rigidbody2D.velocity.y);
    }
}