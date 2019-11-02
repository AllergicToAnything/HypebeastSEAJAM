using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorStates {ShowCursor,HideCursor }
public class CursorHide : MonoBehaviour
{
    public CursorStates cursorStates = CursorStates.HideCursor;
    CursorLockMode CLM = CursorLockMode.Locked;

    // Update is called once per frame
    void Update()
    {

        if(cursorStates == CursorStates.HideCursor)
        {
            Cursor.visible = false;
            CLM = CursorLockMode.Locked;
        }
        else if ( cursorStates == CursorStates.ShowCursor)
        {
            Cursor.visible = true;
            CLM = CursorLockMode.Confined;
        }
    }
}
