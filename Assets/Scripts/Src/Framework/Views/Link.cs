using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using Framework.Core;
using Framework.Core.States;

namespace Framework.Views
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LinkAttribute : Attribute
    {
        public string   name { get; private set; }

        public LinkAttribute(string name)
        {
            this.name = name;
        }
    }

    public static class Link
    {
        public static void Bind(GameObject target, string compType, string name, IState store, View view)
        {
            if (target == null || store == null) return;
            if (string.IsNullOrEmpty(name)) return;

            var property= GetProperty(name, view, store);
            if (property == null) property = store.GetObservable(name);
	        if (property == null) return;

	        property.SubscribeWithState(target, (value, o) =>
	        {
		        if (value == WrapBase.Empty) return;

		        bool suc;
		        switch (compType)
		        {
			        case "UnityEngine.UI.Text": suc = ProcessText(o, value); break;
			        case "UnityEngine.UI.Slider": suc = ProcessSlider(o, value); break;
			        case "UnityEngine.UI.Button": suc = ProcessButton(o, value); break;
			        case "UnityEngine.Animator": suc = ProcessAnimator(o, value); break;
			        case "UnityEngine.Animation": suc = ProcessAnimation(o, value); break;
			        default: suc = ProcessGameObject(o, value); break;
		        }

		        if (!suc)
		        {
			        Log.Framework.I("Link Bind Process Fail for {0}, {1}", o, value);
		        }
	        }).AddTo(target);
        }

        public static IObservable<WrapBase> GetProperty(string name, View view, IState store)
        {
	        var method = Reflection.GetMethod(view, name);
	        if (method == null) return null;

	        var parameters = method.GetParameters();
	        if (parameters.Length <= 0) return null;
	        var properties = (from parameterInfo in parameters
		        select store.GetObservable(parameterInfo.Name)).ToArray();

            var action = Core.Reflections.MethodDelegateBuilder.GetDelegate(view, name);
            if (action == null) return null;

	        var observable = Observable.CombineLatest(properties).Select(x =>
	        {
		        return action(x.ToArray());
	        });
	        return observable;
        }

        private static bool ProcessText(GameObject target, WrapBase property)
        {
            var text = target.GetComponent<Text>();
            if (text == null) return false;

            if (property.Is<string>())
            {
	            text.text = property.RefOf<string>();
		        return true;
		    }

            if (property.IsValueType() && !property.Is<bool>())
            {
	            text.text = property.ValueToString();
		        return true;
		    }

            return false;
        }

        private static bool ProcessButton(GameObject target, WrapBase property)
        {
            var button = target.GetComponent<Button>();
            if (button == null) return false;

            if (property.Is<bool>())
            {
	            button.interactable = property.ValueOf<bool>();
		        return true;
		    }

            return false; 
        }

        private static bool ProcessSlider(GameObject target, WrapBase property)
        {
            var slider = target.GetComponent<Slider>();
            if (slider == null) return false;

            if (property.Is<bool>())
            {
	            slider.interactable = property.ValueOf<bool>();
		        return true;
		    }

            if (property.Is<float>())
            {
	            slider.value = property.ValueOf<float>();
		        return true;
            }

            return false; 
        }

	    private static bool ProcessAnimation(GameObject target, WrapBase property)
        {
            var animation = target.GetComponent<Animation>();
            if (animation == null) return false;

            if (property.Is<string>())
            {
	            var name = property.RefOf<string>();
	            if (!string.IsNullOrEmpty(name)) animation.Play(name);
				return true;
            }

	        return false;
        }

	    private static bool ProcessAnimator(GameObject target, WrapBase property)
        {
            var animator = target.GetComponent<Animator>();
            if (animator == null) return false;

            if (property.Is<string>())
            {
	            var name = property.RefOf<string>();
	            if (!string.IsNullOrEmpty(name)) animator.SetTrigger(name);
		        return true;
            }

	        return false;
        }

	    private static bool ProcessGameObject(GameObject target, WrapBase property)
	    {
            if (property.Is<bool>())
            {
	            target.SetActive(property.ValueOf<bool>());
		        return true;
		    }

            return false;
	    }
    }
}
