// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.Clown
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class Clown : EModeNPCBehaviour
  {
    public int FuseTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(109);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax *= 2;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      float num1 = (float) this.FuseTimer / 1500f;
      int index1 = Dust.NewDust(((Entity) npc).Top, 0, 0, 6, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 1f);
      Main.dust[index1].velocity.Y -= (float) (0.5 + 2.5 * (double) num1);
      Dust dust = Main.dust[index1];
      dust.velocity = Vector2.op_Multiply(dust.velocity, (float) (1.0 + 3.0 * (double) num1));
      Main.dust[index1].scale = (float) (0.5 + 5.5 * (double) num1);
      if (Utils.NextBool(Main.rand, 4))
      {
        Main.dust[index1].scale += 0.5f;
        Main.dust[index1].noGravity = true;
      }
      if (++this.FuseTimer < 1500)
        return;
      npc.life = 0;
      npc.HitEffect(0, 10.0, new bool?());
      if (npc.DeathSound.HasValue)
      {
        SoundStyle soundStyle = npc.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }
      ((Entity) npc).active = false;
      if (!FargoSoulsUtil.HostCheck)
        return;
      if (Luminance.Common.Utilities.Utilities.AnyBosses())
      {
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 517, 60, 8f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        for (int index2 = 0; index2 < 30; ++index2)
        {
          int index3 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), (float) (Main.rand.Next(-1000, 1001) / 100), (float) (Main.rand.Next(-2000, 101) / 100), ModContent.ProjectileType<ClownBomb>(), 100, 8f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Main.projectile[index3].timeLeft -= Main.rand.Next(120);
        }
        for (int index4 = 0; index4 < 30; ++index4)
        {
          int num2 = 30;
          int num3 = 250;
          float num4 = 8f;
          switch (Main.rand.Next(10))
          {
            case 0:
            case 1:
            case 2:
              num2 = 75;
              break;
            case 3:
            case 4:
            case 5:
            case 6:
              num2 = 517;
              break;
            case 7:
            case 8:
            case 9:
              num2 = 397;
              break;
          }
          int index5 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height), (float) (Main.rand.Next(-1000, 1001) / 100), (float) (Main.rand.Next(-2000, 101) / 100), num2, num3, num4, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Main.projectile[index5].timeLeft += Main.rand.Next(-120, 120);
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<FusedBuff>(), 1800, true, false);
      target.AddBuff(ModContent.BuffType<UnluckyBuff>(), 1800, true, false);
    }
  }
}
