// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.SpectreEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class SpectreEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<SpiritHeader>();

    public override int ToggleItemType => ModContent.ItemType<SpectreEnchant>();

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (target.immortal || fargoSoulsPlayer.SpectreCD > 0 || !Utils.NextBool(Main.rand))
        return;
      bool flag = fargoSoulsPlayer.ForceEffect<SpectreEnchant>();
      if (projectile == null)
      {
        float num1 = (float) Main.rand.Next(-100, 101);
        float num2 = (float) Main.rand.Next(-100, 101);
        float num3 = (float) (4.0 / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
        float num4 = num1 * num3;
        float num5 = num2 * num3;
        Projectile proj = FargoSoulsUtil.NewProjectileDirectSafe(this.GetSource_EffectItem(player), ((Entity) target).position, new Vector2(num4, num5), 356, ((NPC.HitInfo) ref hitInfo).Damage / 2, 0.0f, ((Entity) player).whoAmI, (float) ((Entity) target).whoAmI);
        if (!flag && (!hitInfo.Crit || !Utils.NextBool(Main.rand, 5)) || proj == null)
          return;
        this.SpectreHeal(player, target, proj);
        fargoSoulsPlayer.SpectreCD = flag ? 5 : 20;
      }
      else
      {
        if (projectile.type == 356)
          return;
        this.SpectreHurt(projectile);
        if (flag || hitInfo.Crit && Utils.NextBool(Main.rand, 5))
          this.SpectreHeal(player, target, projectile);
        fargoSoulsPlayer.SpectreCD = flag ? 5 : 20;
      }
    }

    public void SpectreHeal(Player player, NPC npc, Projectile proj)
    {
      if (!npc.canGhostHeal || player.moonLeech)
        return;
      float num1 = 0.2f - (float) proj.numHits * 0.05f;
      if ((double) num1 <= 0.0)
        return;
      float num2 = (float) proj.damage * num1;
      if ((int) num2 <= 0 || (double) Main.player[Main.myPlayer].lifeSteal <= 0.0)
        return;
      Main.player[Main.myPlayer].lifeSteal -= num2 * 5f;
      float num3 = 0.0f;
      int num4 = proj.owner;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (((Entity) Main.player[index]).active && !Main.player[index].dead && (!Main.player[proj.owner].hostile && !Main.player[index].hostile || Main.player[proj.owner].team == Main.player[index].team) && (double) Math.Abs(((Entity) Main.player[index]).position.X + (float) (((Entity) Main.player[index]).width / 2) - ((Entity) proj).position.X + (float) (((Entity) proj).width / 2)) + (double) Math.Abs(((Entity) Main.player[index]).position.Y + (float) (((Entity) Main.player[index]).height / 2) - ((Entity) proj).position.Y + (float) (((Entity) proj).height / 2)) < 1200.0 && (double) (Main.player[index].statLifeMax2 - Main.player[index].statLife) > (double) num3)
        {
          num3 = (float) (Main.player[index].statLifeMax2 - Main.player[index].statLife);
          num4 = index;
        }
      }
      Projectile.NewProjectile(((Entity) proj).GetSource_FromThis((string) null), ((Entity) proj).position.X, ((Entity) proj).position.Y, 0.0f, 0.0f, 298, 0, 0.0f, proj.owner, (float) num4, num2, 0.0f);
    }

    public void SpectreHurt(Projectile proj)
    {
      int num1 = proj.damage / 2;
      if (proj.damage / 2 <= 1)
        return;
      int num2 = 1000;
      if ((double) Main.player[Main.myPlayer].ghostDmg > (double) num2)
        return;
      Main.player[Main.myPlayer].ghostDmg += (float) num1;
      int[] numArray = new int[200];
      int index1 = 0;
      int index2 = 0;
      for (int index3 = 0; index3 < 200; ++index3)
      {
        if (Main.npc[index3].CanBeChasedBy((object) this, false))
        {
          float num3 = Math.Abs(((Entity) Main.npc[index3]).position.X + (float) (((Entity) Main.npc[index3]).width / 2) - ((Entity) proj).position.X + (float) (((Entity) proj).width / 2)) + Math.Abs(((Entity) Main.npc[index3]).position.Y + (float) (((Entity) Main.npc[index3]).height / 2) - ((Entity) proj).position.Y + (float) (((Entity) proj).height / 2));
          if ((double) num3 < 800.0)
          {
            if (Collision.CanHit(((Entity) proj).position, 1, 1, ((Entity) Main.npc[index3]).position, ((Entity) Main.npc[index3]).width, ((Entity) Main.npc[index3]).height) && (double) num3 > 50.0)
            {
              numArray[index2] = index3;
              ++index2;
            }
            else if (index2 == 0)
            {
              numArray[index1] = index3;
              ++index1;
            }
          }
        }
      }
      if (index1 == 0 && index2 == 0)
        return;
      int num4 = index2 <= 0 ? numArray[Main.rand.Next(index1)] : numArray[Main.rand.Next(index2)];
      float num5 = (float) Main.rand.Next(-100, 101);
      float num6 = (float) Main.rand.Next(-100, 101);
      float num7 = (float) (4.0 / Math.Sqrt((double) num5 * (double) num5 + (double) num6 * (double) num6));
      float num8 = num5 * num7;
      float num9 = num6 * num7;
      Projectile.NewProjectile(((Entity) proj).GetSource_FromThis((string) null), ((Entity) proj).position.X, ((Entity) proj).position.Y, num8, num9, 356, num1, 0.0f, proj.owner, (float) num4, 0.0f, 0.0f);
    }
  }
}
