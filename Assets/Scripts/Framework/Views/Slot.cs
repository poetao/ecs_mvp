using System;
using System.Linq;
using System.Collections.Generic;
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

        public SlotAttribute(string name)
        {
            this.name = name;
        }
    }

    public static class Slot
    {
        public static void Bind(GameObject target, string name, Presenter presenter, List<InspectorParameter> proxyParameters, float throttle = 0)
        {
            if (target == null || presenter == null) return;
            if (string.IsNullOrEmpty(name)) return;

            var action = MethodDelegateBuilder.GetDelegate(presenter, name);
            if (action == null) return;

            var parameters = CastWrapParameters(proxyParameters);
            if (ProcessButton(target, action, parameters, throttle)) return;
            if (ProcessInputField(target, action, parameters, throttle)) return;

            // @todo add more compoents support
        }

        private static bool ProcessInputField(GameObject target, Func<WrapBase[], WrapBase> action, WrapBase[] parameters, float throttle)
        {
            var inputField = target.GetComponent<InputField>();
            if (inputField == null) return false;

	         inputField.OnValueChangedAsObservable().ThrottleFirst(TimeSpan.FromSeconds(throttle))
                 .Subscribe((x) => action(new WrapBase[] {Wrap<string>.Create(x)})).AddTo(target);

            return true;
        }

        private static bool ProcessButton(GameObject target, Func<WrapBase[], WrapBase> action, WrapBase[] parameters, float throttle)
        {
            var button = target.GetComponent<Button>();
            if (button == null) return false;

            button.OnClickAsObservable().ThrottleFirst(TimeSpan.FromSeconds(throttle))
                .Subscribe(x => action(parameters)).AddTo(target);

            return true;
        }

        private static WrapBase[] CastWrapParameters(List<InspectorParameter> proxyParameters)
        {
            var parameters = (from proxyParmeter in proxyParameters
                select proxyParmeter.WrapValue()).ToArray();
            return parameters;
        }
    }
}
