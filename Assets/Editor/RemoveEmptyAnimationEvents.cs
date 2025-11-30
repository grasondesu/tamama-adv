using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RemoveEmptyAnimationEvents : EditorWindow
{
    [MenuItem("Tools/Remove Empty Animation Events")]
    public static void ShowWindow()
    {
        GetWindow<RemoveEmptyAnimationEvents>("Remove Empty Animation Events");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Scan and Remove Empty Events"))
        {
            RemoveEmptyEvents();
        }
    }

    private static void RemoveEmptyEvents()
    {
        string[] guids = AssetDatabase.FindAssets("t:AnimationClip");
        int totalRemoved = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);

            if (clip == null) continue;

            AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
            List<AnimationEvent> newEvents = new List<AnimationEvent>();

            foreach (AnimationEvent ev in events)
            {
                if (!string.IsNullOrEmpty(ev.functionName))
                {
                    newEvents.Add(ev);
                }
                else
                {
                    totalRemoved++;
                    Debug.Log($"Removed empty AnimationEvent from {clip.name}");
                }
            }

            if (newEvents.Count != events.Length)
            {
                AnimationUtility.SetAnimationEvents(clip, newEvents.ToArray());
                EditorUtility.SetDirty(clip);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Done. Total empty events removed: {totalRemoved}");
    }
}
