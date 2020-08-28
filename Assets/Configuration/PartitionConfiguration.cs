using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Workspace/Config/PartitionConfig")]
public class PartitionConfiguration : ScriptableObject
{
    private const string path = "PartitionConfig";
    private static PartitionConfiguration _instance;
    public static PartitionConfiguration instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load(path) as PartitionConfiguration;

            return _instance;
        }
    }

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