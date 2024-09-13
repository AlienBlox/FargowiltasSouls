// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.GroundStickDR
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class GroundStickDR : AccessoryEffect
  {
    private static readonly int[] ElectricAttacks = new int[12]
    {
      100,
      83,
      84,
      259,
      435,
      436,
      437,
      438,
      449,
      576,
      578,
      682
    };

    public override Header ToggleHeader => (Header) Header.GetHeader<DubiousHeader>();

    public override int ToggleItemType => ModContent.ItemType<GroundStick>();

    public override float ProjectileDamageDR(
      Player player,
      Projectile projectile,
      ref Player.HurtModifiers modifiers)
    {
      float num = 0.0f;
      bool flag = false;
      player.FargoSouls();
      Projectile projectile1 = projectile;
      if (projectile1.ModProjectile == null)
      {
        if (projectile1.aiStyle == 79 || projectile1.aiStyle == 84 || projectile1.aiStyle == 88 || ((IEnumerable<int>) GroundStickDR.ElectricAttacks).Contains<int>(projectile1.type))
          flag = true;
      }
      else if (projectile1.ModProjectile is BaseDeathray)
      {
        flag = true;
      }
      else
      {
        string lower = ((ModType) projectile1.ModProjectile).Name.ToLower();
        if (lower.Contains("lightning") || lower.Contains("electr") || lower.Contains("thunder") || lower.Contains("laser"))
          flag = true;
      }
      if (flag && ((Entity) player).whoAmI == Main.myPlayer && !player.HasBuff(ModContent.BuffType<SuperchargedBuff>()))
      {
        num = 0.5f;
        player.AddBuff(ModContent.BuffType<SuperchargedBuff>(), 1800, true, false);
        foreach (Projectile projectile2 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p =>
        {
          if (!((Entity) p).active || !p.minion || p.owner != ((Entity) player).whoAmI)
            return false;
          return p.type == ModContent.ProjectileType<Probe1>() || p.type == ModContent.ProjectileType<Probe2>();
        })))
        {
          projectile2.ai[1] = 180f;
          projectile2.netUpdate = true;
        }
        SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 229, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 10; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        for (int index6 = 0; index6 < 30; ++index6)
        {
          int index7 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 229, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index7].noGravity = true;
          Dust dust3 = Main.dust[index7];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 21f);
          Main.dust[index7].noLight = true;
          int index8 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 229, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust4 = Main.dust[index8];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 12f);
          Main.dust[index8].noGravity = true;
          Main.dust[index8].noLight = true;
        }
        for (int index9 = 0; index9 < 20; ++index9)
        {
          int index10 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 229, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 2f, 5f));
          if (Utils.NextBool(Main.rand, 3))
            Main.dust[index10].noGravity = true;
          Dust dust = Main.dust[index10];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 12f, 18f));
          Main.dust[index10].position = ((Entity) player).Center;
        }
      }
      return num;
    }
  }
}
