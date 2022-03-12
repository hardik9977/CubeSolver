using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBigCube : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 firstPressPos;
    Vector2 secPressPos;

    Vector2 currrentSwipe;

    float speed = 100;

    Vector3 previuosMousePosition;
    Vector2 mouseDelta;
    [SerializeField] GameObject target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - previuosMousePosition;
            mouseDelta *= 0.1f;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else 
        {
            if (transform.rotation != target.transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, 100 * Time.deltaTime);
            }
        }
            previuosMousePosition = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Debug.Log(firstPressPos);
        }

        if (Input.GetMouseButtonUp(1))
        {
            secPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currrentSwipe = new Vector2(secPressPos.x - firstPressPos.x, secPressPos.y - firstPressPos.y);
            currrentSwipe.Normalize();
            if (LeftSwipe(currrentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currrentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (RightupSwipe(currrentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (RightDownSwipe(currrentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (LeftupSwipe(currrentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (LeftDownSwipe(currrentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.x < 0 & currentSwipe.y > -0.5 & currentSwipe.y < 0.5;
    }

    bool RightSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.x > 0 & currentSwipe.y > -0.5 & currentSwipe.y < 0.5;
    }

    bool RightupSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.y > 0 & currentSwipe.x < 0;
    }

    bool RightDownSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.y < 0 & currentSwipe.x > 0;
    }

    bool LeftupSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.y > 0 & currentSwipe.x > 0;
    }

    bool LeftDownSwipe(Vector2 currentSwipe)
    {
        return currentSwipe.y < 0 & currentSwipe.x < 0;
    }
}
