using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public ColorManager.GameColor color;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private const float Cooldown = .1f;
    private float _timer = 0;
    private bool _swapReady = true;
    private bool _active = false;

    private void Start()
    {
        spriteRenderer.color = ColorManager.GetHue(color);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _swapReady)
        {
            _timer = Cooldown;
            _swapReady = false;
            _active = !_active;
            LeverManager.OnLeverChange(color,_active);
        }
    }

    private void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        else _swapReady = true;
    }
}

[CustomEditor(typeof(Lever))]
public class LeverEditor : Editor
{
    //public SerializedObject leverObject;
    public SerializedProperty
        color_Prop,
        renderer_Prop;

    public ColorManager.GameColor color;
     
    void OnEnable ()
    {
        color_Prop = serializedObject.FindProperty("color");
        renderer_Prop = serializedObject.FindProperty("spriteRenderer");
        color = (ColorManager.GameColor) color_Prop.enumValueIndex;
    }
     
    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        serializedObject.Update ();

        //color_Prop.enumValueIndex = (int)(ColorManager.GameColor) EditorGUILayout.EnumPopup((ColorManager.GameColor)color_Prop.enumValueIndex);
        color = (ColorManager.GameColor) EditorGUILayout.EnumPopup((ColorManager.GameColor)color);
        color_Prop.enumValueIndex = (int) color;
        renderer_Prop.objectReferenceValue = (SpriteRenderer) EditorGUILayout.ObjectField(renderer_Prop.objectReferenceValue, 
            typeof(SpriteRenderer),true);
        (renderer_Prop.objectReferenceValue as SpriteRenderer).color = ColorManager.GetHue(color);

        serializedObject.ApplyModifiedProperties();
    }
         
         
        
}


