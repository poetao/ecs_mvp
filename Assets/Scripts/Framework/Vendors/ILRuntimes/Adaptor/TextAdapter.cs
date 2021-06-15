using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class TextAdapter : CrossBindingAdaptor
{
    static CrossBindingFunctionInfo<UnityEngine.Texture> mget_mainTexture_0 = new CrossBindingFunctionInfo<UnityEngine.Texture>("get_mainTexture");
    static CrossBindingFunctionInfo<System.String> mget_text_1 = new CrossBindingFunctionInfo<System.String>("get_text");
    static CrossBindingMethodInfo<System.String> mset_text_2 = new CrossBindingMethodInfo<System.String>("set_text");
    static CrossBindingMethodInfo mOnEnable_3 = new CrossBindingMethodInfo("OnEnable");
    static CrossBindingMethodInfo mOnDisable_4 = new CrossBindingMethodInfo("OnDisable");
    static CrossBindingMethodInfo mUpdateGeometry_5 = new CrossBindingMethodInfo("UpdateGeometry");
    static CrossBindingMethodInfo<UnityEngine.UI.VertexHelper> mOnPopulateMesh_7 = new CrossBindingMethodInfo<UnityEngine.UI.VertexHelper>("OnPopulateMesh");
    static CrossBindingMethodInfo mCalculateLayoutInputHorizontal_8 = new CrossBindingMethodInfo("CalculateLayoutInputHorizontal");
    static CrossBindingMethodInfo mCalculateLayoutInputVertical_9 = new CrossBindingMethodInfo("CalculateLayoutInputVertical");
    static CrossBindingFunctionInfo<System.Single> mget_minWidth_10 = new CrossBindingFunctionInfo<System.Single>("get_minWidth");
    static CrossBindingFunctionInfo<System.Single> mget_preferredWidth_11 = new CrossBindingFunctionInfo<System.Single>("get_preferredWidth");
    static CrossBindingFunctionInfo<System.Single> mget_flexibleWidth_12 = new CrossBindingFunctionInfo<System.Single>("get_flexibleWidth");
    static CrossBindingFunctionInfo<System.Single> mget_minHeight_13 = new CrossBindingFunctionInfo<System.Single>("get_minHeight");
    static CrossBindingFunctionInfo<System.Single> mget_preferredHeight_14 = new CrossBindingFunctionInfo<System.Single>("get_preferredHeight");
    static CrossBindingFunctionInfo<System.Single> mget_flexibleHeight_15 = new CrossBindingFunctionInfo<System.Single>("get_flexibleHeight");
    static CrossBindingFunctionInfo<System.Int32> mget_layoutPriority_16 = new CrossBindingFunctionInfo<System.Int32>("get_layoutPriority");
    static CrossBindingFunctionInfo<UnityEngine.Material, UnityEngine.Material> mGetModifiedMaterial_19 = new CrossBindingFunctionInfo<UnityEngine.Material, UnityEngine.Material>("GetModifiedMaterial");
    static CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean> mCull_20 = new CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean>("Cull");
    static CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean> mSetClipRect_21 = new CrossBindingMethodInfo<UnityEngine.Rect, System.Boolean>("SetClipRect");
    static CrossBindingMethodInfo mOnTransformParentChanged_22 = new CrossBindingMethodInfo("OnTransformParentChanged");
    static CrossBindingMethodInfo mOnCanvasHierarchyChanged_24 = new CrossBindingMethodInfo("OnCanvasHierarchyChanged");
    static CrossBindingMethodInfo mRecalculateClipping_25 = new CrossBindingMethodInfo("RecalculateClipping");
    static CrossBindingMethodInfo mRecalculateMasking_26 = new CrossBindingMethodInfo("RecalculateMasking");
    static CrossBindingFunctionInfo<UnityEngine.Color> mget_color_27 = new CrossBindingFunctionInfo<UnityEngine.Color>("get_color");
    static CrossBindingMethodInfo<UnityEngine.Color> mset_color_28 = new CrossBindingMethodInfo<UnityEngine.Color>("set_color");
    static CrossBindingFunctionInfo<System.Boolean> mget_raycastTarget_29 = new CrossBindingFunctionInfo<System.Boolean>("get_raycastTarget");
    static CrossBindingMethodInfo<System.Boolean> mset_raycastTarget_30 = new CrossBindingMethodInfo<System.Boolean>("set_raycastTarget");
    static CrossBindingMethodInfo mSetAllDirty_31 = new CrossBindingMethodInfo("SetAllDirty");
    static CrossBindingMethodInfo mSetLayoutDirty_32 = new CrossBindingMethodInfo("SetLayoutDirty");
    static CrossBindingMethodInfo mSetVerticesDirty_33 = new CrossBindingMethodInfo("SetVerticesDirty");
    static CrossBindingMethodInfo mSetMaterialDirty_34 = new CrossBindingMethodInfo("SetMaterialDirty");
    static CrossBindingMethodInfo mOnRectTransformDimensionsChange_35 = new CrossBindingMethodInfo("OnRectTransformDimensionsChange");
    static CrossBindingMethodInfo mOnBeforeTransformParentChanged_36 = new CrossBindingMethodInfo("OnBeforeTransformParentChanged");
    static CrossBindingFunctionInfo<UnityEngine.RectTransform> mget_rectTransform_37 = new CrossBindingFunctionInfo<UnityEngine.RectTransform>("get_rectTransform");
    static CrossBindingFunctionInfo<UnityEngine.Material> mget_defaultMaterial_38 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_defaultMaterial");
    static CrossBindingFunctionInfo<UnityEngine.Material> mget_material_39 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_material");
    static CrossBindingMethodInfo<UnityEngine.Material> mset_material_40 = new CrossBindingMethodInfo<UnityEngine.Material>("set_material");
    static CrossBindingFunctionInfo<UnityEngine.Material> mget_materialForRendering_41 = new CrossBindingFunctionInfo<UnityEngine.Material>("get_materialForRendering");
    static CrossBindingMethodInfo mOnCullingChanged_42 = new CrossBindingMethodInfo("OnCullingChanged");
    static CrossBindingMethodInfo<UnityEngine.UI.CanvasUpdate> mRebuild_43 = new CrossBindingMethodInfo<UnityEngine.UI.CanvasUpdate>("Rebuild");
    static CrossBindingMethodInfo mLayoutComplete_44 = new CrossBindingMethodInfo("LayoutComplete");
    static CrossBindingMethodInfo mGraphicUpdateComplete_45 = new CrossBindingMethodInfo("GraphicUpdateComplete");
    static CrossBindingMethodInfo mUpdateMaterial_46 = new CrossBindingMethodInfo("UpdateMaterial");
    static CrossBindingMethodInfo<System.Collections.Generic.List<UnityEngine.UIVertex>> mOnFillVBO_47 = new CrossBindingMethodInfo<System.Collections.Generic.List<UnityEngine.UIVertex>>("OnFillVBO");
    static CrossBindingMethodInfo<UnityEngine.Mesh> mOnPopulateMesh_48 = new CrossBindingMethodInfo<UnityEngine.Mesh>("OnPopulateMesh");
    static CrossBindingMethodInfo mOnDidApplyAnimationProperties_49 = new CrossBindingMethodInfo("OnDidApplyAnimationProperties");
    static CrossBindingMethodInfo mSetNativeSize_50 = new CrossBindingMethodInfo("SetNativeSize");
    static CrossBindingFunctionInfo<UnityEngine.Vector2, UnityEngine.Camera, System.Boolean> mRaycast_51 = new CrossBindingFunctionInfo<UnityEngine.Vector2, UnityEngine.Camera, System.Boolean>("Raycast");
    static CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean> mCrossFadeColor_52 = new CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean>("CrossFadeColor");
    static CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean, System.Boolean> mCrossFadeColor_53 = new CrossBindingMethodInfo<UnityEngine.Color, System.Single, System.Boolean, System.Boolean, System.Boolean>("CrossFadeColor");
    static CrossBindingMethodInfo<System.Single, System.Single, System.Boolean> mCrossFadeAlpha_54 = new CrossBindingMethodInfo<System.Single, System.Single, System.Boolean>("CrossFadeAlpha");
    static CrossBindingMethodInfo mAwake_55 = new CrossBindingMethodInfo("Awake");
    static CrossBindingMethodInfo mStart_56 = new CrossBindingMethodInfo("Start");
    static CrossBindingMethodInfo mOnDestroy_57 = new CrossBindingMethodInfo("OnDestroy");
    static CrossBindingFunctionInfo<System.Boolean> mIsActive_58 = new CrossBindingFunctionInfo<System.Boolean>("IsActive");
    static CrossBindingMethodInfo mOnCanvasGroupChanged_59 = new CrossBindingMethodInfo("OnCanvasGroupChanged");
    static CrossBindingFunctionInfo<System.Boolean> mIsDestroyed_60 = new CrossBindingFunctionInfo<System.Boolean>("IsDestroyed");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityEngine.UI.Text);
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

    public class Adapter : UnityEngine.UI.Text, CrossBindingAdaptorType
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

        protected override void OnEnable()
        {
            if (mOnEnable_3.CheckShouldInvokeBase(this.instance))
                base.OnEnable();
            else
                mOnEnable_3.Invoke(this.instance);
        }

        protected override void OnDisable()
        {
            if (mOnDisable_4.CheckShouldInvokeBase(this.instance))
                base.OnDisable();
            else
                mOnDisable_4.Invoke(this.instance);
        }

        protected override void UpdateGeometry()
        {
            if (mUpdateGeometry_5.CheckShouldInvokeBase(this.instance))
                base.UpdateGeometry();
            else
                mUpdateGeometry_5.Invoke(this.instance);
        }

        protected override void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill)
        {
            if (mOnPopulateMesh_7.CheckShouldInvokeBase(this.instance))
                base.OnPopulateMesh(toFill);
            else
                mOnPopulateMesh_7.Invoke(this.instance, toFill);
        }

        public override void CalculateLayoutInputHorizontal()
        {
            if (mCalculateLayoutInputHorizontal_8.CheckShouldInvokeBase(this.instance))
                base.CalculateLayoutInputHorizontal();
            else
                mCalculateLayoutInputHorizontal_8.Invoke(this.instance);
        }

        public override void CalculateLayoutInputVertical()
        {
            if (mCalculateLayoutInputVertical_9.CheckShouldInvokeBase(this.instance))
                base.CalculateLayoutInputVertical();
            else
                mCalculateLayoutInputVertical_9.Invoke(this.instance);
        }

        public override UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial)
        {
            if (mGetModifiedMaterial_19.CheckShouldInvokeBase(this.instance))
                return base.GetModifiedMaterial(baseMaterial);
            else
                return mGetModifiedMaterial_19.Invoke(this.instance, baseMaterial);
        }

        public override void Cull(UnityEngine.Rect clipRect, System.Boolean validRect)
        {
            if (mCull_20.CheckShouldInvokeBase(this.instance))
                base.Cull(clipRect, validRect);
            else
                mCull_20.Invoke(this.instance, clipRect, validRect);
        }

        public override void SetClipRect(UnityEngine.Rect clipRect, System.Boolean validRect)
        {
            if (mSetClipRect_21.CheckShouldInvokeBase(this.instance))
                base.SetClipRect(clipRect, validRect);
            else
                mSetClipRect_21.Invoke(this.instance, clipRect, validRect);
        }

        protected override void OnTransformParentChanged()
        {
            if (mOnTransformParentChanged_22.CheckShouldInvokeBase(this.instance))
                base.OnTransformParentChanged();
            else
                mOnTransformParentChanged_22.Invoke(this.instance);
        }

        protected override void OnCanvasHierarchyChanged()
        {
            if (mOnCanvasHierarchyChanged_24.CheckShouldInvokeBase(this.instance))
                base.OnCanvasHierarchyChanged();
            else
                mOnCanvasHierarchyChanged_24.Invoke(this.instance);
        }

        public override void RecalculateClipping()
        {
            if (mRecalculateClipping_25.CheckShouldInvokeBase(this.instance))
                base.RecalculateClipping();
            else
                mRecalculateClipping_25.Invoke(this.instance);
        }

        public override void RecalculateMasking()
        {
            if (mRecalculateMasking_26.CheckShouldInvokeBase(this.instance))
                base.RecalculateMasking();
            else
                mRecalculateMasking_26.Invoke(this.instance);
        }

        public override void SetAllDirty()
        {
            if (mSetAllDirty_31.CheckShouldInvokeBase(this.instance))
                base.SetAllDirty();
            else
                mSetAllDirty_31.Invoke(this.instance);
        }

        public override void SetLayoutDirty()
        {
            if (mSetLayoutDirty_32.CheckShouldInvokeBase(this.instance))
                base.SetLayoutDirty();
            else
                mSetLayoutDirty_32.Invoke(this.instance);
        }

        public override void SetVerticesDirty()
        {
            if (mSetVerticesDirty_33.CheckShouldInvokeBase(this.instance))
                base.SetVerticesDirty();
            else
                mSetVerticesDirty_33.Invoke(this.instance);
        }

        public override void SetMaterialDirty()
        {
            if (mSetMaterialDirty_34.CheckShouldInvokeBase(this.instance))
                base.SetMaterialDirty();
            else
                mSetMaterialDirty_34.Invoke(this.instance);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            if (mOnRectTransformDimensionsChange_35.CheckShouldInvokeBase(this.instance))
                base.OnRectTransformDimensionsChange();
            else
                mOnRectTransformDimensionsChange_35.Invoke(this.instance);
        }

        protected override void OnBeforeTransformParentChanged()
        {
            if (mOnBeforeTransformParentChanged_36.CheckShouldInvokeBase(this.instance))
                base.OnBeforeTransformParentChanged();
            else
                mOnBeforeTransformParentChanged_36.Invoke(this.instance);
        }

        public override void OnCullingChanged()
        {
            if (mOnCullingChanged_42.CheckShouldInvokeBase(this.instance))
                base.OnCullingChanged();
            else
                mOnCullingChanged_42.Invoke(this.instance);
        }

        public override void Rebuild(UnityEngine.UI.CanvasUpdate update)
        {
            if (mRebuild_43.CheckShouldInvokeBase(this.instance))
                base.Rebuild(update);
            else
                mRebuild_43.Invoke(this.instance, update);
        }

        public override void LayoutComplete()
        {
            if (mLayoutComplete_44.CheckShouldInvokeBase(this.instance))
                base.LayoutComplete();
            else
                mLayoutComplete_44.Invoke(this.instance);
        }

        public override void GraphicUpdateComplete()
        {
            if (mGraphicUpdateComplete_45.CheckShouldInvokeBase(this.instance))
                base.GraphicUpdateComplete();
            else
                mGraphicUpdateComplete_45.Invoke(this.instance);
        }

        protected override void UpdateMaterial()
        {
            if (mUpdateMaterial_46.CheckShouldInvokeBase(this.instance))
                base.UpdateMaterial();
            else
                mUpdateMaterial_46.Invoke(this.instance);
        }

        protected override void OnDidApplyAnimationProperties()
        {
            if (mOnDidApplyAnimationProperties_49.CheckShouldInvokeBase(this.instance))
                base.OnDidApplyAnimationProperties();
            else
                mOnDidApplyAnimationProperties_49.Invoke(this.instance);
        }

        public override void SetNativeSize()
        {
            if (mSetNativeSize_50.CheckShouldInvokeBase(this.instance))
                base.SetNativeSize();
            else
                mSetNativeSize_50.Invoke(this.instance);
        }

        public override System.Boolean Raycast(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
        {
            if (mRaycast_51.CheckShouldInvokeBase(this.instance))
                return base.Raycast(sp, eventCamera);
            else
                return mRaycast_51.Invoke(this.instance, sp, eventCamera);
        }

        public override void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha)
        {
            if (mCrossFadeColor_52.CheckShouldInvokeBase(this.instance))
                base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
            else
                mCrossFadeColor_52.Invoke(this.instance, targetColor, duration, ignoreTimeScale, useAlpha);
        }

        public override void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha, System.Boolean useRGB)
        {
            if (mCrossFadeColor_53.CheckShouldInvokeBase(this.instance))
                base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
            else
                mCrossFadeColor_53.Invoke(this.instance, targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
        }

        public override void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale)
        {
            if (mCrossFadeAlpha_54.CheckShouldInvokeBase(this.instance))
                base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
            else
                mCrossFadeAlpha_54.Invoke(this.instance, alpha, duration, ignoreTimeScale);
        }

        protected override void Awake()
        {
            if (mAwake_55.CheckShouldInvokeBase(this.instance))
                base.Awake();
            else
                mAwake_55.Invoke(this.instance);
        }

        protected override void Start()
        {
            if (mStart_56.CheckShouldInvokeBase(this.instance))
                base.Start();
            else
                mStart_56.Invoke(this.instance);
        }

        protected override void OnDestroy()
        {
            if (mOnDestroy_57.CheckShouldInvokeBase(this.instance))
                base.OnDestroy();
            else
                mOnDestroy_57.Invoke(this.instance);
        }

        public override System.Boolean IsActive()
        {
            if (mIsActive_58.CheckShouldInvokeBase(this.instance))
                return base.IsActive();
            else
                return mIsActive_58.Invoke(this.instance);
        }

        protected override void OnCanvasGroupChanged()
        {
            if (mOnCanvasGroupChanged_59.CheckShouldInvokeBase(this.instance))
                base.OnCanvasGroupChanged();
            else
                mOnCanvasGroupChanged_59.Invoke(this.instance);
        }

        public System.Boolean IsDestroyed()
        {
            if (mIsDestroyed_60.CheckShouldInvokeBase(this.instance))
                return base.IsDestroyed();
            else
                return mIsDestroyed_60.Invoke(this.instance);
        }

        public override UnityEngine.Texture mainTexture
        {
        get
        {
            if (mget_mainTexture_0.CheckShouldInvokeBase(this.instance))
                return base.mainTexture;
            else
                return mget_mainTexture_0.Invoke(this.instance);

        }
        }

        public override System.String text
        {
        get
        {
            if (mget_text_1.CheckShouldInvokeBase(this.instance))
                return base.text;
            else
                return mget_text_1.Invoke(this.instance);

        }
        set
        {
            if (mset_text_2.CheckShouldInvokeBase(this.instance))
                base.text = value;
            else
                mset_text_2.Invoke(this.instance, value);

        }
        }

        public override System.Single minWidth
        {
        get
        {
            if (mget_minWidth_10.CheckShouldInvokeBase(this.instance))
                return base.minWidth;
            else
                return mget_minWidth_10.Invoke(this.instance);

        }
        }

        public override System.Single preferredWidth
        {
        get
        {
            if (mget_preferredWidth_11.CheckShouldInvokeBase(this.instance))
                return base.preferredWidth;
            else
                return mget_preferredWidth_11.Invoke(this.instance);

        }
        }

        public override System.Single flexibleWidth
        {
        get
        {
            if (mget_flexibleWidth_12.CheckShouldInvokeBase(this.instance))
                return base.flexibleWidth;
            else
                return mget_flexibleWidth_12.Invoke(this.instance);

        }
        }

        public override System.Single minHeight
        {
        get
        {
            if (mget_minHeight_13.CheckShouldInvokeBase(this.instance))
                return base.minHeight;
            else
                return mget_minHeight_13.Invoke(this.instance);

        }
        }

        public override System.Single preferredHeight
        {
        get
        {
            if (mget_preferredHeight_14.CheckShouldInvokeBase(this.instance))
                return base.preferredHeight;
            else
                return mget_preferredHeight_14.Invoke(this.instance);

        }
        }

        public override System.Single flexibleHeight
        {
        get
        {
            if (mget_flexibleHeight_15.CheckShouldInvokeBase(this.instance))
                return base.flexibleHeight;
            else
                return mget_flexibleHeight_15.Invoke(this.instance);

        }
        }

        public override System.Int32 layoutPriority
        {
        get
        {
            if (mget_layoutPriority_16.CheckShouldInvokeBase(this.instance))
                return base.layoutPriority;
            else
                return mget_layoutPriority_16.Invoke(this.instance);

        }
        }

        public override UnityEngine.Color color
        {
        get
        {
            if (mget_color_27.CheckShouldInvokeBase(this.instance))
                return base.color;
            else
                return mget_color_27.Invoke(this.instance);

        }
        set
        {
            if (mset_color_28.CheckShouldInvokeBase(this.instance))
                base.color = value;
            else
                mset_color_28.Invoke(this.instance, value);

        }
        }

        public override System.Boolean raycastTarget
        {
        get
        {
            if (mget_raycastTarget_29.CheckShouldInvokeBase(this.instance))
                return base.raycastTarget;
            else
                return mget_raycastTarget_29.Invoke(this.instance);

        }
        set
        {
            if (mset_raycastTarget_30.CheckShouldInvokeBase(this.instance))
                base.raycastTarget = value;
            else
                mset_raycastTarget_30.Invoke(this.instance, value);

        }
        }

        public UnityEngine.RectTransform rectTransform
        {
        get
        {
            if (mget_rectTransform_37.CheckShouldInvokeBase(this.instance))
                return base.rectTransform;
            else
                return mget_rectTransform_37.Invoke(this.instance);

        }
        }

        public override UnityEngine.Material defaultMaterial
        {
        get
        {
            if (mget_defaultMaterial_38.CheckShouldInvokeBase(this.instance))
                return base.defaultMaterial;
            else
                return mget_defaultMaterial_38.Invoke(this.instance);

        }
        }

        public override UnityEngine.Material material
        {
        get
        {
            if (mget_material_39.CheckShouldInvokeBase(this.instance))
                return base.material;
            else
                return mget_material_39.Invoke(this.instance);

        }
        set
        {
            if (mset_material_40.CheckShouldInvokeBase(this.instance))
                base.material = value;
            else
                mset_material_40.Invoke(this.instance, value);

        }
        }

        public override UnityEngine.Material materialForRendering
        {
        get
        {
            if (mget_materialForRendering_41.CheckShouldInvokeBase(this.instance))
                return base.materialForRendering;
            else
                return mget_materialForRendering_41.Invoke(this.instance);

        }
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

