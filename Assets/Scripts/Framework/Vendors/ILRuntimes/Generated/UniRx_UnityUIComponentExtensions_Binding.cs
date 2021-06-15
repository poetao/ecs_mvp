using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UniRx_UnityUIComponentExtensions_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(UniRx.UnityUIComponentExtensions);
            args = new Type[]{typeof(UnityEngine.UI.InputField)};
            method = type.GetMethod("OnValueChangedAsObservable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, OnValueChangedAsObservable_0);
            args = new Type[]{typeof(UnityEngine.UI.Button)};
            method = type.GetMethod("OnClickAsObservable", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, OnClickAsObservable_1);


        }


        static StackObject* OnValueChangedAsObservable_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.UI.InputField @inputField = (UnityEngine.UI.InputField)typeof(UnityEngine.UI.InputField).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = UniRx.UnityUIComponentExtensions.OnValueChangedAsObservable(@inputField);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* OnClickAsObservable_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.UI.Button @button = (UnityEngine.UI.Button)typeof(UnityEngine.UI.Button).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = UniRx.UnityUIComponentExtensions.OnClickAsObservable(@button);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
