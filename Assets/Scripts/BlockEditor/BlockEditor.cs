using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BlockEditor
{
    public class BlockEditor : MonoBehaviour
    {
        [SerializeField] private BlockPrefabsReference[] blockPrefabs;
        [SerializeField] private IntReference levelId;
        [SerializeField] private float distance = 3;
        [SerializeField] private float scrollBlockSize = 3;
        [SerializeField] private float blockScrollSpeed = 5;
        [SerializeField] private Button playButton;

        private Vector3 _destinationPosition;
        private bool _dragging;
        private Vector3 _initialOffset;
        private Vector3 _prevMousePos;
        private List<SelectableElement> _selectableElements;

        private static BlockEditor _instance;

        public static BlockEditor Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public void Start()
        {
            _selectableElements = new List<SelectableElement>();
            var localTransform = transform;
            var localPosition = localTransform.position;
            _destinationPosition = localPosition;
            float offset = 0;

            foreach (var listElement in blockPrefabs[levelId.Value].BlockList)
            {
                Vector3 position = new Vector3(offset + localPosition.x, localPosition.y, localPosition.z);
                GameObject go = Instantiate(listElement, position, Quaternion.identity);
                go.transform.parent = localTransform;
                offset += distance;
                SelectableElement se = go.GetComponent<SelectableElement>();
                if (se != null)
                    _selectableElements.Add(se);
            }
        }

        public void ScrollRightBlockwise()
        {
            ScrollBlockwise(-scrollBlockSize);
        }

        public void ScrollLeftBlockwise()
        {
            ScrollBlockwise(scrollBlockSize);
        }

        private void ScrollBlockwise(float amount)
        {
            _selectableElements.ForEach(element => element.ScrollBy(amount));
        }

        void Update()
        {
            if (!_dragging && transform.position.Equals(_destinationPosition))
            {
                transform.position = Vector3.Lerp(transform.position, _destinationPosition, blockScrollSpeed * Time.deltaTime);
            }

            if (_selectableElements.All(element => element.IsPlaced)) playButton.interactable = true;
        }

        // private void OnMouseDown()
        // {
        //     _dragging = true;
        //
        //     Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        //     Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //     objPosition.y = transform.position.y;
        //
        //     _initialOffset = transform.position - objPosition;
        //     _prevMousePos = mousePosition;
        // }
        //
        // private void OnMouseUp()
        // {
        //     _dragging = false;
        // }
        //
        // void OnMouseDrag()
        // {
        //     Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        //     Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //     objPosition.y = transform.position.y;
        //
        //     transform.position = objPosition + _initialOffset;
        //     _destinationPosition = objPosition + _initialOffset;
        //     
        //     
        // }
    }
}