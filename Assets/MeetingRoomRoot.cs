using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingRoomRoot : MonoBehaviour
{
    [SerializeField]
    public List<Transform> m_MeetingRoomList = new List<Transform>();
    [SerializeField]
    public UIDoorPlate m_DoorPlate;

    private void Awake()
    {
        UpdateDoorPlate();
    }

    private void UpdateDoorPlate()
    {
        if (m_DoorPlate == null) return;
        foreach (var i in m_MeetingRoomList)
        {
            UIDoorPlate plate;
            if (i.childCount > 0 && i.GetChild(0) != null)
            {
                plate = i.GetChild(0).GetComponent<UIDoorPlate>();
                plate.transform.localPosition = Vector3.zero;
            }
            else plate = Instantiate(m_DoorPlate, transform);
            plate.SetDoorPlateText(i.name);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var i in m_MeetingRoomList)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(i.position, 0.5f);
        }
    }

    public void Referesh()
    {
#if UNITY_EDITOR
        var rooms = GameObject.FindGameObjectsWithTag("MeetingRoomAnchor");
        m_MeetingRoomList.Clear();
        foreach (var i in rooms)
        {
            if (i != transform)
                m_MeetingRoomList.Add(i.transform);

            UpdateDoorPlate();
        }
#endif
    }
}
