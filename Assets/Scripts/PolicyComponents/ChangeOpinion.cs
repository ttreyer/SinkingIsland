﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOpinion : PolicyComponent {
    public int opinionChange;

    public override void Execute(PolicyController controller) {
        controller.island
            .GetComponentInChildren<OpinionController>()
            .opinion += opinionChange;
    }
}
