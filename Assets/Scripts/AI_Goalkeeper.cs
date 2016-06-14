using UnityEngine;
using System.Collections;

public class AIGoalkeeper {

    // ===================================================
    // AI player thinking procedures
    // ===================================================
    public static void ProcessAI(ref PARAM param,ref AI_PLAYER goalkeeper) {
        float r = Physics.Angle(param.x,param.y, goalkeeper.puck.x, goalkeeper.puck.y);
        float d = Physics.Distance(param.x, param.y, goalkeeper.puck.x,goalkeeper.puck.y);
        param.dir = r;
        param.h = param.dir;

        param.rh += (0.3f * Random.value) - (0.1f * Random.value);
    }
}
