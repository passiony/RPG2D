    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class TouchButton :MonoBehaviour,IPointerUpHandler,IPointerDownHandler
    {
        public UnityEvent OnDown;
        public UnityEvent OnUp;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke();
        }
    }
