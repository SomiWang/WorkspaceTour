using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeetingRoomRoot))]
public class MeetingRoomRootEditor : Editor
{
    MeetingRoomRoot Target;
    private void OnEnable()
    {
        Target = (MeetingRoomRoot)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Refresh"))
        {
            Target.Referesh();
        }
    }

}
