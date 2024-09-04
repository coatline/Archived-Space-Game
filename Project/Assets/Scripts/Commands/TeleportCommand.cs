using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCommand : ICommand
{
    public TeleportCommand(GameObject target, params object[] paramaters)
    {
        target.transform.position = new Vector3((float)paramaters[0], (float)paramaters[1], (float)paramaters[2]);
    }
}
