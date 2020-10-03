using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFall : MonoBehaviour
{
    #region References

    [SerializeField] private BoolReference _isFalling = null;
    [SerializeField] private InputActionAsset _controls;

    #endregion

    #region Variables

    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    private Rigidbody2D _rigidbody2D = null;

    #endregion


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetFalling();

        if (_isFalling.Value)
            Fall();
        else if (_rigidbody2D.velocity.y > 0 && _controls.FindAction("Jump").ReadValue<float>() < 1)
            ShortJumpFall();
    }


    private void SetFalling()
    {
        _isFalling.Value = _rigidbody2D.velocity.y < 0;
    }


    private void Fall()
    {
        var fallVelocity = _rigidbody2D.velocity +
                           Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime);

        _rigidbody2D.velocity = fallVelocity;
    }

    private void ShortJumpFall()
    {
        _rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime);
    }
}