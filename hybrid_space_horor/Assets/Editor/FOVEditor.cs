using UnityEngine;
using System.Collections;
using UnityEditor;
// source: https://www.youtube.com/watch?v=rQG9aUWarwE&t=90s

[CustomEditor (typeof(FOV))]
public class FOVEditor : Editor {

	void OnSceneGUI()
    {
        FOV fow = (FOV)target;
        Handles.color = Color.white; // draw the radius
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius); // draw the field of view radius

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false); // the left point of the field of view angle
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false); // the right point of the field of view angle
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fow.visibleTargets) // for the targets that are in the field of view ...
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position); // ... draw a red line to it
        }
    }
}
