using System;
using System.Collections.Generic;
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
			serializedObject.Update();

			var proxy = target as Proxy;
			InspectorBaseInfo(proxy.gameObject, proxy.assembly);

			var viewType = GetViewScriptType(proxy.path, proxy.isComponent, proxy.assembly);
			InspectorLinkExports(viewType);
			InspectorSlotExports(viewType);
			InspectorCompExports(viewType);

			EditorGUILayout.Space();
			serializedObject.ApplyModifiedProperties();
		}

		private void InspectorBaseInfo(GameObject node, string assembly)
		{
			var isComponentProperty = this.serializedObject.FindProperty("isComponent");
			EditorGUILayout.PropertyField(isComponentProperty);
			var isComponent = isComponentProperty.boolValue;
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

			EditorGUI.indentLevel += 2;
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
			EditorGUI.indentLevel -= 2;

			serializedObject.ApplyModifiedProperties();
		}

		private void InspectorAssemble()
		{
			EditorGUILayout.BeginHorizontal();
			var assembleProperty = this.serializedObject.FindProperty("assembly");
			EditorGUILayout.PropertyField(assembleProperty);
			var assemblies = new List<string> {"Framework", "Game"};
			var selectedIdx = assemblies.IndexOf(assembleProperty.stringValue);
			selectedIdx = selectedIdx == -1 ? assemblies.Count - 1 : selectedIdx;
			var newSelectedIdx = EditorGUILayout.Popup(selectedIdx, assemblies.ToArray(), GUILayout.MaxWidth(125));
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
			if (EditorGUILayout.PropertyField(linkItems))
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
			var newSelectedIdx = EditorGUILayout.Popup(selectedIdx, components.ToArray(), GUILayout.MaxWidth(135));

			return components[newSelectedIdx];
		}

		private void InspectorSlotExports(Type type)
		{
			if (type == null) return;

			var attributes = type.GetCustomAttributes(typeof(SlotAttribute), false).OfType<SlotAttribute>().ToArray();
			var length = attributes.Count();
			if (length <= 0) return;
			
			var slotItems = this.serializedObject.FindProperty("slotItems");
			slotItems.arraySize = length;
			if (EditorGUILayout.PropertyField(slotItems))
			{
				EditorGUI.indentLevel++;
				for (int i = 0; i < slotItems.arraySize; ++i)
				{
					var slotItem = slotItems.GetArrayElementAtIndex(i);
					var slotItemName = slotItem.FindPropertyRelative("name");
					slotItemName.stringValue = attributes[i].name;
					var slotItemGameObject = slotItem.FindPropertyRelative("gameObject");
					EditorGUILayout.PropertyField(slotItemGameObject, new GUIContent(slotItemName.stringValue));

					var throttle = slotItem.FindPropertyRelative("throttle");
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(throttle, new GUIContent("Throttle Time"));
					EditorGUI.indentLevel--;

					var parameters = slotItem.FindPropertyRelative("parameters");
					InspectorSlotPramaters(parameters, attributes[i].parameters);
				}
				EditorGUI.indentLevel--;
			}
		}

		private void InspectorCompExports(Type type)
		{
			if (type == null) return;

			var fields = from field in type.GetFields()
				where field.IsDefined(typeof(InspectorAttribute), false)
				select field;
			if (!fields.Any()) return;

			var inspectorItems = this.serializedObject.FindProperty("inspectorItems");
			inspectorItems.arraySize = fields.Count();
			if (EditorGUILayout.PropertyField(inspectorItems))
			{
				var i = 0;
				foreach (var field in fields)
				{
					EditorGUI.indentLevel++;
					var inspectorItem = inspectorItems.GetArrayElementAtIndex(i);
					var name = inspectorItem.FindPropertyRelative("name");
					name.stringValue = field.Name;
					var parameter = inspectorItem.FindPropertyRelative("parameter");
					InsepectorProxyParamenter(parameter, new ProxyParameter(field.FieldType), name.stringValue);
					EditorGUI.indentLevel--;
					++i;
				}
			}
		}

		private void InspectorSlotPramaters(SerializedProperty properties, ProxyParameter[] parameters)
		{
			EditorGUI.indentLevel++;
			properties.arraySize = parameters.Length;
			for (int i = 0; i < properties.arraySize; ++i)
			{
				var parameter = parameters[i];
				var property = properties.GetArrayElementAtIndex(i);
				InsepectorProxyParamenter(property, parameter);
			}

			EditorGUI.indentLevel--;
		}

		private void InsepectorProxyParamenter(SerializedProperty property, ProxyParameter parameter, string name = null)
		{
			if (property == null || parameter == null) return;

			var intProperty = property.FindPropertyRelative("intValue");
			var boolProperty = property.FindPropertyRelative("boolValue");
			var stringProperty = property.FindPropertyRelative("stringValue");
			var floatProperty = property.FindPropertyRelative("floatValue");
			var gameObjectProperty = property.FindPropertyRelative("gameObject");
			var useFlagProperty = property.FindPropertyRelative("useFlag");
			useFlagProperty.stringValue = parameter.useFlag;
			switch (parameter.useFlag)
			{
				case "I":
					EditorGUILayout.PropertyField(intProperty, new GUIContent(name ?? "int"));
					break;
				case "B":
					EditorGUILayout.PropertyField(boolProperty, new GUIContent(name ?? "bool"));
					break;
				case "F":
					EditorGUILayout.PropertyField(floatProperty, new GUIContent(name ?? "float"));
					break;
				case "S":
					EditorGUILayout.PropertyField(stringProperty, new GUIContent(name ?? "string"));
					break;
				case "O":
					EditorGUILayout.PropertyField(gameObjectProperty, new GUIContent(name ?? "Game Object"));
					break;
				default: break;
			}
		}

		private Type GetViewScriptType(string path, bool isComponent, string assembly)
		{
			if (string.IsNullOrEmpty(path)) return null;

			var type = isComponent ? Framework.Resource.TYPE.Component : Framework.Resource.TYPE.View;
			if (Framework.Core.Path.instance == null) Framework.Core.Path.Setup();
			path = Framework.Core.Path.instance.Resolve(path, type, assembly);
			return Framework.Core.Reflection.GetRuntimeType(path, isComponent ? assembly : "Game");
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

