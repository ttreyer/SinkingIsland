using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayComponent : PolicyComponent {
    public int turns;
    public bool repeat;
    public PolicyController policyControllerPrefab;

    public override void Execute(PolicyController controller) {
        DelayComponent dc = this;
        GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<TurnController>()
            .DelayAction(turns, () => {
                PolicyController pc = Instantiate(policyControllerPrefab);
                pc.island = controller.island;
                pc.Execute(false);
                Destroy(pc.gameObject);

                if (repeat)
                    dc.Execute(controller);
            });
    }
}
