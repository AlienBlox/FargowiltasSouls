// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ShadewoodEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ShadewoodEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<ShadewoodEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      bool flag = fargoSoulsPlayer.ForceEffect<ShadewoodEnchant>();
      int num1 = flag ? 400 : 200;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage)
        {
          Vector2 vector2 = FargoSoulsUtil.ClosestPointInHitbox((Entity) npc, ((Entity) player).Center);
          if ((double) ((Entity) player).Distance(vector2) < (double) num1 && (flag || Collision.CanHitLine(((Entity) player).Center, 0, 0, vector2, 0, 0)))
            npc.AddBuff(ModContent.BuffType<SuperBleedBuff>(), 120, false);
        }
      }
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2_1 = new Vector2();
        double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_1.X += (float) Math.Sin(num2) * (float) num1;
        vector2_1.Y += (float) Math.Cos(num2) * (float) num1;
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_1), new Vector2(4f, 4f));
        if (flag || Collision.CanHitLine(((Entity) player).Left, 0, 0, vector2_2, 0, 0) || Collision.CanHitLine(((Entity) player).Right, 0, 0, vector2_2, 0, 0))
        {
          Dust dust1 = Main.dust[Dust.NewDust(vector2_2, 0, 0, 5, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.velocity = ((Entity) player).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_1), -5f));
          }
          dust1.noGravity = true;
        }
      }
      if (fargoSoulsPlayer.ShadewoodCD <= 0)
        return;
      --fargoSoulsPlayer.ShadewoodCD;
    }

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      ShadewoodEffect.ShadewoodProc(player, target, projectile);
    }

    public static void ShadewoodProc(Player player, NPC target, Projectile projectile)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool flag = fargoSoulsPlayer.ForceEffect<ShadewoodEnchant>();
      int dmg = 12;
      if (flag)
        dmg *= 3;
      if (!target.HasBuff(ModContent.BuffType<SuperBleedBuff>()) || fargoSoulsPlayer.ShadewoodCD != 0 || projectile != null && projectile.type == ModContent.ProjectileType<SuperBlood>() || ((Entity) player).whoAmI != Main.myPlayer)
        return;
      for (int index = 0; index < Main.rand.Next(3, 6); ++index)
        Projectile.NewProjectile(((Entity) player).GetSource_Misc(""), ((Entity) target).Center.X, ((Entity) target).Center.Y - 20f, 0.0f + Utils.NextFloat(Main.rand, -5f, 5f), Utils.NextFloat(Main.rand, -5f, 5f), ModContent.ProjectileType<SuperBlood>(), FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (!flag)
        return;
      target.AddBuff(69, 30, false);
    }
  }
}
