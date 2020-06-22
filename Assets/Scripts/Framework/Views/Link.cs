using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using MVP.Framework.Core;
using MVP.Framework.Core.States;

namespace MVP.Framework.Views
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
        public static void Bind(GameObject target, string name, IState store, View view)
        {
            if (target == null || store == null) return;
            if (string.IsNullOrEmpty(name)) return;

            var property= GetProperty(name, view, store);
            if (property == null) property = store.GetObservable(name);
	        if (property == null) return;

	        property.SubscribeWithState(target, (value, o) =>
	        {
				if (ProcessText(o, value)) return;
				if (ProcessSlider(o, value)) return;
				if (ProcessButton(o, value)) return;
				if (ProcessAnimator(target, value)) return;
				if (ProcessAnimation(target, value)) return;
				if (ProcessGameObject(target, value)) return;
				// @todo add more compoents support
	        }).AddTo(target);
        }

        public static IObservable<Any> GetProperty(string name, View view, IState store)
        {
	        var method = Reflection.GetMethod(view, name);
	        if (method == null) return null;

	        var parameters = method.GetParameters();
	        if (parameters.Length <= 0) return null;
	        var properties = (from parameterInfo in parameters
		        select store.GetObservable(parameterInfo.Name).Select(x =>
		        {
			        if (!x.Equals(Any.Empty)) return x.Object;
			        return new Any(parameterInfo.ParameterType).Object;
		        })).ToArray();

            var action = Core.Reflection.GetMethodDelegate(name, view);
            if (action == null) return null;

	        // 这里不应该用CombineLates, CombineLatest是全部完成，应该用任意一个完成，暂时没找找现成的，要重写，这里用CombineLates测试
	        var observable = Observable.CombineLatest(properties).Select(x =>
	        {
		        var args = x.ToArray();
		        return new Any(action(args));
	        });
	        return observable;
        }

        private static bool ProcessText(GameObject target, Any property)
        {
            var text = target.GetComponent<Text>();
            if (text == null) return false;

            if (property.Is<string>())
            {
	            text.text = property.Value<string>();
		        return true;
		    }

            if (property.Type.IsValueType && !property.Is<bool>())
            {
	            text.text = property.Object.ToString();
		        return true;
		    }

            return false;
        }

        private static bool ProcessButton(GameObject target, Any property)
        {
            var button = target.GetComponent<Button>();
            if (button == null) return false;

            if (property.Is<bool>())
            {
	            button.interactable = property.Value<bool>();
		        return true;
		    }

            return false; 
        }

        private static bool ProcessSlider(GameObject target, Any property)
        {
            var slider = target.GetComponent<Slider>();
            if (slider == null) return false;

            if (property.Is<bool>())
            {
	            slider.interactable = property.Value<bool>();
		        return true;
		    }

            if (property.Is<float>())
            {
	            slider.value = property.Value<float>();
		        return true;
            }

            return false; 
        }

	    private static bool ProcessAnimation(GameObject target, Any property)
        {
            var animation = target.GetComponent<Animation>();
            if (animation == null) return false;

            if (property.Is<string>())
            {
	            var name = property.Value<string>();
	            if (!string.IsNullOrEmpty(name)) animation.Play(name);
				return true;
            }

	        return false;
        }

	    private static bool ProcessAnimator(GameObject target, Any property)
        {
            var animator = target.GetComponent<Animator>();
            if (animator == null) return false;

            if (property.Is<string>())
            {
	            var name = property.Value<string>();
	            if (!string.IsNullOrEmpty(name)) animator.SetTrigger(name);
		        return true;
            }

	        return false;
        }

	    private static bool ProcessGameObject(GameObject target, Any property)
	    {
            if (property.Is<bool>())
            {
	            target.SetActive(property.Value<bool>());
		        return true;
		    }

            return false;
	    }
    }
}
