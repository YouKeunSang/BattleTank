using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : UIControlBase
{
    public override void NormalClick(float x, float y)
    {
        base.NormalClick(x, y);
        BattleMgr.instance.GetPlayer().Move(new Vector2(x, y), false);
    }
    public override void SkillClick(float x, float y)
    {
        base.SkillClick(x, y);
        BattleMgr.instance.GetPlayer().Move(new Vector2(x, y), true);
    }
}
