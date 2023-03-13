using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PivotCenter))]
public class PivotCenterInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Center Pivot"))
        {
            //Get mesh
            PivotCenter script = (PivotCenter)target;

            if (!script.gameObject.GetComponent<MeshFilter>())
            {
                Debug.LogError("No Mesh Filter on this GameObject!");
                EditorUtility.ClearProgressBar();
                return;
            }

            MeshFilter filter = script.gameObject.GetComponent<MeshFilter>();

            filter.sharedMesh.RecalculateBounds();

            Vector3 offset = script.transform.position - script.transform.TransformPoint(filter.sharedMesh.bounds.center);

            GameObject newParent = new GameObject(filter.transform.name + " Pivot");

            if (filter.transform.parent)
                newParent.transform.parent = filter.transform.parent;

            newParent.transform.localScale = filter.transform.localScale;
            newParent.transform.localPosition = Vector3.zero;
            newParent.transform.localRotation = Quaternion.identity;

            filter.transform.localPosition = offset;
            
            filter.transform.parent = newParent.transform;
        }
    }
}
