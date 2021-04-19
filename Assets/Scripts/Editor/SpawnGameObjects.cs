using UnityEditor;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    [MenuItem("GameObject/Weather/Fog", false, 10)]
    public static void AddFog(MenuCommand menuCommand)
    {
        const string name = "Fog";
        var go = Resources.Load<GameObject>("Prefabs/Weather/Fog");
        var g = Instantiate(go, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObjectUtility.SetParentAndAlign(g, menuCommand.context as GameObject);
        g.name = name;
        Undo.RegisterCreatedObjectUndo(g, "Create " + go.name);
        Selection.activeObject = g;
    }

    [MenuItem("GameObject/Weather/Rain", false, 11)]
    public static void AddRain(MenuCommand menuCommand)
    {
        const string name = "Rain";
        var go = Resources.Load<GameObject>("Prefabs/Weather/Rain");
        var g = Instantiate(go, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObjectUtility.SetParentAndAlign(g, menuCommand.context as GameObject);
        g.name = name;
        Undo.RegisterCreatedObjectUndo(g, "Create " + go.name);
        Selection.activeObject = g;
    }

    [MenuItem("GameObject/Liquid/Water", false, 12)]
    public static void AddWater(MenuCommand menuCommand)
    {
        const string name = "Water";
        var go = Resources.Load<GameObject>("Prefabs/Liquid/Water");
        var g = Instantiate(go, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObjectUtility.SetParentAndAlign(g, menuCommand.context as GameObject);
        g.name = name;
        Undo.RegisterCreatedObjectUndo(g, "Create " + go.name);
        Selection.activeObject = g;
    }
}
