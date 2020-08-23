﻿using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Workspace/Config/PartitionConfig")]
public class PartitionConfiguration : ScriptableObject
{
    static string path = "PartitionConfig";
    [SerializeField]
    private PartitionInfo[] m_PartitionInfos;
    public PartitionInfo[] PartitionInfos { get { return m_PartitionInfos; } }
    private static PartitionConfiguration _Instance;
    public static PartitionConfiguration Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = Resources.Load(path) as PartitionConfiguration;
            return _Instance;
        }
    }
}

[Serializable]
public class PartitionInfo
{
    public string Name;
}

public enum PostType
{
    Assistant,
    Enginneer,
    Artist,
    PM
}