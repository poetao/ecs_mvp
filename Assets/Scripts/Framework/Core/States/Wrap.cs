using System;
using System.Collections.Generic;

namespace Framework.Core.States
{
    public class WrapPool<T>
    {
        private static Queue<Wrap<T>> q;

        public static Wrap<T> Rent()
        {
            if (q == null) q = new Queue<Wrap<T>>();

            var instance = (q.Count > 0)
                ? q.Dequeue()
                : CreateInstance();

            return instance;
        }

        public static void Return(Wrap<T> instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            if (q == null) q = new Queue<Wrap<T>>();

            if ((q.Count + 1) == MaxPoolCount)
            {
                throw new InvalidOperationException("Reached Max PoolSize");
            }

            q.Enqueue(instance);
        }

        protected static Wrap<T> CreateInstance()
        {
            return new Wrap<T>();
        }

        protected static int MaxPoolCount
        {
            get
            {
                return int.MaxValue;
            }
        }
    }

    [Serializable]
    public abstract class WrapBase
    {
        public static WrapBase Empty;

        public virtual T ValueOf<T>()
        {
            throw new NotImplementedException();
        }

        public virtual T RefOf<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public virtual bool Is<T>()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValueType()
        {
            throw new NotImplementedException();
        }

        public virtual string ValueToString()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Wrap<T> : WrapBase, IDisposable
    {
        public T Value;

        public static Wrap<T> Create(T value)
        {
            var wrap = WrapPool<T>.Rent();
            wrap.Value = value;
            return wrap;
        }

        public static void Destroy(Wrap<T> wrap)
        {
            wrap.Dispose();
        }

        public void Dispose()
        {
            WrapPool<T>.Return(this);
        }

        public override T1 ValueOf<T1>()
        {
            if (!Is<T1>())
            {
#if UNITY_EDITOR
                throw new InvalidCastException();
#else
                return default(T1);
#endif
            }

            var wrap = this as Wrap<T1>;
            if (null == wrap)
            {
#if UNITY_EDITOR
                throw new InvalidCastException();
#else
                return default(T1);
#endif
            }

            return wrap.Value;
        }

        public override T1 RefOf<T1>()
        {
            if (!Is<T1>())
            {
#if UNITY_EDITOR
                throw new InvalidCastException();
#else
                return default(T1);
#endif
            }

            return Value as T1;
        }

        public override bool Is<T1>()
        {
            return Value is T1;
        }

        public override bool IsValueType()
        {
            return typeof(T).IsValueType;
        }

        public override string ValueToString()
        {
            return Value.ToString();
        }
    }
}