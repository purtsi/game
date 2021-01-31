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

            Vector3[] vertices = mesh.vertices.Where(v => v.x <= 0).ToArray();

            //int[,] positions = new int[(int)(selection.position.x * selection.localScale.x),(int)(selection.position.y * selection.localScale.y)];
            float width = vertices.Max<Vector3>(elem => elem.x) - vertices.Min<Vector3>(elem => elem.x) * selection.transform.localScale.x;
            float height = vertices.Max<Vector3>(elem => elem.y) - vertices.Min<Vector3>(elem => elem.y) * selection.transform.localScale.y;

            Vector2 scale = selection.lossyScale;
            Bounds bounds = particlePrefab.GetComponent<SpriteRenderer>().sprite.bounds;
            int particlesAmountX = Mathf.RoundToInt(scale.x / bounds.size.x);
            int particlesAmountY = Mathf.RoundToInt(scale.y / bounds.size.y);

            for (int i = 0; i < particlesAmountX; i++)
            {
                for (int j = 0; j < particlesAmountY; j++)
                {
                    Vector2 position = new Vector2((i * bounds.size.x - (scale.x / 2) + bounds.extents.x) / scale.x, (j * bounds.size.y - (scale.y / 2) + bounds.extents.y) / scale.y);

                    //if (Mathf.Pow(position.x, 2) + Mathf.Pow(position.y, 2) < 0.25f)
                    //{
                    //    GameObject particle = Instantiate(particlePrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    //    particle.transform.SetParent(selection);
                    //    particle.transform.localPosition = position;
                    //}



                    if (vertices.Any(v => Mathf.Abs(v.x) > Mathf.Abs(position.x) && Mathf.Abs(v.y) > Mathf.Abs(position.y)))
                    {
                        GameObject particle = Instantiate(particlePrefab, Vector2.zero, Quaternion.identity) as GameObject;
                        particle.transform.SetParent(selection);
                        particle.transform.localPosition = position;
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