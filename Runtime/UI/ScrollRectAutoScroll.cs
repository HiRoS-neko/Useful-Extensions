using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using UnityEngine.InputSystem.UI;

namespace UsefulExtensions.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectAutoScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float scrollSpeed = 10f;
        private bool mouseOver = false;

        private List<Selectable> _selectables = new List<Selectable>();
        private ScrollRect _scrollRect;
        private InputSystemUIInputModule _inputModule;

        private Vector2 _nextScrollPosition = Vector2.up;

        private void OnEnable()
        {
            if (_scrollRect)
            {
                _scrollRect.content.GetComponentsInChildren(_selectables);
            }
        }

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            if (_scrollRect)
            {
                _scrollRect.content.GetComponentsInChildren(_selectables);
            }

            ScrollToSelected(true);
        }

        private void Update()
        {
            // Scroll via input.
            InputScroll();
            if (!mouseOver)
            {
                // Lerp scrolling code.
                _scrollRect.normalizedPosition = Vector2.Lerp(_scrollRect.normalizedPosition, _nextScrollPosition,
                    scrollSpeed * Time.unscaledDeltaTime);
            }
            else
            {
                _nextScrollPosition = _scrollRect.normalizedPosition;
            }
        }

#nullable enable
        private void InputScroll()
        {
            if (_selectables.Count > 0)
            {
                if (_inputModule == null)
                {
                    _inputModule = EventSystem.current.currentInputModule as InputSystemUIInputModule;
                }

                if (_inputModule != null)
                {
                    var moveAction = _inputModule.move.action;
                    if (moveAction != null && moveAction.triggered)
                    {
                        var moveValue = moveAction.ReadValue<Vector2>();
                        if (Mathf.Abs(moveValue.y) > 0.1f)
                        {
                            ScrollToSelected(false);
                        }
                    }
                }
            }
        }
#nullable disable
        private void ScrollToSelected(bool quickScroll)
        {
            var selectedIndex = -1;
            var selectedElement = EventSystem.current.currentSelectedGameObject
                ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>()
                : null;

            if (selectedElement)
            {
                selectedIndex = _selectables.IndexOf(selectedElement);
            }

            if (selectedIndex > -1)
            {
                if (quickScroll)
                {
                    _scrollRect.normalizedPosition =
                        new Vector2(0, 1 - (selectedIndex / ((float)_selectables.Count - 1)));
                    _nextScrollPosition = _scrollRect.normalizedPosition;
                }
                else
                {
                    _nextScrollPosition = new Vector2(0, 1 - (selectedIndex / ((float)_selectables.Count - 1)));
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOver = false;
            ScrollToSelected(false);
        }
    }
}
