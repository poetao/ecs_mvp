using System;

namespace MVP.Framework.Core.States
{
    [Serializable]
    public class Any : IEquatable<Any>, IAstarPooledObject
    {
        public static Any Empty = Any.Create(new object());

        private Type _valueType;
        private object _refValue;
        private int _intValue;
        private uint _uintValue;
        private long _longValue;
        private ulong _ulongValue;
        private bool _boolValue;
        private float _floatValue;
        private double _doubleValue;
        private string _stringValue;

        public static Any CreateByType(Type type)
        {
            var any = ObjectPool<Any>.Claim();
            any._valueType = type;
            return any;
        }

        public static Any Create<T>()
        {
            return Create<T>(default(T));
        }
        
        public static Any Create<T>(T value)
        {
            var any = ObjectPool<Any>.Claim();
            any._valueType = typeof(T);
            switch (value)
            {
                case int v: any._intValue = v; break;
                case uint v: any._uintValue = v; break;
                case long v: any._longValue = v; break;
                case ulong v: any._ulongValue = v; break;
                case bool v: any._boolValue = v; break;
                case float v: any._floatValue = v; break;
                case double v: any._doubleValue = v; break;
                case string v: any._stringValue = v; break;
                default:
                    if (!any._valueType.IsValueType)
                    {
                        any._refValue = value;
                    }
                    else
                    {
                        Log.State.Exception(new Exception($"do not implement type {any._valueType} in Any class!"));
                    }
                    break;
            }

            return any;
        }

        public static void Release(Any refAny)
        {
            if (refAny == null) return;
            if (refAny == Any.Empty) return;

            ObjectPool<Any>.Release(ref refAny);
        }

        public void OnEnterPool()
        {
            this._valueType = null;
            this._refValue = null;
            this._intValue = 0;
            this._uintValue = 0;
            this._boolValue = false;
            this._longValue = 0;
            this._ulongValue = 0;
            this._floatValue = 0;
            this._doubleValue = 0;
            this._stringValue = "";
        }

        public string StringValue()
        {
            if (!IsEmpty() && !Is<string>())
            {
                Log.State.Exception(new Exception($"this is not a string value"));
            }

            return _stringValue;
        }

        public bool BoolValue()
        {
            if (!IsEmpty() && !Is<bool>())
            {
                Log.State.Exception(new Exception($"this is not a bool value"));
            }

            return _boolValue;
        }

        public int IntValue()
        {
            if (!IsEmpty() && !Is<int>())
            {
                Log.State.Exception(new Exception($"this is not a int value"));
            }

            return _intValue;
        }

        public uint UIntValue()
        {
            if (!IsEmpty() && !Is<uint>())
            {
                Log.State.Exception(new Exception($"this is not a uint value"));
            }

            return _uintValue;
        }

        public long LongValue()
        {
            if (!IsEmpty() && !Is<long>())
            {
                Log.State.Exception(new Exception($"this is not a long value"));
            }

            return _longValue;
        }

        public ulong ULongValue()
        {
            if (!IsEmpty() && !Is<ulong>())
            {
                Log.State.Exception(new Exception($"this is not a ulong value"));
            }

            return _ulongValue;
        }

        public float FloatValue()
        {
            if (!IsEmpty() && !Is<float>())
            {
                Log.State.Exception(new Exception($"this is not a float value"));
            }

            return _floatValue;
        }

        public double DoubleValue()
        {
            if (!IsEmpty() && !Is<double>())
            {
                Log.State.Exception(new Exception($"this is not a double value"));
            }

            return _doubleValue;
        }

        public T RefValue<T>() where T : class
        {
            if (!IsEmpty() && !Is<T>())
            {
                Log.State.Exception(new Exception($"this is not a {typeof(T)} value"));
            }

            return _refValue as T;
        }

        public bool Is<T>()
        {
            return typeof(T).IsAssignableFrom(this._valueType);
        }

        public bool IsEmpty()
        {
            return this.Equals(Any.Empty);
        }

        public bool IsValueType()
        {
            return _valueType.IsValueType;
        }

        public string ValueString()
        {
            if (_valueType == typeof(string))
            {
                return _stringValue;
            }

            if (_valueType == typeof(bool))
            {
                return _boolValue.ToString();
            }

            if (_valueType == typeof(int))
            {
                return _intValue.ToString();
            }

            if (_valueType == typeof(uint))
            {
                return _uintValue.ToString();
            }

            if (_valueType == typeof(long))
            {
                return _longValue.ToString();
            }

            if (_valueType == typeof(ulong))
            {
                return _ulongValue.ToString();
            }

            if (_valueType == typeof(float))
            {
                return _floatValue.ToString();
            }

            if (_valueType == typeof(double))
            {
                return _doubleValue.ToString();
            }

            return _refValue.ToString();
        }

        public override string ToString()
        {
            return $"Any(Type:{_valueType}, Object:{_refValue})";
        }

        public bool Equals(Any other)
        {
            if (_valueType != other._valueType) return false;

            if (_valueType == null) return true;

            if (_valueType == typeof(string))
            {
                return _stringValue.Equals(other._stringValue);
            }

            if (_valueType == typeof(bool))
            {
                return _boolValue.Equals(other._boolValue);
            }

            if (_valueType == typeof(int))
            {
                return _intValue.Equals(other._boolValue);
            }

            if (_valueType == typeof(uint))
            {
                return _uintValue.Equals(other._uintValue);
            }

            if (_valueType == typeof(long))
            {
                return _longValue.Equals(other._longValue);
            }

            if (_valueType == typeof(ulong))
            {
                return _ulongValue.Equals(other._ulongValue);
            }

            if (_valueType == typeof(float))
            {
                return _floatValue.Equals(other._floatValue);
            }

            if (_valueType == typeof(double))
            {
                return _doubleValue.Equals(other._doubleValue);
            }

            return _refValue.Equals(other._refValue);
        }
    }
}