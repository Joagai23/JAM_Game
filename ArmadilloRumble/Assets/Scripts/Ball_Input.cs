using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Input : MonoBehaviour
{
    public float lineLenght = 1.0f;
    public int throwLimit = 2;

    private Vector2 startPoint;
    private Vector2 finishPoint;
    private Vector2 direction;

    private float screenWidth;
    private float screenHeight;

    private int numThrows = 0;

    private Camera camera;
    private LineRenderer lineRenderer;

    public Ball_Movement ballMovement;

    // Setter - Getter
    public int NumThrows
    {
        set { numThrows = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse pressed for the first time
        if (Input.GetMouseButtonDown(0))
        {
            // Normalize mouse position to screen point
            float x = Input.mousePosition.x / screenWidth;
            float y = Input.mousePosition.y / screenHeight;

            startPoint = new Vector2(x, y);
        }

        if(Input.GetMouseButton(0))
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ballMovement.transform.position);

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPos = new Vector3(hit.point.x, ballMovement.transform.position.y, hit.point.z);

                Vector3 offset = ballMovement.transform.position - newPos;
                Vector3 linePoint = new Vector3(lineRenderer.GetPosition(0).x + offset.x, ballMovement.transform.position.y, lineRenderer.GetPosition(0).z + offset.z);

                Vector3 pointDif = linePoint - lineRenderer.GetPosition(0);

                if (pointDif.magnitude > lineLenght)
                {
                    float angle = Mathf.Atan2(pointDif.z, pointDif.x);
                    float x = lineLenght * Mathf.Cos(angle);
                    float y = lineLenght * Mathf.Sin(angle);
                    
                    linePoint = new Vector3(lineRenderer.GetPosition(0).x + x, ballMovement.transform.position.y, lineRenderer.GetPosition(0).z + y);
                }

                lineRenderer.SetPosition(1, linePoint);
            }
        }

        // Mouse unpressed
        if (Input.GetMouseButtonUp(0))
        {
            // Normalize mouse position to screen point
            float x = Input.mousePosition.x / screenWidth;
            float y = Input.mousePosition.y / screenHeight;

            finishPoint = new Vector2(x, y);
            lineRenderer.SetPosition(1, finishPoint);

            direction = finishPoint - startPoint;
            direction.Normalize();

            if(numThrows < throwLimit)
            {
                ballMovement.Shoot(direction, camera.transform.position);
                numThrows++;
            }

            //Clean all line render points
            lineRenderer.positionCount = 0;
        }
    }
}
