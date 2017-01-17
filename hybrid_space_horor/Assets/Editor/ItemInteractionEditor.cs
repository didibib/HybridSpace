using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ItemInteraction))]
public class ItemInteractionEditor : Editor {

    private ItemInteraction myTarget = null;

    void OnEnable()
    {
        myTarget = (ItemInteraction)target;
    }

	public override void OnInspectorGUI()
    {
        
        myTarget.interactWithName = EditorGUILayout.TextField("Name interact with", myTarget.interactWithName);
        myTarget.interactWithObject = (GameObject)EditorGUILayout.ObjectField("Influence this", myTarget.interactWithObject, typeof(GameObject), true);
        myTarget.isKeycard = EditorGUILayout.Toggle("isKeycard", myTarget.isKeycard);
        myTarget.isBattery = EditorGUILayout.Toggle("isBattery", myTarget.isBattery);
        
        if (myTarget.isBattery)
        {
            myTarget.batteryValue = EditorGUILayout.Slider("Fill percantage", myTarget.batteryValue, 0f, 1f);
            myTarget.insertBatteryAudio = (AudioClip)EditorGUILayout.ObjectField("Insert battery audio", myTarget.insertBatteryAudio, typeof(AudioClip), true);
        }

        if (myTarget.isKeycard)
        {
            myTarget.accessGrantedAudio = (AudioClip)EditorGUILayout.ObjectField("Acces granted audio", myTarget.accessGrantedAudio, typeof(AudioClip), true);
            myTarget.accessDeniedAudio = (AudioClip)EditorGUILayout.ObjectField("Acces granted audio", myTarget.accessDeniedAudio, typeof(AudioClip), true);
        }
    }
}
