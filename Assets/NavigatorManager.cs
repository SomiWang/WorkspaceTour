using System.Collections.Generic;
using UnityEngine;

public class NavigatorManager : MonoBehaviour
{
    public enum Type
    {
        MeetingRoom,
        Partition
    }

    [SerializeField]
    NavMeshAgentController agentController;
    [SerializeField]
    private MeetingRoomRoot m_MeetingRoomRoot;
    [SerializeField]
    private UIAnchorRoot m_PartitionAnchorRoot;

    public List<Transform> MeetingRooms
    {
        get
        {
            return m_MeetingRoomRoot.m_MeetingRoomList;
        }
    }
    private List<Transform> _partitionAnchorList;
    public List<Transform> PartitionAnchorList
    {
        get
        {
            if (_partitionAnchorList == null)
            {
                List<Transform> list = new List<Transform>();

                foreach (var i in m_PartitionAnchorRoot.Anchors)
                {
                    list.Add(i.UI.transform);
                }
                _partitionAnchorList = list;
            }
            return _partitionAnchorList;
        }
    }

    public static NavigatorManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetAgentTarget(int index, Type type)
    {
        if (agentController == null) return;
        switch (type)
        {
            case Type.MeetingRoom:
                if (MeetingRooms[index] != null)
                    agentController.goal = MeetingRooms[index];
                break;
            case Type.Partition:
                if (m_PartitionAnchorRoot.Anchors[index] != null)
                {
                    transform.position = m_PartitionAnchorRoot.Anchors[index].OriginPos;
                    gameObject.name = m_PartitionAnchorRoot.Anchors[index].UI.name;
                    agentController.goal = transform;
                }
                break;
        }
    }

}
