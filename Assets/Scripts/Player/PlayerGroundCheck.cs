using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    #region References

    [SerializeField]
    private BoolReference _isGrounded = null;

    #endregion

    #region Variables

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _groundCheckRadius;

    #endregion


    private void Update()
    {
        _isGrounded.Value = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded.Value ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}