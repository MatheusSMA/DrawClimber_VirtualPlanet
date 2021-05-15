using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool startDrawning;
    GameObject lineGo;
    Vector3 mousePos;
    LineRenderer LR;
    [SerializeField]
    Material Linemat;
    int currentIndex=0;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform colliderPrefab;
    Transform lastinstantiate_Collider;
    public Transform eixoRoda;
    public Transform limit_top_left;
    public Transform limit_bottom_right;
    public Canvas canvasScenario;

    void Start()
    {
        lineGo = new GameObject();
    }
    void FixedUpdate()
    {
        if (startDrawning == true)
        {
            Vector3 Dist = mousePos - Input.mousePosition;
            float Distance_sqrmag = Dist.sqrMagnitude;                      
            if (Dist.sqrMagnitude > 500f && mousePos.x > RectTransformUtility.PixelAdjustPoint(limit_top_left.position, limit_top_left, canvasScenario).x && mousePos.x < RectTransformUtility.PixelAdjustPoint(limit_bottom_right.position, limit_bottom_right, canvasScenario).x && mousePos.y < RectTransformUtility.PixelAdjustPoint(limit_top_left.position, limit_top_left, canvasScenario).y && mousePos.y > RectTransformUtility.PixelAdjustPoint(limit_bottom_right.position, limit_bottom_right, canvasScenario).y)
            {
                LR.positionCount = currentIndex + 1;                
                LR.SetPosition(currentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Input.mousePosition.z + 10f)));
                #region instanciar o 3d
                if (lastinstantiate_Collider != null)
                {
                    Vector3 Currentlinepos = LR.GetPosition(currentIndex);
                    lastinstantiate_Collider.LookAt(Currentlinepos);
                }
                lastinstantiate_Collider = Instantiate(colliderPrefab,new Vector3(LR.GetPosition(currentIndex).x, LR.GetPosition(currentIndex).y, -44.4f), Quaternion.identity, lineGo.transform);
               lineGo.transform.SetParent(eixoRoda);
                #endregion
                mousePos = Input.mousePosition;
                currentIndex++;               
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startDrawning = true;
        mousePos = Input.mousePosition;
        LR = lineGo.AddComponent<LineRenderer>();
        LR.startWidth = 0.2f;
        LR.material = Linemat;

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //lineGo.AddComponent<Rigidbody>(); 
        LR.useWorldSpace = false;
        startDrawning = false;
        Start();
        currentIndex = 0;
        //Destroy(LR.gameObject); 

    }

}
