using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    public MoveCommand(GameObject target, params object[] paramaters)
    {
        target.transform.Translate(new Vector3((float)paramaters[0], (float)paramaters[1], (float)paramaters[2]));
    }
}
