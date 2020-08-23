using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Workspace/Config/PartitionConfig")]
public class PartitionConfiguration : ScriptableSingleton<PartitionConfiguration>
{
    [SerializeField]
    private PartitionInfo[] m_PartitionInfos;
    public PartitionInfo[] PartitionInfos { get { return m_PartitionInfos; } }
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