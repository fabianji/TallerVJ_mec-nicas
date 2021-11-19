using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor00 : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture2D cursorArrow;
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorArrow,Vector2.zero,CursorMode.ForceSoftware);
    }


}
