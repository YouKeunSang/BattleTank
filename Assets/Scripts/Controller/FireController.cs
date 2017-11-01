using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : UIControlBase
{
    public override void NormalClick(float x, float y)
    {
        base.NormalClick(x, y);
        BattleMgr.instance.GetPlayer().Attack(new Vector2(x, y), false);
    }
    public override void SkillClick(float x, float y)
    {
        base.SkillClick(x, y);
        BattleMgr.instance.GetPlayer().Attack(new Vector2(x, y), true);
    }
}
