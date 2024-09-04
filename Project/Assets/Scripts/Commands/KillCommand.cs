using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCommand : ICommand
{
    public KillCommand(GameObject target, params object[] parameters)
    {
        Object.Destroy(target);
    }
}
