// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow.Unicorn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow
{
  public class Unicorn : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(86);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) Math.Abs(((Entity) npc).velocity.X) < 3.0 || ++this.Counter < 3)
        return;
      this.Counter = 0;
      int num = (double) ((Entity) npc).velocity.X > 0.0 ? 1 : -1;
      int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), new Vector2(((Entity) npc).Center.X - (float) (num * (((Entity) npc).width / 2)), ((Entity) npc).Center.Y), ((Entity) npc).velocity, 251, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 1f, -1, 0.0f, 0.0f, 0.0f);
      if (index == Main.maxProjectiles)
        return;
      Main.projectile[index].friendly = false;
      Main.projectile[index].hostile = true;
      Main.projectile[index].FargoSouls().Rainbow = true;
    }
  }
}
