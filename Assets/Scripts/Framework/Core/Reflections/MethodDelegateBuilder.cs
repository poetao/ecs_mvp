using System;
using System.Collections.Generic;
using Framework.Core.States;

namespace Framework.Core.Reflections
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WrapDelegateAttribute : Attribute { }

    public static class MethodDelegateBuilder
    {
        private static Dictionary<int, Delegate> actions = new Dictionary<int, Delegate>();

        public static Func<WrapBase[], WrapBase> GetDelegate<TInstance>(TInstance instance, string methodName)
        {
            var method = Reflection.GetMethod(instance, methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.TryGetValue(key, out var proxy))
            {
                return (x) => proxy.DynamicInvoke(instance, x) as WrapBase;
            }

            throw new Exception(GetRegisteTipStr(instance.GetType(), method));
        }

        public static Func<TInstance, WrapBase[], WrapBase> GetDelegate<TInstance>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.TryGetValue(key, out var proxy))
            {
                return proxy as Func<TInstance, WrapBase[], WrapBase>;
            }

            throw new Exception(GetRegisteTipStr(typeof(TInstance), method));
        }

        public static void RegisteAction<TInstance>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var action = method.CreateDelegate(typeof(Action<TInstance>)) as Action<TInstance>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                action?.Invoke(o);
                return WrapBase.Empty;
            };
            actions.Add(key, proxy);
        }

        public static void RegisteAction<TInstance, T0>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var action = method.CreateDelegate(typeof(Action<TInstance, T0>)) as Action<TInstance, T0>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (x[0] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                action?.Invoke(o, x[0].ValueOf<T0>());
                return WrapBase.Empty;
            };
            actions.Add(key, proxy);
        }

        public static void RegisteAction<TInstance, T0, T1>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var action = method.CreateDelegate(typeof(Action<TInstance, T0, T1>)) as Action<TInstance, T0, T1>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (x[0] == WrapBase.Empty || x[1] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                action?.Invoke(o, x[0].ValueOf<T0>(), x[1].ValueOf<T1>());
                return WrapBase.Empty;
            };
            actions.Add(key, proxy);
        }

        public static void RegisteAction<TInstance, T0, T1, T2>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var action = method.CreateDelegate(typeof(Action<TInstance, T0, T1>)) as Action<TInstance, T0, T1, T2>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (x[0] == WrapBase.Empty || x[1] == WrapBase.Empty || x[2] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                action?.Invoke(o, x[0].ValueOf<T0>(), x[1].ValueOf<T1>(), x[2].ValueOf<T2>());
                return WrapBase.Empty;
            };
            actions.Add(key, proxy);
        }

        public static void RegisteFunc<TInstance, TReturn>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var func = method.CreateDelegate(typeof(Func<TInstance, TReturn>)) as Func<TInstance, TReturn>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                return Wrap<TReturn>.Create(func != null ? func.Invoke(o) : default(TReturn));
            };
            actions.Add(key, proxy);
        }

        public static void RegisteFunc<TInstance, T0, TReturn>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var func = method.CreateDelegate(typeof(Func<TInstance, T0, TReturn>)) as Func<TInstance, T0, TReturn>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (func == null || x[0] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                return Wrap<TReturn>.Create(func(o, x[0].ValueOf<T0>()));
            };
            actions.Add(key, proxy);
        }

        public static void RegisteFunc<TInstance, T0, T1, TReturn>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var func = method.CreateDelegate(typeof(Func<TInstance, T0, T1, TReturn>)) as Func<TInstance, T0, T1, TReturn>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (func == null || x[0] == WrapBase.Empty || x[1] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                return Wrap<TReturn>.Create(func(o, x[0].ValueOf<T0>(), x[1].ValueOf<T1>()));
            };
            actions.Add(key, proxy);
        }

        public static void RegisteFunc<TInstance, T0, T1, T2, TReturn>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var func = method.CreateDelegate(typeof(Func<TInstance, T0, T1, T2, TReturn>)) as Func<TInstance, T0, T1, T2, TReturn>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (func == null || x[0] == WrapBase.Empty || x[1] == WrapBase.Empty || x[2] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                return Wrap<TReturn>.Create(func(o, x[0].ValueOf<T0>(), x[1].ValueOf<T1>(), x[2].ValueOf<T2>()));
            };
            actions.Add(key, proxy);
        }

        public static void RegisteFunc<TInstance, T0, T1, T2, T3, TReturn>(string methodName)
        {
            var method = Reflection.GetMethod<TInstance>(methodName);
            if (method == null) throw new MissingMethodException(methodName);

            var key = method.GetHashCode();
            if (actions.ContainsKey(key))
            {
                Log.Reflection.W("Duplicate RegisteAction for {0} : {1}", typeof(TInstance), method);
                return;
            }

            var func = method.CreateDelegate(typeof(Func<TInstance, T0, T1, T2, T3, TReturn>)) as Func<TInstance, T0, T1, T2, T3, TReturn>;
            Func<TInstance, WrapBase[], WrapBase> proxy = (o, x) =>
            {
                if (func == null || x[0] == WrapBase.Empty || x[1] == WrapBase.Empty || x[2] == WrapBase.Empty || x[3] == WrapBase.Empty)
                {
                    return WrapBase.Empty;
                }

                return Wrap<TReturn>.Create(func(o, x[0].ValueOf<T0>(), x[1].ValueOf<T1>(), x[2].ValueOf<T2>(), x[3].ValueOf<T3>()));
            };
            actions.Add(key, proxy);
        }

        private static string GetRegisteTipStr(Type instanceType, System.Reflection.MethodInfo methodInfo)
        {
            var parameterTypes = "";
            var parameters = methodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; ++i)
            {
                parameterTypes = $"{parameterTypes}, {parameters[i].ParameterType}";
            }

            if (methodInfo.ReturnType == typeof(void))
            {
                return $"Please Register Action:\nMethodDelegateBuilder.RegisteAction<{instanceType}{parameterTypes}>(\"{methodInfo.Name}\");\n";
            }

            return $"Please Register Action:\nMethodDelegateBuilder.RegisteFunc<{instanceType}{parameterTypes}, {methodInfo.ReturnType}>(\"{methodInfo.Name}\");\n";
        }
    }
}
