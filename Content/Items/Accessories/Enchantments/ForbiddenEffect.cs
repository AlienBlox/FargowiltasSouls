// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ForbiddenEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ForbiddenEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<SpiritHeader>();

    public override int ToggleItemType => ModContent.ItemType<ForbiddenEnchant>();

    public static void ActivateForbiddenStorm(Player player)
    {
      if (!player.HasEffect<ForbiddenEffect>())
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.CanSummonForbiddenStorm)
        return;
      ForbiddenEffect.CommandForbiddenStorm(player);
      fargoSoulsPlayer.CanSummonForbiddenStorm = false;
    }

    public static void CommandForbiddenStorm(Player Player)
    {
      List<int> intList = new List<int>();
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (((Entity) projectile).active && projectile.type == ModContent.ProjectileType<ForbiddenTornado>() && projectile.owner == ((Entity) Player).whoAmI)
          intList.Add(index);
      }
      Vector2 center = ((Entity) Player).Center;
      Vector2 mouseWorld = Main.MouseWorld;
      bool flag1 = false;
      float[] numArray = new float[10];
      Vector2 vector2_1 = Vector2.op_Subtraction(mouseWorld, center);
      Collision.LaserScan(center, Utils.SafeNormalize(vector2_1, Vector2.Zero), 60f, ((Vector2) ref vector2_1).Length(), numArray);
      float num1 = 0.0f;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if ((double) numArray[index] > (double) num1)
          num1 = numArray[index];
      }
      foreach (float num2 in numArray)
      {
        if ((double) Math.Abs(num2 - ((Vector2) ref vector2_1).Length()) < 10.0)
        {
          flag1 = true;
          break;
        }
      }
      if (intList.Count <= 1)
      {
        Vector2 vector2_2 = Vector2.op_Addition(center, Vector2.op_Multiply(Utils.SafeNormalize(vector2_1, Vector2.Zero), num1));
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, center);
        if ((double) ((Vector2) ref vector2_3).Length() > 0.0)
        {
          for (float num3 = 0.0f; (double) num3 < (double) ((Vector2) ref vector2_3).Length(); num3 += 15f)
          {
            Vector2 vector2_4 = Vector2.op_Addition(center, Vector2.op_Multiply(vector2_3, num3 / ((Vector2) ref vector2_3).Length()));
            Dust dust = Main.dust[Dust.NewDust(vector2_4, 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f)];
            dust.position = vector2_4;
            dust.fadeIn = 0.5f;
            dust.scale = 0.7f;
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.4f);
            dust.noLight = true;
          }
        }
        for (float num4 = 0.0f; (double) num4 < 6.2831854820251465; num4 += 0.209439516f)
        {
          Dust dust = Main.dust[Dust.NewDust(vector2_2, 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f)];
          dust.position = vector2_2;
          dust.fadeIn = 1f;
          dust.scale = 0.3f;
          dust.noLight = true;
        }
      }
      bool flag2 = intList.Count <= 1 & flag1;
      if (flag2)
      {
        flag2 = Player.CheckMana(20, true, false);
        if (flag2)
          Player.manaRegenDelay = (float) (int) Player.maxRegenDelay;
      }
      if (!flag2)
        return;
      foreach (int index in intList)
      {
        Projectile projectile = Main.projectile[index];
        if ((double) projectile.ai[0] < 780.0)
        {
          projectile.ai[0] = (float) (780.0 + (double) projectile.ai[0] % 60.0);
          projectile.netUpdate = true;
        }
      }
      int num5 = (int) (20.0 * (1.0 + (double) ((StatModifier) ref Player.GetDamage(DamageClass.Magic)).Additive + (double) ((StatModifier) ref Player.GetDamage(DamageClass.Summon)).Additive - 2.0));
      Projectile.NewProjectile(Player.GetSource_EffectItem<ForbiddenEffect>(), mouseWorld, Vector2.Zero, ModContent.ProjectileType<ForbiddenTornado>(), num5, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public override void DrawEffects(
      Player player,
      PlayerDrawSet drawInfo,
      ref float r,
      ref float g,
      ref float b,
      ref float a,
      ref bool fullBright)
    {
      if ((double) drawInfo.shadow != 0.0)
        return;
      Color color1 = Color.Lerp(player.GetImmuneAlphaPure(Lighting.GetColor((int) ((double) drawInfo.Position.X + (double) ((Entity) player).width * 0.5) / 16, (int) ((double) drawInfo.Position.Y + (double) ((Entity) player).height * 0.5) / 16, Color.White), drawInfo.shadow), Color.White, 0.7f);
      Texture2D texture2D1 = TextureAssets.Extra[74].Value;
      Texture2D texture2D2 = TextureAssets.GlowMask[217].Value;
      int num1 = !player.setForbiddenCooldownLocked ? 1 : 0;
      int num2 = (int) ((double) Utils.ToRotationVector2((float) ((double) player.miscCounter / 300.0 * 6.2831854820251465)).Y * 6.0);
      float num3 = Utils.ToRotationVector2((float) ((double) player.miscCounter / 75.0 * 6.2831854820251465)).X * 4f;
      Color color2 = Color.op_Multiply(Color.op_Multiply(new Color(80, 70, 40, 0), (float) ((double) num3 / 8.0 + 0.5)), 0.8f);
      if (num1 == 0)
      {
        num2 = 0;
        num3 = 2f;
        color2 = Color.op_Multiply(new Color(80, 70, 40, 0), 0.3f);
        color1 = Utils.MultiplyRGB(color1, new Color(0.5f, 0.5f, 1f));
      }
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(new Vector2((float) (int) ((double) drawInfo.Position.X - (double) Main.screenPosition.X - (double) (player.bodyFrame.Width / 2) + (double) (((Entity) player).width / 2)), (float) (int) ((double) drawInfo.Position.Y - (double) Main.screenPosition.Y + (double) ((Entity) player).height - (double) player.bodyFrame.Height + 4.0)), player.bodyPosition), new Vector2((float) (player.bodyFrame.Width / 2), (float) (player.bodyFrame.Height / 2))), new Vector2((float) (-(double) ((Entity) player).direction * 10.0), (float) (num2 - 20)));
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D1, vector2, new Rectangle?(), color1, player.bodyRotation, Vector2.op_Division(Utils.Size(texture2D1), 2f), 1f, drawInfo.playerEffect, 0.0f);
      int num4 = 0;
      if (player.dye[1] != null)
        num4 = player.dye[1].dye;
      drawData.shader = num4;
      drawInfo.DrawDataCache.Add(drawData);
      for (float num5 = 0.0f; (double) num5 < 4.0; ++num5)
      {
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D2, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.ToRotationVector2(num5 * 1.57079637f), num3)), new Rectangle?(), color2, player.bodyRotation, Vector2.op_Division(Utils.Size(texture2D1), 2f), 1f, drawInfo.playerEffect, 0.0f);
        drawInfo.DrawDataCache.Add(drawData);
      }
    }
  }
}
