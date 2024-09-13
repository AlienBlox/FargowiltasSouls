// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.HallowMimic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class HallowMimic : BiomeMimics
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(475);

    public override void AI(NPC npc)
    {
      if (this.CanDoAttack && npc.HasValidTarget)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 4f));
        if (this.IndividualAttackTimer == 10 && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(9f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)), 5, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        if (this.IndividualAttackTimer % 10 == 0 && !Main.player[npc.target].ZoneRockLayerHeight)
        {
          SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector(((Entity) npc).Center.X + Utils.NextFloat(Main.rand, -100f, 100f), ((Entity) Main.player[npc.target]).Center.Y - (float) Main.rand.Next(600, 801));
          Vector2 vector2_2 = Vector2.op_Multiply(10f, Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.NextVector2Square(Main.rand, -100f, 100f)), vector2_1)));
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_2, 5, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          for (int index1 = 0; index1 < 40; ++index1)
          {
            int num = Utils.Next<int>(Main.rand, new int[3]
            {
              15,
              57,
              58
            });
            int index2 = Dust.NewDust(((Entity) npc).Center, 0, 0, num, vector2_2.X / 2f, (float) (-(double) vector2_2.Y / 2.0), 100, new Color(), 1.2f);
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
            Main.dust[index2].noGravity = Utils.NextBool(Main.rand);
          }
        }
        if (this.IndividualAttackTimer > 30)
          this.IndividualAttackTimer = 0;
      }
      base.AI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(31, 180, true, false);
    }
  }
}
