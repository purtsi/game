using UnityEngine;
using UnityEditor;
using System.Linq;

public class PlaceParticlesEditor : EditorWindow
{
    string placeParticles = "PlaceParticles";
    string deleteChildren = "DeleteChildren";

    [MenuItem("ParticlePlacement/ParticlePlacement")]
    static void Init()
    {
        PlaceParticlesEditor ppe = (PlaceParticlesEditor)EditorWindow.GetWindow(typeof(PlaceParticlesEditor));
        ppe.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button(placeParticles))
        {
            GameObject particlePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Particle.prefab", typeof(GameObject));

            Transform selection = Selection.transforms[0];
            Mesh mesh = selection.GetComponent<MeshFilter>().sharedMesh;

            //Get the mesh size and transform it to world space
            Vector2 meshSize = Matrix4x4.Scale(selection.localScale) * mesh.bounds.size;

            //Transform vertices relative to selection rotation
            Vector3[] vertices = mesh.vertices.Select(v => selection.rotation * v).ToArray();

            //Include only "front" vertices
            vertices = vertices.Where(v => (selection.rotation * v).z <= 0).ToArray();

            Vector2 scale = selection.lossyScale;
            Bounds bounds = particlePrefab.GetComponent<SpriteRenderer>().sprite.bounds;
            int particlesAmountX = Mathf.RoundToInt(meshSize.x / bounds.size.x);
            int particlesAmountY = Mathf.RoundToInt(meshSize.y / bounds.size.y);

            for (int i = 0; i < particlesAmountX; i++)
            {
                for (int j = 0; j < particlesAmountY; j++)
                {
                    Vector3 position = new Vector3(
                        (i * bounds.size.x - (meshSize.x / 2) + bounds.extents.x) / scale.x, 
                        (j * bounds.size.y - (meshSize.y / 2) + bounds.extents.y) / scale.y);

                    if (vertices.Any(v => Mathf.Abs(v.x) > Mathf.Abs(position.x) && Mathf.Abs(v.y) > Mathf.Abs(position.y)))
                    {
                        GameObject particle = Instantiate(particlePrefab, Vector2.zero, Quaternion.identity) as GameObject;
                        particle.transform.SetParent(selection);
                        particle.transform.localPosition = selection.rotation * position;
                    }
                }
            }
        }

        if (GUILayout.Button(deleteChildren))
        {
            Transform selection = Selection.transforms[0];
            for (int i = selection.childCount-1; i >= 0; i--) {
                DestroyImmediate(selection.GetChild(i).gameObject);
            }
        }
    }
}