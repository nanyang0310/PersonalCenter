using UnityEditor;
using TMPro.EditorUtilities;

[CustomEditor(typeof(CustomTMPDropdown),true)]
[CanEditMultipleObjects]
public class CustomTMPDropdownEditor : TMPro.EditorUtilities.DropdownEditor
{
    private SerializedProperty m_isCoorect;
    private SerializedProperty m_answerIndex;
    private SerializedProperty m_answer;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_isCoorect = serializedObject.FindProperty("m_isCoorect");
        m_answerIndex= serializedObject.FindProperty("m_answerIndex");
        m_answer = serializedObject.FindProperty("m_answer");
    }
    //并且特别注意，如果用这种序列化方式，需要在 OnInspectorGUI 开头和结尾各加一句 serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();//空行
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_isCoorect);//显示我们创建的属性
        EditorGUILayout.PropertyField(m_answerIndex);
        EditorGUILayout.PropertyField(m_answer);
        serializedObject.ApplyModifiedProperties();
    }

}
