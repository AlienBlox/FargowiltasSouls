// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.CorruptMimic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class CorruptMimic : BiomeMimics
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(473);

    public override void AI(NPC npc)
    {
      if (this.CanDoAttack && this.IndividualAttackTimer > 90)
      {
        this.IndividualAttackTimer = 0;
        if (npc.HasValidTarget)
        {
          float num = 16f * Utils.NextFloat(Main.rand, 5f, 35f);
          for (int index = -1; index <= 1; index += 2)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Bottom, new Vector2((float) index * num, -108f));
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.UnitY, ModContent.ProjectileType<ClingerFlame>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      base.AI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(39, 180, true, false);
    }
  }
}
