using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MVP.Framework.Core.States;

namespace MVP.Framework.Core.Reflections
{
    [Serializable]
    public class ProxyParameter
    {
        private static Dictionary<Type, string> DELEGATE_MAP = new Dictionary<Type, string> {
            { typeof(bool),        "B" },
            { typeof(int),         "I" },
            { typeof(uint),        "X" },
            { typeof(long),        "L" },
            { typeof(ulong),       "A" },
            { typeof(float),       "F" },
            { typeof(double),      "D" },
            { typeof(string),      "S" },
            { typeof(GameObject),  "O" },
        };

        public int intValue;
        public uint uintValue;
        public long longValue;
        public ulong ulongValue;
        public bool boolValue;
        public string stringValue;
        public float floatValue;
        public string useFlag;
        public GameObject gameObject;

        public ProxyParameter(bool value)
        {
            this.boolValue = value;
            useFlag = "B";
        }

        public ProxyParameter(int value)
        {
            this.intValue = value;
            useFlag = "I";
        }

        public ProxyParameter(uint value)
        {
            this.uintValue = value;
            useFlag = "X";
        }

        public ProxyParameter(long value)
        {
            this.longValue = value;
            useFlag = "L";
        }

        public ProxyParameter(ulong value)
        {
            this.ulongValue = value;
            useFlag = "A";
        }

        public ProxyParameter(float value)
        {
            this.floatValue = value;
            useFlag = "F";
        }

        public ProxyParameter(string value)
        {
            this.stringValue = value;
            useFlag = "S";
        }

        public ProxyParameter(GameObject gameObject)
        {
            this.gameObject = gameObject;
            useFlag = "O";
        }

        public ProxyParameter(Type type)
        {
            this.useFlag = CastFlag(type);
        }

        public object GetObjectValue()
        {
            switch (useFlag)
            {
                case "I": return intValue;
                case "X": return uintValue;
                case "L": return longValue;
                case "A": return ulongValue;
                case "S": return stringValue;
                case "B": return boolValue;
                case "F": return floatValue;
                case "O": return gameObject;
            }

            return null;
        }

        public Any GetAnyValue()
        {
            switch (useFlag)
            {
                case "I": return Any.Create(intValue);
                case "X": return Any.Create(uintValue);
                case "L": return Any.Create(longValue);
                case "A": return Any.Create(ulongValue);
                case "S": return Any.Create(stringValue);
                case "B": return Any.Create(boolValue);
                case "F": return Any.Create(floatValue);
                case "O": return Any.Create(gameObject);
            }

            return Any.Empty;
        }

        public static string CastFlag(Type type)
        {
            if (!DELEGATE_MAP.ContainsKey(type)) return "";
            return DELEGATE_MAP[type];
        }
    }

    public static class MethodDelegateBuilder
    {
        public static Func<Any[], Any> GetMethodDelegate<T>(MethodInfo method, T presenter)
        {
            Func<Any[], Any> func = null;
            if (method.ReturnType == typeof(void))
            {
                var action = GetMethodActionDelegate(method, presenter);
                func = x => { action(x); return Any.Empty; };
            }
            else if (method.ReturnType == typeof(string))
            {
                func = x =>
                {
                    var action = GetMethodFuncDelegate<T, string>(method, presenter);
                    return Any.Create(action(x));
                };
            }
            else if (method.ReturnType == typeof(int))
            {
                func = x =>
                {
                    var action = GetMethodFuncDelegate<T, int>(method, presenter);
                    return Any.Create(action(x));
                };
            }
            else if (method.ReturnType == typeof(float))
            {
                func = x =>
                {
                    var action = GetMethodFuncDelegate<T, float>(method, presenter);
                    return Any.Create(action(x));
                };
            }
            else if (method.ReturnType == typeof(bool))
            {
                func = x =>
                {
                    var action = GetMethodFuncDelegate<T, bool>(method, presenter);
                    return Any.Create(action(x));
                };
            }

            return func;
        }

        public static Func<Any[], TReturn> GetMethodFuncDelegate<T, TReturn>(MethodInfo method, T presenter)
        {
	        if (method == null) return null;

            var parametersTagName = GetDelegateTagNameByParamenterTypes(method);
            Func<Any[], TReturn> proxyAction = null;
            switch (parametersTagName)
            {
                case "":
                {
                    var action = Core.Reflection.CreateFunc<TReturn, T>(presenter, method);
                    proxyAction += (x => action()); break;
                }
                case "I":
                {
                    var action = Core.Reflection.CreateFunc<int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue())); break;
                }
                case "X":
                {
                    var action = Core.Reflection.CreateFunc<uint, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].UIntValue())); break;
                }
                case "L":
                {
                    var action = Core.Reflection.CreateFunc<long, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue())); break;
                }
                case "A":
                {
                    var action = Core.Reflection.CreateFunc<ulong, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].ULongValue())); break;
                }
                case "F":
                {
                    var action = Core.Reflection.CreateFunc<float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue())); break;
                }
                case "D":
                {
                    var action = Core.Reflection.CreateFunc<double, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue())); break;
                }
                case "B":
                {
                    var action = Core.Reflection.CreateFunc<bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue())); break;
                }
                case "S":
                {
                    var action = Core.Reflection.CreateFunc<string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue())); break;
                }
                case "IB":
                {
                    var action = Core.Reflection.CreateFunc<int, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].BoolValue())); break;
                }
                case "XB":
                {
                    var action = Core.Reflection.CreateFunc<uint, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].UIntValue(), x[1].BoolValue())); break;
                }
                case "LB":
                {
                    var action = Core.Reflection.CreateFunc<long, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].BoolValue())); break;
                }
                case "AB":
                {
                    var action = Core.Reflection.CreateFunc<ulong, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].ULongValue(), x[1].BoolValue())); break;
                }
                case "FB":
                {
                    var action = Core.Reflection.CreateFunc<float, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].BoolValue())); break;
                }
                case "BB":
                {
                    var action = Core.Reflection.CreateFunc<bool, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].BoolValue())); break;
                }
                case "SB":
                {
                    var action = Core.Reflection.CreateFunc<string, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].BoolValue())); break;
                }
                case "IF":
                {
                    var action = Core.Reflection.CreateFunc<int, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].FloatValue())); break;
                }
                case "XF":
                {
                    var action = Core.Reflection.CreateFunc<uint, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].UIntValue(), x[1].FloatValue())); break;
                }
                case "LF":
                {
                    var action = Core.Reflection.CreateFunc<long, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].FloatValue())); break;
                }
                case "AF":
                {
                    var action = Core.Reflection.CreateFunc<ulong, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].ULongValue(), x[1].FloatValue())); break;
                }
                case "FF":
                {
                    var action = Core.Reflection.CreateFunc<float, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].FloatValue())); break;
                }
                case "BF":
                {
                    var action = Core.Reflection.CreateFunc<bool, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].FloatValue())); break;
                }
                case "SF":
                {
                    var action = Core.Reflection.CreateFunc<string, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].FloatValue())); break;
                }
                case "II":
                {
                    var action = Core.Reflection.CreateFunc<int, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].IntValue())); break;
                }
                case "FI":
                {
                    var action = Core.Reflection.CreateFunc<float, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].IntValue())); break;
                }
                case "BI":
                {
                    var action = Core.Reflection.CreateFunc<bool, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].IntValue())); break;
                }
                case "SI":
                {
                    var action = Core.Reflection.CreateFunc<string, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].IntValue())); break;
                }
                case "IS":
                {
                    var action = Core.Reflection.CreateFunc<int, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue())); break;
                }
                case "FS":
                {
                    var action = Core.Reflection.CreateFunc<float, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue())); break;
                }
                case "BS":
                {
                    var action = Core.Reflection.CreateFunc<bool, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue())); break;
                }
                case "SS":
                {
                    var action = Core.Reflection.CreateFunc<string, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue())); break;
                }
                case "ISI":
                {
                    var action = Core.Reflection.CreateFunc<int, string, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue(), x[2].IntValue())); break;
                }
                case "FSI":
                {
                    var action = Core.Reflection.CreateFunc<float, string, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue(), x[2].IntValue())); break;
                }
                case "BSI":
                {
                    var action = Core.Reflection.CreateFunc<bool, string, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].IntValue())); break;
                }
                case "SSI":
                {
                    var action = Core.Reflection.CreateFunc<string, string, int, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].IntValue())); break;
                }
                case "ISF":
                {
                    var action = Core.Reflection.CreateFunc<int, string, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue(), x[2].FloatValue())); break;
                }
                case "FSF":
                {
                    var action = Core.Reflection.CreateFunc<float, string, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue(), x[2].FloatValue())); break;
                }
                case "BSF":
                {
                    var action = Core.Reflection.CreateFunc<bool, string, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].FloatValue())); break;
                }
                case "SSF":
                {
                    var action = Core.Reflection.CreateFunc<string, string, float, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].FloatValue())); break;
                }
                case "ISB":
                {
                    var action = Core.Reflection.CreateFunc<int, string, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "FSB":
                {
                    var action = Core.Reflection.CreateFunc<float, string, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "BSB":
                {
                    var action = Core.Reflection.CreateFunc<bool, string, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "SSB":
                {
                    var action = Core.Reflection.CreateFunc<string, string, bool, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "ISS":
                {
                    var action = Core.Reflection.CreateFunc<int, string, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "FSS":
                {
                    var action = Core.Reflection.CreateFunc<float, string, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "BSS":
                {
                    var action = Core.Reflection.CreateFunc<bool, string, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "SSS":
                {
                    var action = Core.Reflection.CreateFunc<string, string, string, TReturn, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                default:
                {
                    Log.Reflection.Exception(new Exception($"UNDEFIDED FUNC BUILD FOR TYPE: {parametersTagName}"));
                    break;
                }
            }

            return proxyAction;
        }

        public static Action<Any[]> GetMethodActionDelegate<T>(MethodInfo method, T presenter)
        {
	        if (method == null) return null;

            var parametersTagName = GetDelegateTagNameByParamenterTypes(method);
            Action<Any[]> proxyAction = null;
            switch (parametersTagName)
            {
                case "":
                {
                    var action = Core.Reflection.CreateAction(presenter, method);
                    proxyAction += (x => action()); break;
                }
                case "I":
                {
                    var action = Core.Reflection.CreateAction<int, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue())); break;
                }
                case "L":
                {
                    var action = Core.Reflection.CreateAction<long, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue())); break;
                }
                case "F":
                {
                    var action = Core.Reflection.CreateAction<float, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue())); break;
                }
                case "D":
                {
                    var action = Core.Reflection.CreateAction<double, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue())); break;
                }
                case "B":
                {
                    var action = Core.Reflection.CreateAction<bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue())); break;
                }
                case "S":
                {
                    var action = Core.Reflection.CreateAction<string, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue())); break;
                }
                case "IB":
                {
                    var action = Core.Reflection.CreateAction<int, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].BoolValue())); break;
                }
                case "LB":
                {
                    var action = Core.Reflection.CreateAction<long, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].BoolValue())); break;
                }
                case "FB":
                {
                    var action = Core.Reflection.CreateAction<float, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].BoolValue())); break;
                }
                case "DB":
                {
                    var action = Core.Reflection.CreateAction<double, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].BoolValue())); break;
                }
                case "BB":
                {
                    var action = Core.Reflection.CreateAction<bool, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].BoolValue())); break;
                }
                case "SB":
                {
                    var action = Core.Reflection.CreateAction<string, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].BoolValue())); break;
                }
                case "IF":
                {
                    var action = Core.Reflection.CreateAction<int, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].FloatValue())); break;
                }
                case "LF":
                {
                    var action = Core.Reflection.CreateAction<long, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].FloatValue())); break;
                }
                case "FF":
                {
                    var action = Core.Reflection.CreateAction<float, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].FloatValue())); break;
                }
                case "DF":
                {
                    var action = Core.Reflection.CreateAction<double, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].FloatValue())); break;
                }
                case "BF":
                {
                    var action = Core.Reflection.CreateAction<bool, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].FloatValue())); break;
                }
                case "SF":
                {
                    var action = Core.Reflection.CreateAction<string, float, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].FloatValue())); break;
                }
                case "ID":
                {
                    var action = Core.Reflection.CreateAction<int, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].DoubleValue())); break;
                }
                case "LD":
                {
                    var action = Core.Reflection.CreateAction<long, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].DoubleValue())); break;
                }
                case "FD":
                {
                    var action = Core.Reflection.CreateAction<float, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].DoubleValue())); break;
                }
                case "DD":
                {
                    var action = Core.Reflection.CreateAction<double, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].DoubleValue())); break;
                }
                case "BD":
                {
                    var action = Core.Reflection.CreateAction<bool, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].DoubleValue())); break;
                }
                case "SD":
                {
                    var action = Core.Reflection.CreateAction<string, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].DoubleValue())); break;
                }
                case "II":
                {
                    var action = Core.Reflection.CreateAction<int, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].IntValue())); break;
                }
                case "LI":
                {
                    var action = Core.Reflection.CreateAction<long, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].IntValue())); break;
                }
                case "FI":
                {
                    var action = Core.Reflection.CreateAction<float, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].IntValue())); break;
                }
                case "DI":
                {
                    var action = Core.Reflection.CreateAction<double, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].IntValue())); break;
                }
                case "BI":
                {
                    var action = Core.Reflection.CreateAction<bool, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].IntValue())); break;
                }
                case "SI":
                {
                    var action = Core.Reflection.CreateAction<string, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].IntValue())); break;
                }
                case "IS":
                {
                    var action = Core.Reflection.CreateAction<int, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue())); break;
                }
                case "IL":
                {
                    var action = Core.Reflection.CreateAction<int, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].LongValue())); break;
                }
                case "LL":
                {
                    var action = Core.Reflection.CreateAction<long, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].LongValue())); break;
                }
                case "FL":
                {
                    var action = Core.Reflection.CreateAction<float, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].LongValue())); break;
                }
                case "DL":
                {
                    var action = Core.Reflection.CreateAction<double, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].LongValue())); break;
                }
                case "BL":
                {
                    var action = Core.Reflection.CreateAction<bool, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].LongValue())); break;
                }
                case "SL":
                {
                    var action = Core.Reflection.CreateAction<string, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].LongValue())); break;
                }
                case "LS":
                {
                    var action = Core.Reflection.CreateAction<long, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].StringValue())); break;
                }
                case "FS":
                {
                    var action = Core.Reflection.CreateAction<float, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].FloatValue(), x[1].StringValue())); break;
                }
                case "DS":
                {
                    var action = Core.Reflection.CreateAction<double, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].StringValue())); break;
                }
                case "BS":
                {
                    var action = Core.Reflection.CreateAction<bool, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue())); break;
                }
                case "SS":
                {
                    var action = Core.Reflection.CreateAction<string, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue())); break;
                }
                case "ISI":
                {
                    var action = Core.Reflection.CreateAction<int, string, int, T>(presenter, method);
                    proxyAction += (x => action(x[0].IntValue(), x[1].StringValue(), x[2].IntValue())); break;
                }
                case "FSI":
                case "DSI":
                case "FSL":
                case "DSL":
                {
                    var action = Core.Reflection.CreateAction<double, string, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].StringValue(), x[2].LongValue())); break;
                }
                case "BSI":
                case "BSL":
                {
                    var action = Core.Reflection.CreateAction<bool, string, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].LongValue())); break;
                }
                case "SSI":
                case "SSL":
                {
                    var action = Core.Reflection.CreateAction<string, string, long, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].LongValue())); break;
                }
                case "ISF":
                case "LSF":
                case "ISD":
                case "LSD":
                {
                    var action = Core.Reflection.CreateAction<long, string, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].StringValue(), x[2].DoubleValue())); break;
                }
                case "FSF":
                case "DSF":
                case "FSD":
                case "DSD":
                {
                    var action = Core.Reflection.CreateAction<double, string, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].StringValue(), x[2].DoubleValue())); break;
                }
                case "BSF":
                case "BSD":
                {
                    var action = Core.Reflection.CreateAction<bool, string, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].DoubleValue())); break;
                }
                case "SSF":
                case "SSD":
                {
                    var action = Core.Reflection.CreateAction<string, string, double, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].DoubleValue())); break;
                }
                case "ISB":
                case "LSB":
                {
                    var action = Core.Reflection.CreateAction<long, string, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "FSB":
                case "DSB":
                {
                    var action = Core.Reflection.CreateAction<double, string, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "BSB":
                {
                    var action = Core.Reflection.CreateAction<bool, string, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "SSB":
                {
                    var action = Core.Reflection.CreateAction<string, string, bool, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].BoolValue())); break;
                }
                case "ISS":
                case "LSS":
                {
                    var action = Core.Reflection.CreateAction<long, string, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].LongValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "FSS":
                case "DSS":
                {
                    var action = Core.Reflection.CreateAction<double, string, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].DoubleValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "BSS":
                {
                    var action = Core.Reflection.CreateAction<bool, string, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].BoolValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                case "SSS":
                {
                    var action = Core.Reflection.CreateAction<string, string, string, T>(presenter, method);
                    proxyAction += (x => action(x[0].StringValue(), x[1].StringValue(), x[2].StringValue())); break;
                }
                default:
                {
                    Log.Reflection.Exception(new Exception($"UNDEFIDED ACTION BUILD FOR TYPE: {parametersTagName}"));
                    break;
                }
            }

            return proxyAction;
        }

        private static string GetDelegateTagNameByParamenterTypes(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var types = from parameter in parameters
                select parameter.ParameterType;

            var result = "";
            foreach (var type in types)
            {
                var flag = ProxyParameter.CastFlag(type);
                if (string.IsNullOrEmpty(flag))
                {
                    Log.Reflection.E($"unsupport slot delegate param type {type.Name}");
                    return "ERROR";
                }

                result += flag;
            }

            return result;
        }
    }
}
