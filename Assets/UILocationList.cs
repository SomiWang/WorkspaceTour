using Michsky.UI.ModernUIPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILocationList : UIBehavior
{
    [SerializeField]
    private CustomDropdown m_DropdownList;
    [SerializeField]
    private Sprite m_LocationMark;
    [SerializeField]
    private Toggle m_MeetingRoomToggle;
    [SerializeField]
    private Toggle m_PartitionToggle;

    private void Awake()
    {
        if (m_MeetingRoomToggle)
            m_MeetingRoomToggle.onValueChanged.AddListener(OnToggleSelected);
        if (m_PartitionToggle)
            m_PartitionToggle.onValueChanged.AddListener(OnToggleSelected);
        RefreshDropdownList();

        if (m_DropdownList)
            m_DropdownList.dropdownEvent.AddListener(_OnDropdownEvent);
    }

    //protected override void OnShow()
    //{
    //    RefreshDropdownList();
    //}

    private void OnDestroy()
    {
        if (m_MeetingRoomToggle)
            m_MeetingRoomToggle.onValueChanged.RemoveListener(OnToggleSelected);
        if (m_PartitionToggle)
            m_PartitionToggle.onValueChanged.RemoveListener(OnToggleSelected);

        if (m_DropdownList)
            m_DropdownList.dropdownEvent.RemoveListener(_OnDropdownEvent);
    }

    private void OnToggleSelected(bool isOn)
    {
        RefreshDropdownList();
        m_DropdownList.SetupDropdown();
        m_DropdownList.UpdateValues();
    }

    private void _OnDropdownEvent(int index)
    {
        OnSelectLocation(dropdownListMap[index].Key, dropdownListMap[index].Value);
    }

    private void RefreshDropdownList()
    {
        if (m_DropdownList)
        {
            m_DropdownList.dropdownItems = _GetItemList();
            //m_DropdownList.SetupDropdown();
            //m_DropdownList.UpdateValues();
            //m_DropdownList.Start();
        }
    }

    private Dictionary<int, KeyValuePair<int, NavigatorManager.Type>> dropdownListMap;

    private List<CustomDropdown.Item> _GetItemList()
    {
        List<CustomDropdown.Item> targetList = new List<CustomDropdown.Item>();
        dropdownListMap = new Dictionary<int, KeyValuePair<int, NavigatorManager.Type>>();

        if (m_MeetingRoomToggle.isOn)
        {
            var list = _CreateItemList(NavigatorManager.Instance.MeetingRooms);
            UpdateDropdownMap(list.Count, targetList.Count, NavigatorManager.Type.MeetingRoom);
            targetList.AddRange(list);
        }
        if (m_PartitionToggle.isOn)
        {
            var list = _CreateItemList(NavigatorManager.Instance.PartitionAnchorList);
            UpdateDropdownMap(list.Count, targetList.Count, NavigatorManager.Type.Partition);
            targetList.AddRange(list);
        }
        return targetList;
    }

    private void UpdateDropdownMap(int listCount, int targetListCount, NavigatorManager.Type type)
    {
        int index = targetListCount;
        for (int i = 0; i < listCount; i++)
        {
            dropdownListMap.Add(index, new KeyValuePair<int, NavigatorManager.Type>(i, type));
            index++;
        }
    }

    private List<CustomDropdown.Item> _CreateItemList(List<Transform> list)
    {
        List<CustomDropdown.Item> _list = new List<CustomDropdown.Item>();
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (l == null) continue;
            var item = new CustomDropdown.Item
            {
                itemName = l.gameObject.name,
                itemIcon = m_LocationMark,
            };
            _list.Add(item);
        }
        return _list;
    }

    private async void OnSelectLocation(int index, NavigatorManager.Type type)
    {
        Hide();
        await Task.Delay(700);
        NavigatorManager.Instance.SetAgentTarget(index, type);
    }
}
