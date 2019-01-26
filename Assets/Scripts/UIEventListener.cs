using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// <para>UGUI事件工具</para>
/// <para>使用方法：</para>
/// <para>UIEventListener.Get(gameObject).onPointerClick += (go) =>{Debug.Log(go.name);};</para>
/// <para>注意：在添加的事件的时候必须使用 += 操作符,主相机必须挂上Physics Raycaster组件</para>
/// <para>Created by: yumingzhou Date: 2017/07/21</para>
/// </summary>
public class UIEventListener : EventTrigger
{
    public delegate void BaseDelegate(GameObject go, BaseEventData data);
    public delegate void PointerDelegate(GameObject go, PointerEventData data);
    public delegate void AxisDelegate(GameObject go, AxisEventData data);

    public event BaseDelegate onBeginDrag;
    public event BaseDelegate onCancel;
    public event BaseDelegate onDeselect;
    public event PointerDelegate onDrag;
    public event PointerDelegate onDrop;
    public event PointerDelegate onEndDrag;
    public event PointerDelegate onInitializePotentialDrag;
    public event AxisDelegate onMove;
    public event PointerDelegate onPointerClick;
    public event PointerDelegate onPointerDown;
    public event PointerDelegate onPointerEnter;
    public event PointerDelegate onPointerExit;
    public event PointerDelegate onPointerUp;
    public event PointerDelegate onScroll;
    public event BaseDelegate onSelect;
    public event BaseDelegate onSubmit;
    public event BaseDelegate onUpdateSelected;

    public static UIEventListener Get(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIEventListener>();
        }
        return listener;
    }

    public static void RemoveAllListeners(GameObject go)
    {
        UIEventListener listener = go.GetComponent<UIEventListener>();
        if (listener == null)
        {
            Destroy(listener);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag(gameObject, eventData);
        }
    }

    public override void OnCancel(BaseEventData eventData)
    {
        if (onCancel != null)
        {
            onCancel(gameObject, eventData);
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if (onDeselect != null)
        {
            onDeselect(gameObject, eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(gameObject, eventData);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null)
        {
            onDrop(gameObject, eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
        {
            onEndDrag(gameObject, eventData);
        }
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (onInitializePotentialDrag != null)
        {
            onInitializePotentialDrag(gameObject, eventData);
        }
    }

    public override void OnMove(AxisEventData eventData)
    {
        if (onMove != null)
        {
            onMove(gameObject, eventData);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onPointerClick != null)
        {
            onPointerClick(gameObject, eventData);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
        {
            onPointerDown(gameObject, eventData);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null)
        {
            onPointerEnter(gameObject, eventData);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null)
        {
            onPointerExit(gameObject, eventData);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null)
        {
            onPointerUp(gameObject, eventData);
        }
    }

    public override void OnScroll(PointerEventData eventData)
    {
        if (onScroll != null)
        {
            onScroll(gameObject, eventData);
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
        {
            onSelect(gameObject, eventData);
        }
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (onSubmit != null)
        {
            onSubmit(gameObject, eventData);
        }
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelected != null)
        {
            onUpdateSelected(gameObject, eventData);
        }
    }
}
