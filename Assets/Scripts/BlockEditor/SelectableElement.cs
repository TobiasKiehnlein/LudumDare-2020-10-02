using UnityEngine;

namespace BlockEditor
{
    public class SelectableElement : MonoBehaviour
    {
        public bool isPlaced = false;
        [SerializeField] private float blockScrollSpeed = 5;
        [SerializeField] private float snapBackDistance = 5;
        [SerializeField] private Vector3 previewScale = Vector3.one * 0.5f;
        [SerializeField] private float scalingSpeed = 4;

        private bool _dragging;
        private Vector3 _destinationPosition;
        private Vector3 _initialPosition;

        // Start is called before the first frame update
        void Start()
        {
            _initialPosition = transform.position;
            _destinationPosition = transform.position;
            transform.localScale = previewScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_dragging && !transform.position.Equals(_destinationPosition))
            {
                transform.position = Vector3.Lerp(transform.position, _destinationPosition, blockScrollSpeed * Time.deltaTime);
            }

            if (!_dragging && !isPlaced)
            {
                //TODO Lerp to small
                if (transform.localScale != previewScale)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, previewScale, scalingSpeed * Time.deltaTime);
                }
            }
            else
            {
                //TODO Lerp to large
                if (transform.localScale != Vector3.one)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, scalingSpeed * Time.deltaTime);
                }
            }
        }

        private void OnMouseDown()
        {
            _dragging = true;

            _initialPosition = transform.position;
        }

        private void OnMouseUp()
        {
            _dragging = false;
            // if (!((_initalPosition - transform.position).y < snapBackDistance) || isPlaced) return;

            if ((_initialPosition - transform.position).magnitude > snapBackDistance || isPlaced)
            {
                isPlaced = true;
                return;
            }

            _destinationPosition = _initialPosition;
        }

        void OnMouseDrag()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
            _destinationPosition = objPosition;
        }

        public void ScrollBy(float amount)
        {
            if (!isPlaced)
                _destinationPosition.x += amount;
        }
    }
}