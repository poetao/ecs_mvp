using System;
using System.Reflection;

namespace Framework.Core
{
    public static class Reflection
    {
        public static bool useAsmDef { get; set; } = true;

	    public static Type GetRuntimeType(string path, Path.ASSEMBLY_TYPE assemblyType = Path.ASSEMBLY_TYPE.GAME)
        {
            if (!useAsmDef || assemblyType == Path.ASSEMBLY_TYPE.FRAMEWORK)
            {
                return Type.GetType(path);
            }

            var assemblyName = Path.instance.GetAssemblyPath(assemblyType);
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                Log.Reflection.W($"{assemblyName}.dll do not loaded");
                return null;
            }

	        return assembly.GetType(path);
	    }

        public static MethodInfo GetMethod<T>(string methodName, BindingFlags flag)
        {
            if (methodName == null) return null;
            return typeof(T).GetMethod(methodName, flag);
        }

        public static MethodInfo GetMethod(object instance, string methodName, BindingFlags flag) 
        {
            if (methodName == null) return null;
            return instance.GetType().GetMethod(methodName, flag);
        }

        public static MethodInfo GetMethod<T>(string methodName)
        {
            var flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            return GetMethod<T>(methodName, flag);
        }

        public static MethodInfo GetMethod(object instance, string methodName)
        {
            var flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            return GetMethod(instance, methodName, flag);
        }

        public static bool CallMethod(object instance, string methodName, params object[] args)
        {
            MethodInfo info = GetMethod(instance, methodName);
            if (info == null) return false;

            try
            {
                info.Invoke(instance, args);
            }
            catch (Exception e)
            {
                Log.Reflection.E($"Fail CallMethod for methodName {instance.GetType()} : {methodName}, {e.Message}");
                return false;
            }
            return true;
        }

        public static object CallMethodForRet(object instance, string methodName, params object[] args)
        {
            MethodInfo info = GetMethod(instance, methodName);
            if (info == null) return null;

            try
            {
                object ret = info.Invoke(instance, args);
                return ret;
            }
            catch (Exception e)
            {
                Log.Reflection.E($"Fail CallMethodForRet for methodName {instance.GetType()} : {methodName}, {e.Message}");
            }
            return null;
        }

        public static bool GetPropertyValue<T>(object instance, string name, ref T ret)
        {
            if (name == null) return false;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            PropertyInfo info = type.GetProperty(name, flag);
            if (info == null) return false;
            try
            {
                ret = (T)info.GetValue(instance, null);
            }
            catch (Exception e)
            {
                Log.Reflection.E($"Fail GetPropertyBase for methodName {instance.GetType()} : {name}, {e.Message}");
                return false;
            }
            return true;
        }

        public static T GetPropertyRef<T>(object instance, string name) where T : class
        {
            if (name == null) return null;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            PropertyInfo info = type.GetProperty(name, flag);
            if (info == null) return null;
            return info.GetValue(instance, null) as T;
        }

        public static bool SetProperty(object instance, string name, object obj)
        {
            if (name == null) return false;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            PropertyInfo info = type.GetProperty(name, flag);
            if (info == null) return false;
            try
            {
                info.SetValue(instance, obj, null);
            }
            catch (Exception e)
            {
                Log.Reflection.E($"Fail SetProperty for name  {instance.GetType()} : {name}, {e.Message}");
                return false;
            }
            return true;
        }

        public static FieldInfo[] GetFields(Type type)
        {
            var flag = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
            return type.GetFields(flag);
        }

        public static bool GetFieldValue<T>(object instance, string name, ref T ret)
        {
            if (name == null) return false;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo info = type.GetField(name, flag);
            if (info == null) return false;
            try
            {
                ret = (T)info.GetValue(instance);
            }
            catch (Exception e)
            {
                Log.Reflection.E($"Fail GetFieldValue for name  {instance.GetType()} : {name}, {e.Message}");
                return false;
            }
            return true;
        }

        public static T GetFieldRef<T>(object instance, string name) where T : class
        {
            if (name == null) return null;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo info = type.GetField(name, flag);
            if (info == null) return null;
            return info.GetValue(instance) as T;
        }

        public static T GetStaticFieldRef<T, TType>(string name)
            where T : class
            where TType : new()
        {
            var type = typeof(TType);
            BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo info = type.GetField(name, flag);
            if (info == null) return null;
            return info.GetValue(new TType()) as T;
        }

        public static bool SetField(object instance, string name, object obj)
        {
            if (name == null) return false;
            Type type = instance.GetType();
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo info = type.GetField(name, flag);
            if (info == null) return false;
            try
            {
                info.SetValue(instance, obj);
            }
            catch (Exception e)
            {
                Log.Reflection.E(e.Message);
                return false;
            }
            return true;
        }

        public static TFunc CreateDelegate<TFunc, TInstanceType>(TInstanceType instance, MethodInfo method) where TFunc : class
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(TFunc), instance) as TFunc; 
	    }

        public static Action CreateAction<TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Action), instance) as Action;
	    }

        public static Action<T> CreateAction<T, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Action<T>), instance) as Action<T>;
	    }

        public static Action<T1, T2> CreateAction<T1, T2, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Action<T1, T2>), instance) as Action<T1, T2>;
	    }

        public static Action<T1, T2, T3> CreateAction<T1, T2, T3, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Action<T1, T2, T3>), instance) as Action<T1, T2, T3>;
	    }

        public static Func<TReturn> CreateFunc<TReturn, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Func<TReturn>), instance) as Func<TReturn>;
	    }

        public static Func<T, TReturn> CreateFunc<T, TReturn, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Func<T, TReturn>), instance) as Func<T, TReturn>;
	    }

        public static Func<T1, T2, TReturn> CreateFunc<T1, T2, TReturn, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Func<T1, T2, TReturn>), instance) as Func<T1, T2, TReturn>;
	    }

        public static Func<T1, T2, T3, TReturn> CreateFunc<T1, T2, T3, TReturn, TInstanceType>(TInstanceType instance, MethodInfo method)
	    {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (method == null) throw new ArgumentNullException(nameof(method));

            return method.CreateDelegate(typeof(Func<T1, T2, T3, TReturn>), instance) as Func<T1, T2, T3, TReturn>;
	    }

        public static T CreateInstance<T>(string path) where T : class
        {
            return CreateInstance<T>(path, Path.ASSEMBLY_TYPE.GAME);
        }

        public static T CreateInstance<T>(string path, Path.ASSEMBLY_TYPE assembly, params object[] args) where T : class
        {
            var type = GetRuntimeType(path, assembly);
            if (type == null)
            {
                Log.Reflection.E($"Class do not defined: {path}");
                return default(T);
            }

            return CreateInstance<T>(type, args);
        }

        public static T CreateInstance<T>(Type type, params object[] args) where T : class
        {
            var instance = CreateInstance(type, args);
            if (instance == null) return default(T);

            return instance as T;
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            var instance = Activator.CreateInstance(type, args);
            if (instance == null)
            {
                Log.Reflection.E($"{type} is not defined");
                return null;
            }

            return instance;
        }
    }
}
