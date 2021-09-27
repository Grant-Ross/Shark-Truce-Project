using UnityEditor;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public ColorManager.GameColor color;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite offSprite;
    public Sprite onSprite;

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
            spriteRenderer.sprite = _active ? onSprite : offSprite;
            LeverManager.OnLeverChange(color,_active);
        }
    }

    private void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        else _swapReady = true;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Lever))]
public class LeverEditor : Editor
{
    //public SerializedObject leverObject;
    public SerializedProperty
        color_Prop,
        renderer_Prop,
        onSpriteProp,
        offSpriteProp;

    public ColorManager.GameColor color;
     
    void OnEnable ()
    {
        color_Prop = serializedObject.FindProperty("color");
        renderer_Prop = serializedObject.FindProperty("spriteRenderer");
        onSpriteProp = serializedObject.FindProperty("onSprite");
        offSpriteProp = serializedObject.FindProperty("offSprite");
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

        onSpriteProp.objectReferenceValue =
            (Sprite) EditorGUILayout.ObjectField("On Sprite", onSpriteProp.objectReferenceValue, typeof(Sprite), false);
        offSpriteProp.objectReferenceValue =
            (Sprite) EditorGUILayout.ObjectField("Off Sprite", offSpriteProp.objectReferenceValue, typeof(Sprite), false);

        serializedObject.ApplyModifiedProperties();
    }
         
         
        
}

#endif
