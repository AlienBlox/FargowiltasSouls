// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.AngryBones
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class AngryBones : EModeNPCBehaviour
  {
    public int BabyTimer;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(31, 294, 296, 295);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.justHit)
        this.BabyTimer += 20;
      if (++this.BabyTimer <= 300)
        return;
      this.BabyTimer = 0;
      if (!FargoSoulsUtil.HostCheck || !npc.HasValidTarget || !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
        return;
      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), ModContent.ProjectileType<SkeletronGuardian2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (!FargoSoulsUtil.HostCheck)
        return;
      if (Utils.NextBool(Main.rand, 5))
        FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 34, velocity: new Vector2());
      for (int index = 0; index < 15; ++index)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) Main.rand.Next(-50, 51), (float) Main.rand.Next(-100, 1));
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, Utils.NextFloat(Main.rand, 3f, 6f));
        vector2.Y -= Math.Abs(vector2.X) * 0.2f;
        vector2.Y -= 3f;
        vector2.Y *= Utils.NextFloat(Main.rand, 1.5f);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 471, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
