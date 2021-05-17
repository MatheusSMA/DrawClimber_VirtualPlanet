using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLineCanvas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{

    #region Draw variables
    bool startDrawing;
    int currentIndex;
    #endregion

    #region Line variables
    Vector3 mousePos;
    GameObject lineGO;
    GameObject lineGO2;
    LineRenderer lR;
    public Material lineMat;
    #endregion

    #region 3D instantiate variables
    public GameObject body;
    public Camera cam;
    public Transform collider_Prefab;
    Transform lastInstantiated_Collider;
    Transform lastInstantiated_Collider2;
    public Transform legPosition;
    public Transform legPosition2;
    public Mesh cubeShapeColliderDraw;
    #endregion


    void Start()
    {
        lineGO = new GameObject();
        lineGO2 = new GameObject();
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

                if (lastInstantiated_Collider != null)
                {
                    Vector3 CurLinePos = lR.GetPosition(currentIndex);
                    lastInstantiated_Collider2.gameObject.SetActive(true);
                    lastInstantiated_Collider2.LookAt(CurLinePos);

                    if (lastInstantiated_Collider2.rotation.y == 0)
                    {
                        lastInstantiated_Collider2.eulerAngles = new Vector3(lastInstantiated_Collider2.rotation.eulerAngles.x, 90, lastInstantiated_Collider2.rotation.eulerAngles.z);
                    }

                    lastInstantiated_Collider2.localScale = new Vector3(lastInstantiated_Collider2.localScale.x, lastInstantiated_Collider2.localScale.y, Vector3.Distance(lastInstantiated_Collider2.position, CurLinePos) * 1.1f);
                }

                //Put the pivot on the line start, and instantiate new 3D forms for every new mouse position
                lineGO2.transform.position = lR.GetPosition(0);
                lineGO.transform.position = lR.GetPosition(0);

                lastInstantiated_Collider = Instantiate(collider_Prefab, lR.GetPosition(currentIndex), Quaternion.identity, lineGO.transform);
                lastInstantiated_Collider2 = Instantiate(collider_Prefab, lR.GetPosition(currentIndex), Quaternion.identity, lineGO2.transform);
                lastInstantiated_Collider.localScale = new Vector3(0.2f, 0.2f, lastInstantiated_Collider.localScale.z);
                lastInstantiated_Collider.gameObject.SetActive(false);
                lastInstantiated_Collider2.gameObject.SetActive(false);

                lastInstantiated_Collider.gameObject.AddComponent<MeshCollider>().convex = true;
                lastInstantiated_Collider.gameObject.GetComponent<MeshCollider>().sharedMesh = cubeShapeColliderDraw;

                lastInstantiated_Collider2.gameObject.AddComponent<MeshCollider>().convex = true;
                lastInstantiated_Collider2.gameObject.GetComponent<MeshCollider>().sharedMesh = cubeShapeColliderDraw;
                lastInstantiated_Collider2.localScale = new Vector3(0.2f, 0.2f, lastInstantiated_Collider.localScale.z);


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
        if (legPosition.childCount != 0)
        {

        Destroy(legPosition.GetChild(0).gameObject);

        Destroy(legPosition2.GetChild(0).gameObject);
        }

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
        lineGO2.transform.position = legPosition2.position;
        lineGO2.transform.SetParent(legPosition2);
        lineGO2.transform.rotation = Quaternion.Euler(0, 0f, 180f);



        Start();
        currentIndex = 0;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // List<GameObject> listDetroy = new List<GameObject>();

            
    }


}