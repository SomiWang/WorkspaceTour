using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIAnchorRoot : MonoBehaviour
{
    [SerializeField]
    private UIBehavior ui;
    [SerializeField]
    private Vector2Int Count;
    [SerializeField]
    private Vector2 Offset;

    [System.Serializable]
    public class UIOriginPos
    {
        public UIBehavior UI;
        public Vector3 OriginPos;
    }

    public List<UIOriginPos> Anchors = new List<UIOriginPos>();
    private readonly string goName = "UIAnchor";

    private void Awake()
    {
        _ClearUnnecessaryAnchors();
    }

    private void _ClearUnnecessaryAnchors()
    {
        int beginIndex = Count.x * Count.y;
        for (int i = beginIndex; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i));
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var i in Anchors)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(i.OriginPos, 0.3f);
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Anchors.Clear();
            if (Count.x <= 0 || Count.y <= 0) return;
            for (int i = 0; i < Count.x; i++)
            {
                for (int j = 0; j < Count.y; j++)
                {
                    GameObject go;
                    if (transform.childCount > Count.x * j + i)
                        go = transform.GetChild(Count.x * j + i).gameObject;
                    else
                    {
                        var _ui = Instantiate(ui, transform);
                        go = _ui.gameObject;
                    }

                    go.transform.localPosition = new Vector3(Offset.x * i, 0.0f, Offset.y * j);
                    go.name = goName + $"({Count.x * j + i + 1})";
                }
            }

            for (int i = 0; i < Count.x * Count.y; i++)
            {
                var _ui = transform.GetChild(i).GetComponent<UIBehavior>();
                var _pos = transform.GetChild(i).position;
                var uiPos = new UIOriginPos { UI = _ui, OriginPos = _pos };
                Anchors.Add(uiPos);
            }
        }
#endif
    }
}
