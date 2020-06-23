using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteAlways]
public class ShockPlatform : MonoBehaviour
{

    public bool isActive;

    public bool doesRotate;
    public float rotationSpeed;

    public bool doesTranslate;
    public float translationSpeed;
    public Transform[] translationPoints;

    private int iterator;
    private bool forward;

    private void Awake()
    {
        iterator = 0;
        forward = true;
        isActive = false;
        SetEmissive();
    }

    private void SetEmissive()
    {
        if (isActive)
        {
            GetComponentInChildren<Renderer>().material.SetColor("_EmissiveColor", new Color(0, 1f / 255f, 1f / 255f));
            transform.Find("Gear_Crystals_LP").GetComponentInChildren<Renderer>().material.SetColor("_EmissiveColor", new Color(0, 1f / 255f, 1f / 255f));
        }
        else
        {
            GetComponentInChildren<Renderer>().material.SetColor("_EmissiveColor", new Color(0,0,0));
            transform.Find("Gear_Crystals_LP").GetComponentInChildren<Renderer>().material.SetColor("_EmissiveColor", new Color(0, 0, 0));
        }
    }


    public void Activate()
    {
        if (!isActive)
        {
            foreach (ShockPlatform s in FindObjectsOfType<ShockPlatform>())
            {
                if (s != this)
                {
                    if (s.isActive)
                    {
                        s.isActive = false;
                        break;
                    }
                }
            }
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        SetEmissive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            if (isActive)
            {
                if (doesRotate)
                {
                    transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                }
                if (doesTranslate)
                {
                    if (forward) //if moving forward
                    {
                        Debug.Log(iterator);
                        if (transform.position == translationPoints[iterator + 1].position) //if you have reached the next position
                        {
                            if (translationPoints.Length == iterator + 2) //if you are at the end of the list
                            {
                                forward = false;
                            }
                            iterator++;
                        }
                        else //move
                        {
                            transform.position = Vector3.MoveTowards(transform.position, translationPoints[iterator + 1].position, translationSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        if (transform.position == translationPoints[iterator - 1].position) //if you have reached the next position
                        {
                            if (0 == iterator - 1) //if you are at the end of the list
                            {
                                forward = true;
                            }
                            iterator--;
                        }
                        else //move
                        {
                            transform.position = Vector3.MoveTowards(transform.position, translationPoints[iterator - 1].position, translationSpeed * Time.deltaTime);
                        }

                    }
                }
            }
            else
            {
                SetEmissive();
            }
        }
        else if(doesTranslate)
        {
            for (int i = 0; i < translationPoints.Length; i++)
            {
                if (i + 1 != translationPoints.Length)
                {
                    Debug.DrawLine(translationPoints[i].position, translationPoints[i + 1].position);
                }
            }
        }
    }


    private Vector3 position;
    private Quaternion rotation;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null, true);
        }

    }
}

