using System.Collections.Generic;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Runtime.Enviorment;
using Framework.Core;

namespace Framework.Vendors.ILRuntimes.Redirectors
{
    public class ReActivator
    {
        unsafe static StackObject* CreateInstance(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            //获取泛型参数<T>的实际类型
            IType[] genericArguments = __method.GenericArguments;
            if (genericArguments != null && genericArguments.Length == 1)
            {
                var t = genericArguments[0];
                if (t is ILType)//如果T是热更DLL里的类型
                {
                    //通过ILRuntime的接口来创建实例
                    return ILIntepreter.PushObject(__esp, __mStack, ((ILType)t).Instantiate());
                }
                else
                    return ILIntepreter.PushObject(__esp, __mStack, System.Activator.CreateInstance(t.TypeForCLR));//通过系统反射接口创建实例
            }
            else
            {
                Log.Startup.D("================== exception for create Error");
                throw new System.EntryPointNotFoundException();
            }
        }

        unsafe public static void SetupCLRRedirection(AppDomain appdomain)
        {
            foreach (var i in typeof(System.Activator).GetMethods())
            {
                //找到名字为CreateInstance，并且是泛型方法的方法定义
                if (i.Name == "CreateInstance" && i.IsGenericMethodDefinition)
                // if (i.Name == "CreateInstance" && i.GetGenericArguments().Length == 1)
                {
                    appdomain.RegisterCLRMethodRedirection(i, CreateInstance);
                }
            }
        }
    }
}