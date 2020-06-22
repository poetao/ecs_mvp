using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using MVP.Framework.Views;
using MVP.Framework.Core.Reflections;

namespace MVP.Editors.Inspector
{
	[CanEditMultipleObjects, CustomEditor(typeof(Proxy))]
	public class ProxyCustomEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			this.serializedObject.Update();

			var view = this.serializedObject.FindProperty("View");
			EditorGUILayout.PropertyField(view);

			var proxy = this.target as Proxy;
			InspectorCreateScriptButton(proxy.gameObject, view.stringValue);

			var viewType = GetViewType(view.stringValue);
			InspectorLinkExports(viewType);
			InspectorSlotExports(viewType);

			EditorGUILayout.Space();
			this.serializedObject.ApplyModifiedProperties();
		}

		private Type GetViewType(string name)
		{
			if (string.IsNullOrEmpty(name)) return null;

			if (Framework.Core.Path.instance == null) Framework.Core.Path.Setup();
			var path = Framework.Core.Path.instance.Resolve(name, Framework.Resource.TYPE.View);
			return Framework.Core.Reflection.GetRuntimeType(path);
		}

		private void BuildViewScript(GameObject node, string relatePath)
		{
			dynamic data = ScriptAutoBuilder.BuildViewScript(node, ScriptAutoBuilder.GetViewPath(relatePath));
			if (data == null) return;

			var components = this.serializedObject.FindProperty("linkComponents");
			components.arraySize = data.linkData.Length;
			for (var i = 0; i < components.arraySize; ++i)
			{
				GameObject go = data.linkData[i].gameObject;
				var component = components.GetArrayElementAtIndex(i);
				component.objectReferenceValue = go;
			}

			components = this.serializedObject.FindProperty("slotComponents");
			components.arraySize = data.slotData.Length;
			for (var i = 0; i < components.arraySize; ++i)
			{
				GameObject go = data.slotData[i].gameObject;
				var component = components.GetArrayElementAtIndex(i);
				component.objectReferenceValue = go;
			}
		}

		private void BuildPresenterScript(GameObject node, string relatePath)
		{
			ScriptAutoBuilder.BuildPresenterScript(node, ScriptAutoBuilder.GetPresenterPath(relatePath));
		}

		private void InspectorCreateScriptButton(GameObject node, string relatePath)
		{
			if (node == null) return;

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Create View Script"))
			{
				BuildViewScript(node, relatePath);
			}

			if (GUILayout.Button("Create Presenter Script"))
			{
				BuildPresenterScript(node, relatePath);
			}
			EditorGUILayout.EndHorizontal();
		}

		private void InspectorLinkExports(Type type)
		{
			if (type == null) return;

			var attributes = type.GetCustomAttributes(typeof(LinkAttribute), false);
			var names = (from e in attributes select (e as LinkAttribute).name).ToArray();
			if (names == null) return;

			var methods = type.GetMethods();
			foreach (var method in methods)
			{
				method.GetParameters();
			}
			var properties = this.serializedObject.FindProperty("linkProperties");
			var components = this.serializedObject.FindProperty("linkComponents");
			properties.arraySize = names.Length;
			if (components.arraySize < names.Length) components.arraySize = names.Length;
			if (EditorGUILayout.PropertyField(components))
			{
				EditorGUI.indentLevel++;
				for (int i = 0; i < names.Length; ++i)
				{
					var component = components.GetArrayElementAtIndex(i);
					var property = properties.GetArrayElementAtIndex(i);
					property.stringValue = names[i];
					EditorGUILayout.PropertyField(component, new GUIContent(property.stringValue));
				}
				EditorGUI.indentLevel--;
			}
		}

		private void InspectorSlotExports(Type type)
		{
			if (type == null) return;

			var attributes = type.GetCustomAttributes(typeof(SlotAttribute), false).OfType<SlotAttribute>().ToArray();
			var length = attributes.Count();
			
			var properties = this.serializedObject.FindProperty("slotProperties");
			var components = this.serializedObject.FindProperty("slotComponents");
			var parameters = this.serializedObject.FindProperty("slotParameters");
			var throttles  = this.serializedObject.FindProperty("slotThrottles");
			properties.arraySize = length;
			parameters.arraySize = length;
			components.arraySize = length;
			throttles.arraySize  = length;
			if (EditorGUILayout.PropertyField(components))
			{
				EditorGUI.indentLevel++;
				for (int i = 0; i < length; ++i)
				{
					var property = properties.GetArrayElementAtIndex(i);
					property.stringValue = attributes[i].name;
					var component = components.GetArrayElementAtIndex(i);
					EditorGUILayout.PropertyField(component, new GUIContent(property.stringValue));
					var parameter = parameters.GetArrayElementAtIndex(i);
					InspectorSlotPramaters(parameter.FindPropertyRelative("array"), attributes[i].parameters);
					var throttle = throttles.GetArrayElementAtIndex(i);
					throttle.floatValue = attributes[i].throttle;
				}
				EditorGUI.indentLevel--;
			}
		}

		private void InspectorSlotPramaters(SerializedProperty properties, ProxyParameter[] parameters)
		{
			properties.arraySize = parameters.Length;
			for (int i = 0; i < properties.arraySize; ++i)
			{
				var parameter = parameters[i];
				var property = properties.GetArrayElementAtIndex(i);
				var intProperty = property.FindPropertyRelative("intValue");
				var boolProperty = property.FindPropertyRelative("boolValue");
				var stringProperty = property.FindPropertyRelative("stringValue");
				var floatProperty = property.FindPropertyRelative("floatValue");
				var useFlagProperty = property.FindPropertyRelative("useFlag");
				useFlagProperty.stringValue = parameter.useFlag;
				switch (parameter.useFlag)
				{
					case "I": intProperty.intValue = parameter.intValue; break;
					case "B": boolProperty.boolValue = parameter.boolValue; break;
					case "F": floatProperty.floatValue = parameter.floatValue; break;
					case "S": stringProperty.stringValue = parameter.stringValue; break;
					default: break;
				}
			}
		}
	}
}

