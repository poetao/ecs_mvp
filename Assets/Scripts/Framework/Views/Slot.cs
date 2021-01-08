using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using MVP.Framework.Core.Reflections;
using MVP.Framework.Core.States;

namespace MVP.Framework.Views
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SlotAttribute : Attribute
    {
        public string name { get; private set; }
        public ProxyParameter[] parameters;
        public float throttle;

        public SlotAttribute(string name)
        {
            this.name = name;
            this.parameters = new ProxyParameter[] {};
            this.throttle = 0;
        }

        public SlotAttribute(string name, float throttle)
        {
            this.name = name;
            this.parameters = new ProxyParameter[] {};
            this.throttle = throttle;
        }

        public SlotAttribute(string name, object[] parameters)
        {
            this.name = name;
            this.parameters = (from parameter in parameters
                select ConvertToParameter(parameter)).ToArray();
            this.throttle = 0;
        }

        public SlotAttribute(string name, object[] parameters, float throttle)
        {
            this.name = name;
            this.parameters = (from parameter in parameters
                select ConvertToParameter(parameter)).ToArray();
            this.throttle = throttle;
        }

        public SlotAttribute(string name, ProxyParameter[] parameters)
        {
            this.name = name;
            this.parameters = parameters;
        }

        public SlotAttribute(string name, ProxyParameter[] parameters, float throttle)
        {
            this.name = name;
            this.parameters = parameters;
            this.throttle = throttle;
        }

        private ProxyParameter ConvertToParameter(object parameter)
        {
            if (parameter is bool)
            {
                return new ProxyParameter((bool)parameter);
            }

            if (parameter is int)
            {
                return new ProxyParameter((int)parameter);
            }

            if (parameter is float)
            {
                return new ProxyParameter((float)parameter);
            }

            if (parameter is string)
            {
                return new ProxyParameter((string)parameter);
            }

            return new ProxyParameter(0f);
        }
    }

    public static class Slot
    {
        public static void Bind(GameObject target, string name, Presenter presenter, List<ProxyParameter> proxyParameters, float throttle = 0)
        {
            if (target == null || presenter == null) return;
            if (string.IsNullOrEmpty(name)) return;

            var action = Core.Reflection.GetMethodActionDelegate<Presenter>(name, presenter);
            if (action == null) return;

            var parameters = Core.Reflection.GetParemeters(proxyParameters.ToArray());
            if (ProcessButton(target, action, parameters, throttle)) return;
            if (ProcessInputField(target, action, parameters, throttle)) return;

            // @todo add more compoents support
        }

        private static bool ProcessInputField(GameObject target, Action<Any[]> action, Any[] parameters, float throttle)
        {
            var inputField = target.GetComponent<InputField>();
            if (inputField == null) return false;

	         inputField.OnValueChangedAsObservable().ThrottleFirst(TimeSpan.FromSeconds(throttle))
                 .Subscribe((x) => action(new Any[] {Any.Create(x)})).AddTo(target);

            return true;
        }

        private static bool ProcessButton(GameObject target, Action<Any[]> action, Any[] parameters, float throttle)
        {
            var button = target.GetComponent<Button>();
            if (button == null) return false;

            button.OnClickAsObservable().ThrottleFirst(TimeSpan.FromSeconds(throttle))
                .Subscribe(x => action(parameters)).AddTo(target);

            return true;
        }
    }
}
