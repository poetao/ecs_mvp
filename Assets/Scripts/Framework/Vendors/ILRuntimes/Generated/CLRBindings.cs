using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            System_String_Binding.Register(app);
            Framework_Core_Logs_Logger_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncVoidMethodBuilder_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Tuple_2_Dictionary_2_String_ILTypeInstance_AsyncOperation_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Object_Binding.Register(app);
            System_Threading_Tasks_Task_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Tuple_2_String_Object_Binding.Register(app);
            System_Threading_Tasks_Task_1_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Tuple_Binding.Register(app);
            System_Threading_Tasks_Task_1_AsyncOperation_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_AsyncOperation_Binding.Register(app);
            System_Collections_Generic_List_1_IObservable_1_Tuple_2_String_Object_Binding.Register(app);
            UniRx_Observable_Binding.Register(app);
            UniRx_AsyncSubject_1_Tuple_2_String_Object_Array_Binding.Register(app);
            System_Tuple_2_String_Object_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Tuple_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_IObservable_1_Tuple_2_String_ILTypeInstance_Binding.Register(app);
            System_Threading_Tasks_Task_1_ILTypeInstance_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_ILTypeInstance_Binding.Register(app);
            System_Tuple_2_String_ILTypeInstance_Binding.Register(app);
            System_Linq_Enumerable_Binding.Register(app);
            UniRx_AsyncSubject_1_Tuple_2_String_ILTypeInstance_Array_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Threading_Tasks_Task_1_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            System_Threading_Tasks_Task_1_Object_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Object_Binding.Register(app);
            System_Tuple_2_Dictionary_2_String_ILTypeInstance_AsyncOperation_Binding.Register(app);
            System_Threading_Tasks_Task_1_Tuple_2_Dictionary_2_String_ILTypeInstance_AsyncOperation_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Tuple_2_Dictionary_2_String_ILTypeInstance_AsyncOperation_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Boolean_Binding.Register(app);
            System_Threading_Tasks_Task_1_Boolean_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Boolean_Binding.Register(app);
            UniRx_AsyncSubject_1_Int64_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_List_1_String_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_Array_Binding.Register(app);
            UniRx_ObservableExtensions_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            UnityEngine_Mathf_Binding.Register(app);
            System_Func_2_ILTypeInstance_Task_1_Object_Binding.Register(app);
            System_Func_1_Task_1_Object_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            System_DateTime_Binding.Register(app);
            System_GC_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_ILTypeInstance_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Int32_ILTypeInstance_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            System_Collections_Generic_IEnumerable_1_Object_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_Object_Binding.Register(app);
            System_Action_2_CoroutineAdapter_Binding_Adaptor_Object_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_String_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Dictionary_2_Type_WeakReference_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_WeakReference_1_ILTypeInstance_Binding.Register(app);
            System_WeakReference_1_ILTypeInstance_Binding.Register(app);
            UniRx_Triggers_ObservableTriggerExtensions_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            UniRx_CompositeDisposable_Binding.Register(app);
            System_Type_Binding.Register(app);
            UniRx_DisposableExtensions_Binding.Register(app);
            System_Math_Binding.Register(app);
            System_Collections_Generic_List_1_Subject_1_Boolean_Binding.Register(app);
            System_Collections_Generic_List_1_Subject_1_Boolean_Binding_Enumerator_Binding.Register(app);
            UniRx_Subject_1_Boolean_Binding.Register(app);
            UniRx_AsyncSubject_1_Boolean_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_CoroutineAdapter_Binding_Adaptor_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_AsyncOperation_Binding.Register(app);
            System_IO_Path_Binding.Register(app);
            UnityEngine_AssetBundle_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_ILTypeInstance_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_AssetBundleManifest_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Boolean_Binding.Register(app);
            UnityEngine_AssetBundleManifest_Binding.Register(app);
            System_NotImplementedException_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_AssetBundle_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Int32_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);
            UnityEngine_SceneManagement_SceneManager_Binding.Register(app);
            UnityEngine_AsyncOperation_Binding.Register(app);
            UnityEngine_AssetBundleRequest_Binding.Register(app);
            System_Threading_Tasks_Task_1_AssetBundleManifest_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_AssetBundleManifest_Binding.Register(app);
            System_TimeSpan_Binding.Register(app);
            System_Threading_Tasks_Task_1_AssetBundle_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_AssetBundle_Binding.Register(app);
            IEnumeratorAwaitExtensions_Binding.Register(app);
            IEnumeratorAwaitExtensions_Binding_SimpleCoroutineAwaiter_1_Object_Binding.Register(app);
            UniRx_AsyncSubject_1_Int32_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            IEnumeratorAwaitExtensions_Binding_SimpleCoroutineAwaiter_1_AsyncOperation_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_Runtime_CompilerServices_AsyncTaskMethodBuilder_1_Object_Array_Binding.Register(app);
            UniRx_Subject_1_Unit_Binding.Register(app);
            System_Threading_Tasks_Task_1_Object_Array_Binding.Register(app);
            System_Runtime_CompilerServices_TaskAwaiter_1_Object_Array_Binding.Register(app);
            UniRx_AsyncSubject_1_Unit_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_String_Binding.Register(app);
            System_Reflection_MethodInfo_Binding.Register(app);
            System_Reflection_MethodBase_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            UnityEngine_UI_Selectable_Binding.Register(app);
            UnityEngine_UI_Slider_Binding.Register(app);
            UnityEngine_Animation_Binding.Register(app);
            UnityEngine_Animator_Binding.Register(app);
            System_Reflection_ParameterInfo_Binding.Register(app);
            System_Func_2_ILTypeInstance_Array_ILTypeInstance_Binding.Register(app);
            UnityEngine_SceneManagement_Scene_Binding.Register(app);
            UniRx_UnityUIComponentExtensions_Binding.Register(app);
            UnityEditor_AssetDatabase_Binding.Register(app);
            UnityEngine_AssetBundleCreateRequest_Binding.Register(app);
            UniRx_Unit_Binding.Register(app);
            System_Action_1_Boolean_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_CoroutineAdapter_Binding_Adaptor_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Type_CoroutineAdapter_Binding_Adaptor_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_Object_Binding.Register(app);
            System_Char_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Object_Binding.Register(app);
            UnityEngine_Resources_Binding.Register(app);
            UnityEngine_TextAsset_Binding.Register(app);
            UniRx_AsyncSubject_1_String_Array_Binding.Register(app);
            Newtonsoft_Json_JsonConvert_Binding.Register(app);
            System_IObserver_1_String_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Tuple_2_String_String_Binding.Register(app);
            System_Tuple_2_String_String_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            System_Runtime_InteropServices_OSPlatform_Binding.Register(app);
            System_Runtime_InteropServices_RuntimeInformation_Binding.Register(app);
            System_Diagnostics_ProcessStartInfo_Binding.Register(app);
            System_Diagnostics_Process_Binding.Register(app);
            System_IO_TextReader_Binding.Register(app);
            System_Reflection_Assembly_Binding.Register(app);
            System_Reflection_PropertyInfo_Binding.Register(app);
            System_Reflection_FieldInfo_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Collections_Generic_IDictionary_2_String_ISubject_1_ILTypeInstance_Binding.Register(app);
            UniRx_BehaviorSubject_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_IDictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_NullReferenceException_Binding.Register(app);
            System_IObserver_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_IEnumerable_1_KeyValuePair_2_String_ISubject_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_IEnumerator_1_KeyValuePair_2_String_ISubject_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_ISubject_1_ILTypeInstance_Binding.Register(app);
            System_Text_StringBuilder_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ISubject_1_ILTypeInstance_Binding.Register(app);
            System_IO_Directory_Binding.Register(app);
            System_Runtime_Serialization_Formatters_Binary_BinaryFormatter_Binding.Register(app);
            System_IO_File_Binding.Register(app);
            System_IO_Stream_Binding.Register(app);
            System_MissingMethodException_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Delegate_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding.Register(app);
            System_Collections_Generic_List_1_Type_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_List_1_MonoBehaviourAdapter_Binding_Adaptor_Binding.Register(app);
            System_Collections_Generic_List_1_MonoBehaviourAdapter_Binding_Adaptor_Binding_Enumerator_Binding.Register(app);
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
