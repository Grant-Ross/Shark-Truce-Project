using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fan : ActivatedObject
{
    [SerializeField] private Collider2D fanBody;
    [SerializeField] private Collider2D airZone;
    [SerializeField] private ParticleSystem pSystem;
     
    protected override void SetObjectState(bool active)
    {
        if (active) pSystem.Play();
        else pSystem.Stop();
        airZone.enabled = active;
    }
}
[CustomEditor(typeof(Fan))]
public class FanEditor : Editor
{
    //public SerializedObject leverObject;
    public SerializedProperty
        color_Prop,
        renderer_Prop,
        colliderProp,
        airTriggerProp,
        particleProp,
        enabledProp;
    

    public ColorManager.GameColor color;
     
    void OnEnable ()
    {
        color_Prop = serializedObject.FindProperty("color");
        renderer_Prop = serializedObject.FindProperty("spriteRenderer");
        colliderProp = serializedObject.FindProperty("fanBody");
        airTriggerProp = serializedObject.FindProperty("airZone");
        particleProp = serializedObject.FindProperty("pSystem");
        enabledProp = serializedObject.FindProperty("startEnabled");
        color = (ColorManager.GameColor) color_Prop.enumValueIndex;
    }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();
        color = (ColorManager.GameColor) EditorGUILayout.EnumPopup((ColorManager.GameColor)color);
        color_Prop.enumValueIndex = (int) color;
        renderer_Prop.objectReferenceValue = (SpriteRenderer) EditorGUILayout.ObjectField(renderer_Prop.objectReferenceValue, 
            typeof(SpriteRenderer),true);
        (renderer_Prop.objectReferenceValue as SpriteRenderer).color = ColorManager.GetHue(color);
        
        colliderProp.objectReferenceValue = (Collider2D) EditorGUILayout.ObjectField("Fan Body",colliderProp.objectReferenceValue, 
            typeof(Collider2D),true);

        airTriggerProp.objectReferenceValue = (Collider2D) EditorGUILayout.ObjectField("Air Zone",airTriggerProp.objectReferenceValue, typeof(Collider2D),true);
        particleProp.objectReferenceValue = (ParticleSystem) EditorGUILayout.ObjectField("Air Particles",particleProp.objectReferenceValue, typeof(ParticleSystem),true);

        enabledProp.boolValue = EditorGUILayout.Toggle("Start Enabled", enabledProp.boolValue);
        serializedObject.ApplyModifiedProperties();
    }
         
         
        
}