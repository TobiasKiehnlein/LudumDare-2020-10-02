using System.Diagnostics.Contracts;
using UnityEngine;

namespace BlockEditor
{
    public class SelectableElement : MonoBehaviour
    {
        private bool _isPlaced = false;
        [SerializeField] private float blockScrollSpeed = 5;
        [SerializeField] private float snapBackDistance = 5;
        [SerializeField] private Vector3 previewScale = Vector3.one * 0.5f;
        [SerializeField] private float scalingSpeed = 4;
        [SerializeField] private TimeDirectionReference _timeDirection;
        [SerializeField] private BoxCollider2D _editorHitbox;

        public bool IsPlaced
        {
            get => _isPlaced;
            set
            {
                _isPlaced = value;
                UpdateHitboxEnabledState();
            }
        }

        public bool IsDraggedOut => _isDraggedOut;

        private bool _dragging;

        // private Vector3 _destinationPosition;
        private Vector3 _initialPosition;
        private bool _isDraggedOut;

        // Start is called before the first frame update
        void Start()
        {
            UpdateHitboxEnabledState();
            _initialPosition = transform.position;
            // _destinationPosition = _initialPosition;
            transform.localScale = previewScale;
        }

        void Update()
        {
            if (_timeDirection.Value == TimeDirection.Forward)
            {
                return;
            }

            // if (!_dragging && !transform.position.Equals(_destinationPosition))
            // {
            //     transform.position = Vector3.Lerp(transform.position, _destinationPosition,
            //         blockScrollSpeed * Time.deltaTime);
            // }

            if (!_isDraggedOut && !_isPlaced && !_dragging)
            {
                if (transform.localScale != previewScale)
                {
                    transform.localScale =
                        Vector3.Lerp(transform.localScale, previewScale, scalingSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (transform.localScale != Vector3.one)
                {
                    transform.localScale =
                        Vector3.Lerp(transform.localScale, Vector3.one, scalingSpeed * Time.deltaTime);
                }
            }
        }

        private void OnMouseDown()
        {
            if (IsPlaced) return;
            _dragging = true;

            _initialPosition = transform.position;
        }

        private void OnMouseUp()
        {
            if (IsPlaced) return;
            _dragging = false;
            // if (!((_initalPosition - transform.position).y < snapBackDistance) || isPlaced) return;

            if ((_initialPosition - transform.position).magnitude > snapBackDistance || _isDraggedOut)
            {
                _isDraggedOut = true;
                return;
            }

            // _destinationPosition = _initialPosition;
        }

        void OnMouseDrag()
        {
            if (IsPlaced) return;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
            // _destinationPosition = objPosition;
        }

        public void ScrollBy(float amount)
        {
            // if (!IsPlaced)
            //     _destinationPosition.x += amount;
        }

        public void UpdateHitboxEnabledState()
        {
            if (!_editorHitbox)
            {
                return;
            }

            _editorHitbox.enabled = !_isPlaced;
        }
    }
}