// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.JungleMimic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class JungleMimic : BiomeMimics
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(476);

    public override void AI(NPC npc)
    {
      if (this.DoStompAttack)
      {
        if ((double) ((Entity) npc).velocity.Y == 0.0)
        {
          for (int index = 0; index < 5; ++index)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), (float) Main.rand.Next(-30, 31) * 0.1f, (float) Main.rand.Next(-40, -15) * 0.1f, ModContent.ProjectileType<FakeHeart>(), 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          for (int index1 = 0; index1 < 30; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
          }
          for (int index3 = 0; index3 < 20; ++index3)
          {
            int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
            Main.dust[index4].noGravity = true;
            Dust dust1 = Main.dust[index4];
            dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
            int index5 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust2 = Main.dust[index5];
            dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
          }
          float num = 0.5f;
          for (int index6 = 0; index6 < 4; ++index6)
          {
            int index7 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
            Gore gore = Main.gore[index7];
            gore.velocity = Vector2.op_Multiply(gore.velocity, num);
            ++Main.gore[index7].velocity.X;
            ++Main.gore[index7].velocity.Y;
          }
        }
      }
      else if ((double) ((Entity) npc).velocity.Y > 0.0)
        this.DoStompAttack = true;
      if (this.CanDoAttack && npc.HasValidTarget)
      {
        if (this.IndividualAttackTimer > 10)
        {
          this.IndividualAttackTimer = 0;
          SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          float num = Main.player[npc.target].ZoneRockLayerHeight ? 9f : 14f;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Multiply(num, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), (double) MathHelper.ToRadians(5f)));
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<JungleTentacle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
          }
        }
        if (Main.player[npc.target].ZoneRockLayerHeight)
          ++this.AttackCycleTimer;
      }
      base.AI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 360, true, false);
    }
  }
}
