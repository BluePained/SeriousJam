using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;

[Overlay(typeof(SceneView), "Custom Overlay Toolbar")]
public class CustomOverlayToolbar : ToolbarOverlay
{
    CustomOverlayToolbar() : base(SceneFinderDropdown.Id)
    {
    }

    [EditorToolbarElement(Id, typeof(SceneView))]
    public class SceneFinderDropdown : EditorToolbarButton
    {
        public const string Id = "Scenes";

        public SceneFinderDropdown()
        {
            text = "Scenes";
            clicked += ShowDropdown;
        }

        private void ShowDropdown()
        {
            string[] scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" })
                .Select(s => AssetDatabase.GUIDToAssetPath(s)).ToArray();

            if (scenePaths.Length == 0) return;

            var menu = new GenericMenu();

            foreach (string scene in scenePaths)
            {
                string path = System.IO.Path.GetFileNameWithoutExtension(scene);

                menu.AddItem(new GUIContent(path), false, () =>
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(scene);
                    }
                });
            }

            menu.ShowAsContext();
        }
    }
}