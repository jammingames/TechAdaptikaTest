using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FishHook hook;
    [SerializeField] private float hookDropTime = 5.0f;
    bool isDropping = false;
    public void DropHook()
    {
        isDropping = true;
    }

	private void Update()
	{
        if (GameManager.instance.currentState != GameState.Game) return;
	    if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            print("DROPPING HOOK!!!");
            hook.DropHook();
        }
	}

}
