using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DoozyUI;

namespace Workspace.Client.UI
{
    public class UIPartition : UIBehavior
    {
        [SerializeField]
        private TextMeshProUGUI m_NameText;
        [SerializeField]
        private Button m_Button;
        private PartitionInfo partitionInfo;

        private void Awake()
        {
            if (m_Button) m_Button.onClick.AddListener(_OnButtonClick);
        }

        private void _OnButtonClick()
        {

        }

        public void SetPartitionInfo(PartitionInfo info)
        {
            if (info == null) return;
            partitionInfo = info;
            m_NameText.text = partitionInfo.Name;
            gameObject.name = partitionInfo.Name;
        }
    }
}