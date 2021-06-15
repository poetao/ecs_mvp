using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;


public class EventTriggerAdapter : CrossBindingAdaptor
{
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerEnter_0 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerEnter");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerExit_1 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerExit");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnDrag_2 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnDrag");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnDrop_3 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnDrop");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerDown_4 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerDown");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerUp_5 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerUp");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerClick_6 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerClick");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData> mOnSelect_7 = new CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData>("OnSelect");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData> mOnDeselect_8 = new CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData>("OnDeselect");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnScroll_9 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnScroll");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.AxisEventData> mOnMove_10 = new CrossBindingMethodInfo<UnityEngine.EventSystems.AxisEventData>("OnMove");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData> mOnUpdateSelected_11 = new CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData>("OnUpdateSelected");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnInitializePotentialDrag_12 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnInitializePotentialDrag");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnBeginDrag_13 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnBeginDrag");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnEndDrag_14 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnEndDrag");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData> mOnSubmit_15 = new CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData>("OnSubmit");
    static CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData> mOnCancel_16 = new CrossBindingMethodInfo<UnityEngine.EventSystems.BaseEventData>("OnCancel");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityEngine.EventSystems.EventTrigger);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adapter);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adapter(appdomain, instance);
    }

    public class Adapter : UnityEngine.EventSystems.EventTrigger, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adapter()
        {

        }

        public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

        public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnPointerEnter_0.CheckShouldInvokeBase(this.instance))
                base.OnPointerEnter(eventData);
            else
                mOnPointerEnter_0.Invoke(this.instance, eventData);
        }

        public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnPointerExit_1.CheckShouldInvokeBase(this.instance))
                base.OnPointerExit(eventData);
            else
                mOnPointerExit_1.Invoke(this.instance, eventData);
        }

        public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnDrag_2.CheckShouldInvokeBase(this.instance))
                base.OnDrag(eventData);
            else
                mOnDrag_2.Invoke(this.instance, eventData);
        }

        public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnDrop_3.CheckShouldInvokeBase(this.instance))
                base.OnDrop(eventData);
            else
                mOnDrop_3.Invoke(this.instance, eventData);
        }

        public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnPointerDown_4.CheckShouldInvokeBase(this.instance))
                base.OnPointerDown(eventData);
            else
                mOnPointerDown_4.Invoke(this.instance, eventData);
        }

        public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnPointerUp_5.CheckShouldInvokeBase(this.instance))
                base.OnPointerUp(eventData);
            else
                mOnPointerUp_5.Invoke(this.instance, eventData);
        }

        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnPointerClick_6.CheckShouldInvokeBase(this.instance))
                base.OnPointerClick(eventData);
            else
                mOnPointerClick_6.Invoke(this.instance, eventData);
        }

        public override void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
        {
            if (mOnSelect_7.CheckShouldInvokeBase(this.instance))
                base.OnSelect(eventData);
            else
                mOnSelect_7.Invoke(this.instance, eventData);
        }

        public override void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
        {
            if (mOnDeselect_8.CheckShouldInvokeBase(this.instance))
                base.OnDeselect(eventData);
            else
                mOnDeselect_8.Invoke(this.instance, eventData);
        }

        public override void OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnScroll_9.CheckShouldInvokeBase(this.instance))
                base.OnScroll(eventData);
            else
                mOnScroll_9.Invoke(this.instance, eventData);
        }

        public override void OnMove(UnityEngine.EventSystems.AxisEventData eventData)
        {
            if (mOnMove_10.CheckShouldInvokeBase(this.instance))
                base.OnMove(eventData);
            else
                mOnMove_10.Invoke(this.instance, eventData);
        }

        public override void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
        {
            if (mOnUpdateSelected_11.CheckShouldInvokeBase(this.instance))
                base.OnUpdateSelected(eventData);
            else
                mOnUpdateSelected_11.Invoke(this.instance, eventData);
        }

        public override void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnInitializePotentialDrag_12.CheckShouldInvokeBase(this.instance))
                base.OnInitializePotentialDrag(eventData);
            else
                mOnInitializePotentialDrag_12.Invoke(this.instance, eventData);
        }

        public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnBeginDrag_13.CheckShouldInvokeBase(this.instance))
                base.OnBeginDrag(eventData);
            else
                mOnBeginDrag_13.Invoke(this.instance, eventData);
        }

        public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (mOnEndDrag_14.CheckShouldInvokeBase(this.instance))
                base.OnEndDrag(eventData);
            else
                mOnEndDrag_14.Invoke(this.instance, eventData);
        }

        public override void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
        {
            if (mOnSubmit_15.CheckShouldInvokeBase(this.instance))
                base.OnSubmit(eventData);
            else
                mOnSubmit_15.Invoke(this.instance, eventData);
        }

        public override void OnCancel(UnityEngine.EventSystems.BaseEventData eventData)
        {
            if (mOnCancel_16.CheckShouldInvokeBase(this.instance))
                base.OnCancel(eventData);
            else
                mOnCancel_16.Invoke(this.instance, eventData);
        }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }
    }
}

