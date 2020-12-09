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
    //�����ر�ע�⣬������������л���ʽ����Ҫ�� OnInspectorGUI ��ͷ�ͽ�β����һ�� serializedObject.Update();  serializedObject.ApplyModifiedProperties();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();//����
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_isCoorect);//��ʾ���Ǵ���������
        EditorGUILayout.PropertyField(m_answerIndex);
        EditorGUILayout.PropertyField(m_answer);
        serializedObject.ApplyModifiedProperties();
    }

}
