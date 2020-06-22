using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEditor;
using MVP.Framework.Core;

namespace MVP.Editors
{
	public static class ScriptAutoBuilderTemplate
	{
	   public static string ViewClass =
@"using MVP.Framework;
using MVP.Framework.Views;

namespace Views#NameSpace#
{
#Link##Slot#    public class #Class# : View {}
}
";

	    public static string PresenterClass =
@"using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters#NameSpace#
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class #Class# : Presenter
    {
        private Window window;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);

			window = builder.GetManager<Window>();
	    }
    }
}
";
	}

	public class ScriptAutoBuilder
	{
	    [MenuItem("MVP/Create & Refresh Script")]
	    public static void BuildScripts()
		{
			foreach (var go in Selection.gameObjects)
			{
				var prefab = go.transform.root.gameObject;
				var relatePath = GetPrefabRelativePath(prefab);
				BuildViewScript(prefab, GetViewPath(relatePath));
				BuildPresenterScript(prefab, GetPresenterPath(relatePath));
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

	    public static dynamic BuildViewScript(GameObject node, string scriptPath)
	    {
			var linkStr = "";
		    var linkData = (from trans in node.GetComponentsInChildren<Transform>(true)
			    where trans.name.Contains("_link_")
				select new { name = trans.name.Replace("_link_", ""), trans.gameObject }).ToArray();
			foreach (var data in linkData)
			{
				linkStr += $"    [Link(\"{data.name}\")]\n\n";
		    }

			var slotStr = "";
		    var slotData = (from trans in node.GetComponentsInChildren<Transform>(true)
			    where trans.name.Contains("_slot_")
				select new { name = trans.name.Replace("_slot_", ""), trans.gameObject }).ToArray();
			foreach (var data in slotData)
			{
				slotStr += $"    [Slot(\"{data.name}\")]\n\n";
			}

			if (File.Exists(scriptPath))
			{
                // @todo wait proc exists file
				return null;
			}

			var name		= GetNameFromScriptPath(scriptPath);
			var nameSpace	= GetNameSpaceFromScriptPath(scriptPath, "Views");
			var content		= ScriptAutoBuilderTemplate.ViewClass;
			content = content.Replace("#NameSpace#", nameSpace);
			content = content.Replace("#Class#", name);
			content = content.Replace("#Link#", linkStr);
			content = content.Replace("#Slot#", slotStr);

			WriteFile(content, name, scriptPath);
			return new { slotData, linkData };
		}

	    public static void BuildPresenterScript(GameObject node, string scriptPath)
		{
			if (File.Exists(scriptPath)) return;

			var name		= GetNameFromScriptPath(scriptPath);
			var nameSpace	= GetNameSpaceFromScriptPath(scriptPath, "Presenters");
			var content		= ScriptAutoBuilderTemplate.PresenterClass;
			content = content.Replace("#NameSpace#", nameSpace);
			content = content.Replace("#Class#", name);

			WriteFile(content, name, scriptPath); 
		}

	    public static string GetViewPath(string relatePath)
		{
			return $"{Application.dataPath}/Scripts/Game/View/{relatePath}.cs";
		}

	    public static string GetPresenterPath(string relatePath)
		{
			return $"{Application.dataPath}/Scripts/Game/Presenter/{relatePath}.cs";
		}

	    private static void WriteFile(string content, string name, string path)
		{
			var directory = path.Replace($"{name}.cs", "");
			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

			FileStream file = new FileStream(path, FileMode.CreateNew);
			StreamWriter fileW = new StreamWriter(file, new UTF8Encoding(false));
			fileW.Write(content);
			fileW.Flush();
			fileW.Close();
			file.Close();

			Log.Editor.I($"GENERATOR SCRIPT {path.Replace(Application.dataPath, "")} SUCCESS!");
		}

		private static string GetNameSpaceFromScriptPath(string path, string type)
		{
			Match match;
			switch (type)
			{
				case "Views":
					match = Regex.Match(path, @".+Scripts/Game/View(.+)/[^/]+\.cs");
					break;
				case "Presenters":
					match = Regex.Match(path, @".+Scripts/Game/Presenter(.+)/[^/]+\.cs");
					break;
				default: return "";
			}
			return match.Groups[1].Value.Replace("/", ".");
		}

		private static string GetNameFromScriptPath(string path)
	    {
			Match match = Regex.Match(path, @".+/(.+)\.cs");
			return match.Groups[1].Value;
	    }

		private static string GetPrefabRelativePath(GameObject prefab)
	    {
			var assetPath = AssetDatabase.GetAssetPath(prefab);
			return assetPath.Replace("Assets/Resources/Prefab/", "").Replace(".prefab", "");
	    }
	}
}
