using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColoredObstacle : ActivatedObject
{
    [SerializeField] private Collider2D collider2D;
    protected override void SetObjectState(bool active)
    {
        //if (objectEnabled == active) return;
        //else objectEnabled = active;
        collider2D.enabled = active;
        var c = spriteRenderer.color;
        c.a = active ? 1 : .5f;
        spriteRenderer.color = c;
    }
    

}
#if UNITY_EDITOR
[CustomEditor(typeof(ColoredObstacle))]
public class ColoredObstacleEditor : Editor
{
    //public SerializedObject leverObject;
    public SerializedProperty
        color_Prop,
        renderer_Prop,
        colliderProp,
        enabledProp;

    public ColorManager.GameColor color;
     
    void OnEnable ()
    {
        color_Prop = serializedObject.FindProperty("color");
        renderer_Prop = serializedObject.FindProperty("spriteRenderer");
        colliderProp = serializedObject.FindProperty("collider2D");
        enabledProp = serializedObject.FindProperty("startEnabled");
        color = (ColorManager.GameColor) color_Prop.enumValueIndex;
    }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();

        color = (ColorManager.GameColor) EditorGUILayout.EnumPopup(color);
        color_Prop.enumValueIndex = (int) color;
        renderer_Prop.objectReferenceValue = (SpriteRenderer) EditorGUILayout.ObjectField(renderer_Prop.objectReferenceValue, 
            typeof(SpriteRenderer),true);
        (renderer_Prop.objectReferenceValue as SpriteRenderer).color = ColorManager.GetHue(color);
        
        colliderProp.objectReferenceValue = (Collider2D) EditorGUILayout.ObjectField(colliderProp.objectReferenceValue, typeof(Collider2D),true);
        enabledProp.boolValue = EditorGUILayout.Toggle("Start Enabled", enabledProp.boolValue);
        serializedObject.ApplyModifiedProperties();
    }
         
         
        
}
#endif