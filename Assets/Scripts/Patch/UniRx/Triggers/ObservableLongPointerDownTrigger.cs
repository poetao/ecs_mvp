using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRx.Triggers
{
    public class ObservableLongPointerDownTrigger : ObservableTriggerBase, IPointerDownHandler, IPointerUpHandler
    {
        public float IntervalSecond = 1f;

        Subject<Unit> onLongPointerDown;

        float? raiseTime;

        void Update()
        {
            if (raiseTime != null && raiseTime <= Time.realtimeSinceStartup)
            {
                if (onLongPointerDown != null) onLongPointerDown.OnNext(Unit.Default);
                raiseTime = null;
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            raiseTime = Time.realtimeSinceStartup + IntervalSecond;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            raiseTime = null;
        }

        public IObservable<Unit> OnLongPointerDownAsObservable()
        {
            return onLongPointerDown ?? (onLongPointerDown = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onLongPointerDown != null)
            {
                onLongPointerDown.OnCompleted();
            }
        }
    }
}
