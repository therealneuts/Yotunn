using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLayout : MonoBehaviour {

    public Transform[] slots;

    [SerializeField] float maxAngle = Mathf.PI / 2;
    [SerializeField] float radius = 5f;
    [SerializeField] float zSpacing = 0.02f;

    Vector3 middleCardPosition;
    Vector3 virtualCenter;
    int numChildren;

    public List<Transform> trimmedSlots = new List<Transform>();

    // Use this for initialization
    void Start () {
        foreach (Transform slot in slots)
        {
            if (slot.GetComponentInChildren<CardManager>() != null)
            {
                trimmedSlots.Add(slot);
            }
        }

        middleCardPosition = transform.position;
        numChildren = trimmedSlots.Count;

        virtualCenter = middleCardPosition - new Vector3(0f, radius, 0f);

        float adjustedPos;
        float angle;
        float radiansBetweenSegments = maxAngle / (numChildren - 1);

        for (int i = 0; i < numChildren; i++)
        {
            adjustedPos = i - (numChildren / 2f);
            angle = (Mathf.PI / 2) + adjustedPos * radiansBetweenSegments;

            float xOffset = Mathf.Cos(angle);
            float yOffset = Mathf.Sin(angle);

            trimmedSlots[i].position = new Vector3(virtualCenter.x + radius * xOffset, virtualCenter.y + radius * yOffset, i * zSpacing);
            trimmedSlots[i].rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Rad2Deg * angle - 90f));
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
