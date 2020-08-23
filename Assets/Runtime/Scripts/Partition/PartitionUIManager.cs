using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Workspace.Client.UI
{
    public class PartitionUIManager : MonoBehaviour
    {
        [SerializeField]
        UIAnchorRoot m_AnchorRoot;
        [SerializeField]
        UIPartition m_PartitionPrefab;

        public static PartitionUIManager Instance;

        private void Awake()
        {
            PartitionUIManager[] mgrs = FindObjectsOfType<PartitionUIManager>();
            if (mgrs.Length > 1)
            {
                Destroy(this.gameObject);
            }
            Instance = this;
            //_CreateAllPartitionUI();
            _SetAllInfos();
        }

        private void _SetAllInfos()
        {
            if (m_AnchorRoot == null || m_AnchorRoot.Anchors == null || m_AnchorRoot.Anchors.Count == 0) return;
            for (int i = 0; i < m_AnchorRoot.Anchors.Count; i++)
            {
                if (m_AnchorRoot.Anchors[i].UI is UIPartition)
                {
                    var part = (UIPartition)m_AnchorRoot.Anchors[i].UI;
                    var follower = m_AnchorRoot.Anchors[i].UI.GetComponent<UIFollower>();
                    if (follower) follower.OriginPos = m_AnchorRoot.Anchors[i].OriginPos;
                    if (PartitionConfiguration.Instance.PartitionInfos.Length > i)
                    {
                        var _info = PartitionConfiguration.Instance.PartitionInfos[i];
                        part.SetPartitionInfo(_info);
                    }
                }
            }
        }

        private void _CreatePartitionUI(Transform anchor, int index)
        {
            if (m_PartitionPrefab == null) return;
            var _partition = Instantiate(m_PartitionPrefab, anchor) as UIPartition;
            var _info = PartitionConfiguration.Instance.PartitionInfos[index];
            if (_info != null) _partition.SetPartitionInfo(_info);
        }
    }
}