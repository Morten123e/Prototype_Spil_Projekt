using UnityEditor;
using UnityEngine;

// Engangs-værktøj: Tools -> Lav Krukke Prefabs
// Laver én prefab per sprite i "krukker (1).aseprite" (Krukke, Blomst, Skaar1-7).
// Hver prefab får: SpriteRenderer, PolygonCollider2D (formet efter spritten), Piece-scriptet og layer "Pieces".
// Prefabs gemmes i Assets/Prefabs/Krukke. Kan køres igen - så overskrives de.
public static class KrukkePrefabTool
{
    const string SourcePath = "Assets/Aseprites/krukker (1).aseprite";
    const string OutputFolder = "Assets/Prefabs/Krukke";

    [MenuItem("Tools/Lav Krukke Prefabs")]
    static void Make()
    {
        int piecesLayer = LayerMask.NameToLayer("Pieces");
        if (piecesLayer == -1)
        {
            Debug.LogError("Layer 'Pieces' findes ikke.");
            return;
        }

        if (!AssetDatabase.IsValidFolder("Assets/Prefabs")) AssetDatabase.CreateFolder("Assets", "Prefabs");
        if (!AssetDatabase.IsValidFolder(OutputFolder)) AssetDatabase.CreateFolder("Assets/Prefabs", "Krukke");

        int count = 0;
        foreach (Object asset in AssetDatabase.LoadAllAssetsAtPath(SourcePath))
        {
            // Aseprite-filen indeholder også texture, animationer osv. - vi vil kun have sprites.
            Sprite sprite = asset as Sprite;
            if (sprite == null) continue;

            GameObject go = new GameObject(sprite.name);
            go.layer = piecesLayer;

            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 10; // foran baggrunden

            // Tilføjes EFTER spritten er sat, så formen bliver regnet ud fra spritten.
            go.AddComponent<PolygonCollider2D>();
            go.AddComponent<Piece>();

            PrefabUtility.SaveAsPrefabAsset(go, OutputFolder + "/" + sprite.name + ".prefab");
            Object.DestroyImmediate(go);
            count++;
        }

        if (count == 0)
            Debug.LogError("Fandt ingen sprites i " + SourcePath + " - er filen importeret?");
        else
            Debug.Log(count + " prefabs lavet i " + OutputFolder);
    }
}
