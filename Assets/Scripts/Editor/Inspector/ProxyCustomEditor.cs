using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using MVP.Framework.Views;

namespace MVP.Editors.Inspector
{
	[CanEditMultipleObjects, CustomEditor(typeof(Proxy))]
	public class ProxyCustomEditor : Editor
	{
		private bool foldoutLinkItems = true;
		private bool foldoutSlotItems = true;
		private bool foldoutCompItems = true;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var proxy = target as Proxy;
			InspectorBaseInfo(proxy.gameObject, proxy.assembly);

			var viewType = GetViewScriptType(proxy.path, proxy.isComponent, proxy.assembly);
			var presenterType = GetPresenterScriptType(proxy.path, proxy.presenterRef);
			InspectorLinkExports(viewType);
			InspectorSlotExports(viewType, presenterType);
			InspectorCompExports(viewType);

			EditorGUILayout.Space();
			serializedObject.ApplyModifiedProperties();
		}

		private void InspectorBaseInfo(GameObject node, string assembly)
		{
			var isComponent = InspectorType();
			if (isComponent) InspectorAssemble();
			var path = this.serializedObject.FindProperty("path");
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(path);
			var buttonText = isComponent ? "Create Component" : "Create View";
			if (!string.IsNullOrEmpty(path.stringValue)
			     && GUILayout.Button(buttonText, GUILayout.Width(125)))
			{
				if (isComponent)
				{
					BuildComponentScript(node, path.stringValue, assembly);
				}
				else
				{
					BuildViewScript(node, path.stringValue);
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUI.indentLevel += 1;
			EditorGUILayout.BeginHorizontal();
			var presenter = this.serializedObject.FindProperty("presenterRef");
			EditorGUILayout.PropertyField(presenter);
			if (!string.IsNullOrEmpty(path.stringValue)
			    && GUILayout.Button("Create Presenter", GUILayout.Width(125)))
			{
				var refPath = string.IsNullOrEmpty(presenter.stringValue) ? "" : $"/{presenter.stringValue}";
				BuildPresenterScript(node, $"{path.stringValue}{refPath}", assembly);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel -= 1;

			serializedObject.ApplyModifiedProperties();
		}

		private bool InspectorType()
		{
			var isComponentProperty = this.serializedObject.FindProperty("isComponent");
			var types = new List<string> {"View", "Component"};
			var selectedIdx = types.IndexOf(isComponentProperty.boolValue ? "Component" : "View");
			selectedIdx = selectedIdx == -1 ? types.Count - 1 : selectedIdx;
			var newSelectedIdx = EditorGUILayout.Popup("Type", selectedIdx, types.ToArray());
			isComponentProperty.boolValue = types[newSelectedIdx] == "Component";
			return isComponentProperty.boolValue;
		}

		private void InspectorAssemble()
		{
			EditorGUILayout.BeginHorizontal();
			var assembleProperty = this.serializedObject.FindProperty("assembly");
			var assemblies = new List<string> {"Framework", "Game"};
			var selectedIdx = assemblies.IndexOf(assembleProperty.stringValue);
			selectedIdx = selectedIdx == -1 ? assemblies.Count - 1 : selectedIdx;
			var newSelectedIdx = EditorGUILayout.Popup("Assembly", selectedIdx, assemblies.ToArray());
			assembleProperty.stringValue = assemblies[newSelectedIdx];
			EditorGUILayout.EndHorizontal();
		}

		private void InspectorLinkExports(Type type)
		{
			if (type == null) return;

			var attributes = type.GetCustomAttributes(typeof(LinkAttribute), false);
			var names = (from e in attributes select (e as LinkAttribute).name).ToArray();
			if (names.Length <= 0) return;

			var linkItems = this.serializedObject.FindProperty("linkItems");
			linkItems.arraySize = names.Length;
			foldoutLinkItems = EditorGUILayout.Foldout(foldoutLinkItems, "Link Items");
			if (foldoutLinkItems)
			{
				EditorGUI.indentLevel++;
				for (int i = 0; i < linkItems.arraySize; ++i)
				{
					EditorGUILayout.BeginHorizontal();
					var linkItem = linkItems.GetArrayElementAtIndex(i);
					var linkItemName = linkItem.FindPropertyRelative("name");
					linkItemName.stringValue = names[i];
					var linkItemGameObject = linkItem.FindPropertyRelative("gameObject");
					EditorGUILayout.PropertyField(linkItemGameObject, new GUIContent($"{linkItemName.stringValue}"));
					var linkItemType = linkItem.FindPropertyRelative("componentType");
					var gameObject = linkItemGameObject.objectReferenceValue as GameObject;
					linkItemType.stringValue = InspectorUnityComponents(gameObject, linkItemType.stringValue);
					EditorGUILayout.EndHorizontal();
				}
				EditorGUI.indentLevel--;
			}
		}

		private string InspectorUnityComponents(GameObject go, string componentType)
		{
			if (go == null) return "";

			var compos = go.GetComponents<Component>();
			var components = (from component in go.GetComponents<Component>()
				select component.GetType().ToString()).ToList();
			components.Insert(0, "UnityEngine.GameObject");
			var selectedIdx = components.IndexOf(componentType);
			selectedIdx = selectedIdx == -1 ? components.Count - 1 : selectedIdx;
			var newSelectedIdx = EditorGUILayout.Popup(selectedIdx, components.ToArray(), GUILayout.MaxWidth(140));

			return components[newSelectedIdx];
		}

		private void InspectorSlotExports(Type viewType, Type presenterType)
		{
			if (viewType == null) return;

			var attributes = viewType.GetCustomAttributes(typeof(SlotAttribute), false).OfType<SlotAttribute>().ToArray();
			var length = attributes.Count();
			if (length <= 0) return;
			
			var slotItems = this.serializedObject.FindProperty("slotItems");
			slotItems.arraySize = length;
			foldoutSlotItems = EditorGUILayout.Foldout(foldoutSlotItems, "Slot Items");
			if (foldoutSlotItems)
			{
				EditorGUI.indentLevel++;
				for (int i = 0; i < slotItems.arraySize; ++i)
				{
					var slotItem = slotItems.GetArrayElementAtIndex(i);
					var slotItemName = slotItem.FindPropertyRelative("name");
					slotItemName.stringValue = attributes[i].name;
					var slotItemGameObject = slotItem.FindPropertyRelative("gameObject");
					EditorGUILayout.PropertyField(slotItemGameObject, new GUIContent(slotItemName.stringValue));

					var parameters = slotItem.FindPropertyRelative("parameters");
					InspectorSlotPramaters(presenterType, parameters, attributes[i]);

					EditorGUI.indentLevel++;
					var throttle = slotItem.FindPropertyRelative("throttle");
					var useThrottle = Mathf.Abs(throttle.floatValue - 0) > float.Epsilon;
					var newUseThrottle = EditorGUILayout.Toggle("throttle", useThrottle);
					if (newUseThrottle != useThrottle)
					{
						throttle.floatValue = newUseThrottle ? 1.0f : 0f;
					}
					if (newUseThrottle)
					{
						EditorGUILayout.PropertyField(throttle, new GUIContent("Throttle Time"));
					}
					EditorGUI.indentLevel--;
				}
				EditorGUI.indentLevel--;
			}
		}

		private void InspectorSlotPramaters(Type presenterType, SerializedProperty properties, SlotAttribute attribute)
		{
			if (presenterType == null) return;

            var flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
			var method = presenterType.GetMethod(attribute.name, flag);
			var parameters = method.GetParameters();
			properties.arraySize = parameters.Length;

			EditorGUI.indentLevel++;
			for (int i = 0; i < properties.arraySize; ++i)
			{
				var parameter = parameters[i];
				var property = properties.GetArrayElementAtIndex(i);
				InspectorCustomeParameter(property, parameter.ParameterType, parameter.Name);
			}
			EditorGUI.indentLevel--;
		}

		private void InspectorCompExports(Type type)
		{
			if (type == null) return;

			var fields = from field in type.GetFields()
				where field.IsDefined(typeof(InspectorAttribute), false)
				select field;
			if (!fields.Any()) return;

			var parameterItems = this.serializedObject.FindProperty("parameterItems");
			parameterItems.arraySize = fields.Count();
			foldoutCompItems = EditorGUILayout.Foldout(foldoutCompItems, "Comp Items");
			if (foldoutCompItems)
			{
				var i = 0;
				foreach (var field in fields)
				{
					EditorGUI.indentLevel++;
					var parameterItem = parameterItems.GetArrayElementAtIndex(i);
					var name = parameterItem.FindPropertyRelative("name");
					name.stringValue = field.Name;
					var parameter = parameterItem.FindPropertyRelative("parameter");
					InspectorCustomeParameter(parameter, field.FieldType, name.stringValue);
					EditorGUI.indentLevel--;
					++i;
				}
			}
		}

		private void InspectorCustomeParameter(SerializedProperty property, Type type, string name)
		{
			if (property == null || type == null) return;

			var useFlagProperty = property.FindPropertyRelative("useFlag");
			useFlagProperty.stringValue = InspectorParameter.CastFlag(type);

			var label = $"{name} [{type}]";
			if (type == typeof(int))
			{
				var intProperty = property.FindPropertyRelative("intValue");
				EditorGUILayout.PropertyField(intProperty, new GUIContent(label));
			}
			else if (type == typeof(bool))
			{
				var boolProperty = property.FindPropertyRelative("boolValue");
				EditorGUILayout.PropertyField(boolProperty, new GUIContent(label));
			}
			else if (type == typeof(float))
			{
				var floatProperty = property.FindPropertyRelative("floatValue");
				EditorGUILayout.PropertyField(floatProperty, new GUIContent(label));
			}
			else if (type == typeof(string))
			{
				var stringProperty = property.FindPropertyRelative("stringValue");
				EditorGUILayout.PropertyField(stringProperty, new GUIContent(label));
			}
			else if(type == typeof(GameObject))
			{
				var gameObjectProperty = property.FindPropertyRelative("gameObject");
				EditorGUILayout.PropertyField(gameObjectProperty, new GUIContent(label));
			}
			else
			{
				Framework.Core.Log.Editor.E("Undefined InspectorParameter Type: {0}", type);
			}
		}

		private Type GetViewScriptType(string path, bool isComponent, string assembly)
		{
			if (string.IsNullOrEmpty(path)) return null;

			if (Framework.Core.Path.instance == null) Framework.Core.Path.Setup();
			var type = isComponent ? Framework.Resource.TYPE.Component : Framework.Resource.TYPE.View;
			path = Framework.Core.Path.instance.Resolve(path, type, assembly);
			return Framework.Core.Reflection.GetRuntimeType(path, isComponent ? assembly : "Game");
		}

		private Type GetPresenterScriptType(string path, string refPath)
		{
			if (string.IsNullOrEmpty(path)) return null;

			if (Framework.Core.Path.instance == null) Framework.Core.Path.Setup();
			var subPath = string.IsNullOrEmpty(refPath) ? "" : $"/{refPath}";
			var presenterPath = $"{path}{subPath}";
			var type = Framework.Resource.TYPE.Presenter;
			path = Framework.Core.Path.instance.Resolve(presenterPath, type, "Game");
			return Framework.Core.Reflection.GetRuntimeType(path, "Game");
		}

		private void BuildComponentScript(GameObject node, string relatePath, string assembly)
		{
			var path = ScriptAutoBuilder.GetComponentPath(relatePath, assembly);
			ScriptAutoBuilder.BuildComponentScript(node, path, assembly);
		}

		private void BuildViewScript(GameObject node, string relatePath)
		{
			dynamic data = ScriptAutoBuilder.BuildViewScript(node, ScriptAutoBuilder.GetViewPath(relatePath));
			if (data == null) return;

			var linkItems = this.serializedObject.FindProperty("linkItems");
			linkItems.arraySize = data.linkData.Length;
			for (var i = 0; i < linkItems.arraySize; ++i)
			{
				var linkItem = linkItems.GetArrayElementAtIndex(i);
				var linkItemName = linkItem.FindPropertyRelative("name");
				linkItemName.stringValue = data.linkData[i].name;
				var linkItemComponent = linkItem.FindPropertyRelative("component");
				linkItemComponent.objectReferenceValue = data.linkData[i].gameObject;
			}

			var slotItems = this.serializedObject.FindProperty("slotItems");
			slotItems.arraySize = data.slotData.Length;
			for (var i = 0; i < slotItems.arraySize; ++i)
			{
				var slotItem = slotItems.GetArrayElementAtIndex(i);
				var slotItemName = slotItem.FindPropertyRelative("name");
				slotItemName.stringValue = data.slotData[i].name;
				var slotItemGameObject = slotItem.FindPropertyRelative("gameObject");
				slotItemGameObject.objectReferenceValue = data.slotData[i].gameObject;
			}
		}

		private void BuildPresenterScript(GameObject node, string relatePath, string assembly)
		{
			var path = ScriptAutoBuilder.GetPresenterPath(relatePath, assembly);
			ScriptAutoBuilder.BuildPresenterScript(node, path);
		}
	}
}