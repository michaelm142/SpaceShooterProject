using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualThumbstick : VirtualInputAxis, IPointerUpHandler, IPointerDownHandler
{
    public Transform knob;

    private bool pointerDown;

    private RectTransform rect;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector2.zero;
        pointerDown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pointerDown)
        {
            knob.GetComponent<Image>().color = Color.blue;

#if UNITY_STANDALONE || UNITY_EDITOR
            Vector3 point = Vector3.ClampMagnitude((Input.mousePosition - transform.position) / new Vector2(rect.rect.width * 0.5f, rect.rect.height * 0.5f), 1.0f);
            Value = point;
#endif
#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                // find nearest touch
                List<Touch> touches = Input.touches.ToList();
                sortPosition = transform.position;
                touches.Sort(touchSort);
                Vector3 touchPoint = (Vector3)touches[0].position - transform.position;
                // get normalized vector to touch position
                Value = Vector3.ClampMagnitude(touchPoint / new Vector2(rect.rect.width * 0.5f, rect.rect.height * 0.5f), 1.0f);
            }
#endif
        }
        else knob.GetComponent<Image>().color = Color.white;

        if (knob != null)
            knob.transform.position = (Vector2)transform.position + Value * new Vector2(rect.rect.width * 0.5f, rect.rect.height * 0.5f);
    }

    static Vector3 sortPosition;
    static int touchSort(Touch a, Touch b)
    {
        if (Vector3.Distance(a.position, sortPosition) > Vector3.Distance(b.position, sortPosition))
            return 1;
        return 0;
    }
}
