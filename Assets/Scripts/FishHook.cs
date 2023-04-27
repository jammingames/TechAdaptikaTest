using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHook : MonoBehaviour
{
    bool isDropping = false;
    bool canDrop = true;
    public float dropDuration = 5;
    public float currentDropTime = 0f;

    public Transform dropPositionTarget;
    Vector3 originalPos;
    Vector3 targetPos;

	private void Awake()
	{
		originalPos = transform.position;
        targetPos = dropPositionTarget.position;
	}
	public void DropHook()
    {
        isDropping = true;
        canDrop = false;
        currentDropTime = 0f;
    }

    void DoDropUpdate()
    {
        transform.position = Vector3.Lerp(originalPos, targetPos, Mathf.InverseLerp(0, dropDuration, currentDropTime));
		currentDropTime += Time.deltaTime;
		if (currentDropTime >= dropDuration)
		{
			ResetHook();
		}
	}

    void ResetHook()
    {
        transform.position = originalPos;
        canDrop = true;
        isDropping = false;
        currentDropTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDropping) return;
        DoDropUpdate();
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<ICatchable>() != null) 
        {
            other.GetComponent<ICatchable>().GetCaught();
            ResetHook();
        }
	}
}
