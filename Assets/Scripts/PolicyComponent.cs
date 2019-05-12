using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyComponent : MonoBehaviour {
    public virtual void Execute(PolicyController controller) {
        throw new System.Exception("Missing Execute() implementation :(");
    }
}
