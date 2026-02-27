using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UsefulExtensions.UI
{
    /// <summary>
    /// Automatically adjusts the size of a ScrollRect container to match its content size,
    /// up to a maximum allowed size.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class ScrollViewSizeFitter : UIBehaviour, ILayoutSelfController
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform.Axis axis = RectTransform.Axis.Vertical;
        [SerializeField] private bool useParentSize = false;

        private float _maxSize;
        private DrivenRectTransformTracker _tracker;

        protected override void Awake()
        {
            base.Awake();
            UpdateMaxSize();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateMaxSize();
            SetDirty();
        }

        protected override void OnDisable()
        {
            _tracker.Clear();
            base.OnDisable();
        }

        // Called by the Unity Layout System
        public void SetLayoutHorizontal()
        {
            if (axis == RectTransform.Axis.Horizontal) UpdateSize();
        }

        public void SetLayoutVertical()
        {
            if (axis == RectTransform.Axis.Vertical) UpdateSize();
        }

        private void UpdateMaxSize()
        {
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            
            float currentSize;
            if (useParentSize && rectTransform.parent is RectTransform parentRect)
            {
                currentSize = (axis == RectTransform.Axis.Horizontal) ? parentRect.rect.width : parentRect.rect.height;
            }
            else
            {
                currentSize = (axis == RectTransform.Axis.Horizontal) ? rectTransform.rect.width : rectTransform.rect.height;
            }

            _maxSize = Mathf.Max(_maxSize, currentSize);
        }

        private void UpdateSize()
        {
            if (scrollRect == null || scrollRect.content == null || rectTransform == null) return;

            _tracker.Clear();
            _tracker.Add(this, rectTransform, (axis == RectTransform.Axis.Horizontal) 
                ? DrivenTransformProperties.SizeDeltaX 
                : DrivenTransformProperties.SizeDeltaY);

            float contentSize = (axis == RectTransform.Axis.Horizontal) 
                ? scrollRect.content.rect.width 
                : scrollRect.content.rect.height;

            float newSize = Mathf.Min(contentSize, _maxSize);
            rectTransform.SetSizeWithCurrentAnchors(axis, newSize);
        }

        private void SetDirty()
        {
            if (!IsActive()) return;
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            if (scrollRect == null) scrollRect = GetComponentInChildren<ScrollRect>();
            UpdateMaxSize();
        }
#endif

        protected override void OnRectTransformDimensionsChange()
        {
            UpdateMaxSize();
            SetDirty();
        }
    }
}
