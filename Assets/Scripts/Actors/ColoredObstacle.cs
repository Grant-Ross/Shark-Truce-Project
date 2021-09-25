using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColoredObstacle : ActivatedObject
{

    protected override void SetObjectState(bool active)
    {
        if (objectEnabled == active) return;
        else objectEnabled = active;
        
    }
    

}
[CustomEditor(typeof(ColoredObstacle))]
public class ColoredObstacleEditor : Editor
{
    //public SerializedObject leverObject;
    public SerializedProperty
        color_Prop,
        renderer_Prop,
        colliderProp;

    public ColorManager.GameColor color;
     
    void OnEnable ()
    {
        color_Prop = serializedObject.FindProperty("color");
        renderer_Prop = serializedObject.FindProperty("spriteRenderer");
        colliderProp = serializedObject.FindProperty("collider2D");
        color = (ColorManager.GameColor) color_Prop.enumValueIndex;
    }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();

        //color_Prop.enumValueIndex = (int)(ColorManager.GameColor) EditorGUILayout.EnumPopup((ColorManager.GameColor)color_Prop.enumValueIndex);
        color = (ColorManager.GameColor) EditorGUILayout.EnumPopup((ColorManager.GameColor)color);
        color_Prop.enumValueIndex = (int) color;
        renderer_Prop.objectReferenceValue = (SpriteRenderer) EditorGUILayout.ObjectField(renderer_Prop.objectReferenceValue, 
            typeof(SpriteRenderer),true);
        (renderer_Prop.objectReferenceValue as SpriteRenderer).color = ColorManager.GetHue(color);
        
        colliderProp.objectReferenceValue = (Collider2D) EditorGUILayout.ObjectField(colliderProp.objectReferenceValue, typeof(Collider2D),true);

        serializedObject.ApplyModifiedProperties();
    }
         
         
        
}
