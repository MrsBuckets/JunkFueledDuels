using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectUi : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public const string GAMEPIECE_TAG = "GamePiece";
    public const string MODEL_TAG = "GamePieceModel";
    private bool dragging = false;
    Vector3 originalPosition;
    Vector3 offset;
    Vector3 oldMousePosition;
    Vector3 dropPosittion;
    private Transform objectToDrag;
    private GameObject objectToDropOn;
    private GameObject clickedObject;
    List<RaycastResult> hitObjects = new List<RaycastResult>();
    GamePiece GamePieceScript;
    DragableUI dragableUI;
    bool readyToClick, mouseMoved;
    public bool gamePieceDragging = false;

    void Start()
    {
        oldMousePosition = Input.mousePosition;
    }

    void Update()
    { 
        if (readyToClick)
        {
            if (Input.mousePosition != oldMousePosition)
            {
                Debug.Log("MOUSE MOVED");
                readyToClick = false;
                mouseMoved = true;
                gamePieceDragging = true;
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (clickedObject != null)
            {
                Debug.Log(clickedObject.name);
                //if (clickedObject.tag == DRAGGABLE_TAG)
                //{
                //    objectToDrag = GetDraggableTransformUnderMouse();
                //    //Debug.Log(objectToDrag.name);
                //    if (objectToDrag != null)
                //    {
                //        dragableUI = objectToDrag.GetComponent<DragableUI>();
                //        if (!dragableUI.getGamePiece().isPinned())
                //        {
                //            offset = objectToDrag.position - Input.mousePosition;
                //            //Debug.Log(objectToDrag.name);
                //            dragging = true;
                //            objectToDrag.SetAsLastSibling();
                //            originalPosition = objectToDrag.position;
                //        }

                //    }
                //}
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                objectToDrag = GetDraggableTransformUnderMouse();
                if (objectToDrag != null)
                {
                    dragableUI = objectToDrag.GetComponent<DragableUI>();
                    if (!dragableUI.getGamePiece().isPinned())
                    {
                        offset = objectToDrag.position - Input.mousePosition;
                        //Debug.Log(objectToDrag.name);
                        dragging = true;
                        objectToDrag.SetAsLastSibling();
                        originalPosition = objectToDrag.position;
                    }

                }
                else if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform != null)
                    {
                        clickedObject = hit.transform.gameObject;
                        Debug.Log("clicked " + clickedObject.name);

                        if (clickedObject.tag == MODEL_TAG)
                        {
                            GamePieceScript = clickedObject.GetComponentInParent<GamePiece>();
                            Debug.Log(clickedObject.GetComponentInParent<Transform>().name);
                            //Debug.Log(GamePieceScript.transform.gameObject.name);
                            if (GamePieceScript != null)
                            {
                                Debug.Log("ReadyToClick");
                                readyToClick = true;
                                originalPosition = clickedObject.transform.position;
                                offset = originalPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                            }
                        }
                        if (clickedObject.tag == DRAGGABLE_TAG)
                        {
                            objectToDrag = GetDraggableTransformUnderMouse();
                            Debug.Log(objectToDrag.name);
                            if (objectToDrag != null)
                            {
                                dragableUI = objectToDrag.GetComponent<DragableUI>();
                                if (!dragableUI.getGamePiece().isPinned())
                                {
                                    offset = objectToDrag.position - Input.mousePosition;
                                    //Debug.Log(objectToDrag.name);
                                    dragging = true;
                                    objectToDrag.SetAsLastSibling();
                                    originalPosition = objectToDrag.position;
                                }

                            }
                        }
                    }
                }
            }
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            if (gamePieceDragging)
            {
                dropPosittion = clickedObject.transform.position;
                clickedObject.transform.position = originalPosition;

                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;

                var pointer = new PointerEventData(EventSystem.current);

                pointer.position = Input.mousePosition;

                EventSystem.current.RaycastAll(pointer, hitObjects);

                if (hitObjects.Count <= 0)
                {
                    //dropped on nothing
                }
                if (hitObjects.Count > 0)
                {
                    foreach (RaycastResult r in hitObjects)
                    {
                        if (r.gameObject.tag == MODEL_TAG)
                        {
                            //here


                        }
                    }
                }
                Debug.Log(hitObjects[0].gameObject.name);
                //objectToDropOn = GetObjectUnderMouse();  // todo clean up this mess!!!

            }
            if (readyToClick && !mouseMoved)
            {
                GamePieceScript.onClick();
            }
            readyToClick = false;
            mouseMoved = false;
            clickedObject = null;
            GamePieceScript = null;
            objectToDrag = null;
            dragableUI = null;
            gamePieceDragging = false;
        }
        if (dragging)
        {
            objectToDrag.position = (Input.mousePosition + offset);
        }
        if (gamePieceDragging)
        {
            clickedObject.transform.position = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) + offset);
        }

        oldMousePosition = Input.mousePosition;
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;
        Debug.Log(hitObjects[0].gameObject.name);
        return hitObjects[0].gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }
        return null;
    }
}
