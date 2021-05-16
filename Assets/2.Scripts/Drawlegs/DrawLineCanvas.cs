using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLineCanvas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    #region Draw variables
    bool startDrawing;
    int currentIndex;
    #endregion

    #region Line variables
    Vector3 mousePos;
    GameObject lineGO;
    LineRenderer lR;
    public Material lineMat;
    #endregion

    #region 3D instantiate variables
    public GameObject body;
    public Camera cam;
    public Transform collider_Prefab;
    Transform lastInstantiated_Collider;
    public Transform legPosition;
    #endregion
     
    //lr = LineRenderer//
    //lineGO = Line Gameobject//
    //lineMat = Material of line//

    void Start()
    {
        lineGO = new GameObject();
    }

    void FixedUpdate()
    {
        if (startDrawing)
        {
            Vector3 Dist = mousePos - Input.mousePosition;
            float Distance_SqrMag = Dist.sqrMagnitude;

            if (Distance_SqrMag > 1000f)
            {
                // Set the position for the line
                lR.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Round(Input.mousePosition.z) + 10f)));

                if (lastInstantiated_Collider != null)
                {
                    Vector3 CurLinePos = lR.GetPosition(currentIndex);
                    lastInstantiated_Collider.gameObject.SetActive(true);
                    lastInstantiated_Collider.LookAt(CurLinePos);

                    if (lastInstantiated_Collider.rotation.y == 0)
                    {
                        lastInstantiated_Collider.eulerAngles = new Vector3(lastInstantiated_Collider.rotation.eulerAngles.x, 90, lastInstantiated_Collider.rotation.eulerAngles.z);
                    }

                    lastInstantiated_Collider.localScale = new Vector3(lastInstantiated_Collider.localScale.x, lastInstantiated_Collider.localScale.y, Vector3.Distance(lastInstantiated_Collider.position, CurLinePos) * 1.1f);
                }

                //Put the pivot on the line start, and instantiate new 3D forms for every new mouse position
                lineGO.transform.position = lR.GetPosition(0);
                lastInstantiated_Collider = Instantiate(collider_Prefab, lR.GetPosition(currentIndex), Quaternion.identity, lineGO.transform);
                lastInstantiated_Collider.gameObject.SetActive(false);
                mousePos = Input.mousePosition;
                currentIndex++;
                lR.positionCount = currentIndex + 1;
                lR.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Starts drawning and get the mouse position and add the "Line Renderer" to gameobject
        startDrawing = true;
        mousePos = Input.mousePosition;
        lR = lineGO.AddComponent<LineRenderer>();
        lR.startWidth = 0.2f;
        lR.material = lineMat;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Stop drawning and conect the draw to player
        startDrawing = false;
        lR.useWorldSpace = false;
        lR.enabled = false;
        lineGO.transform.position = legPosition.position;
        lineGO.transform.SetParent(legPosition);
        Start();
        currentIndex = 0;
    }
}