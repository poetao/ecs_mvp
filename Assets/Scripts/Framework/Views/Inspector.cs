using System;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Core.States;

namespace Framework.Views
{
    [Serializable]
    public class InspectorParameter
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

        public InspectorParameter(bool value)
        {
            this.boolValue = value;
            useFlag = "B";
        }

        public InspectorParameter(int value)
        {
            this.intValue = value;
            useFlag = "I";
        }

        public InspectorParameter(uint value)
        {
            this.uintValue = value;
            useFlag = "X";
        }

        public InspectorParameter(long value)
        {
            this.longValue = value;
            useFlag = "L";
        }

        public InspectorParameter(ulong value)
        {
            this.ulongValue = value;
            useFlag = "A";
        }

        public InspectorParameter(float value)
        {
            this.floatValue = value;
            useFlag = "F";
        }

        public InspectorParameter(string value)
        {
            this.stringValue = value;
            useFlag = "S";
        }

        public InspectorParameter(GameObject gameObject)
        {
            this.gameObject = gameObject;
            useFlag = "O";
        }

        public InspectorParameter(Type type)
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

        public WrapBase WrapValue()
        {
            switch (useFlag)
            {
                case "I": return Wrap<int>.Create(intValue);
                case "X": return Wrap<uint>.Create(uintValue);
                case "L": return Wrap<long>.Create(longValue);
                case "A": return Wrap<ulong>.Create(ulongValue);
                case "S": return Wrap<string>.Create(stringValue);
                case "B": return Wrap<bool>.Create(boolValue);
                case "F": return Wrap<float>.Create(floatValue);
                case "O": return Wrap<GameObject>.Create(gameObject);
            }

            return WrapBase.Empty;
        }

        public static string CastFlag(Type type)
        {
            if (!DELEGATE_MAP.ContainsKey(type)) return "";
            return DELEGATE_MAP[type];
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorAttribute : Attribute { }

    public static class Inspector
    {
        public static void Bind(String name, InspectorParameter parameter, Component component)
        {
            Reflection.SetField(component, name, parameter.GetObjectValue());
        }
    }
}