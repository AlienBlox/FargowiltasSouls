// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.BeetleEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class BeetleEnchant : BaseEnchant
  {
    public override Color nameColor => new Color(109, 92, 133);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 8;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      BeetleEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      if (player.beetleDefense || player.beetleOffense)
        return;
      player.AddEffect<BeetleEffect>(item);
      if (!player.HasEffect<BeetleEffect>())
        return;
      Player player1 = player;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.BeetleEnchantDefenseTimer > 0)
      {
        player1.beetleDefense = true;
        ++player1.beetleCounter;
        int num1 = fargoSoulsPlayer.TerrariaSoul ? 3 : 2;
        int num2 = 540 / num1;
        if ((double) player1.beetleCounter >= (double) num2)
        {
          if (player1.beetleOrbs > 0 && player1.beetleOrbs < num1)
          {
            for (int index = 0; index < Player.MaxBuffs; ++index)
            {
              if (player1.buffType[index] >= 95 && player1.buffType[index] <= 96)
                player1.DelBuff(index);
            }
          }
          if (player1.beetleOrbs < num1)
          {
            player1.AddBuff(95 + player1.beetleOrbs, 5, false, false);
            player1.beetleCounter = 0.0f;
          }
          else
            player1.beetleCounter = (float) num2;
        }
      }
      else
      {
        player1.beetleOffense = true;
        player1.beetleCounter -= 3f;
        player1.beetleCounter -= (float) player1.beetleCountdown / 10f;
        ++player1.beetleCountdown;
        if ((double) player1.beetleCounter < 0.0)
          player1.beetleCounter = 0.0f;
        int num3 = 0;
        int num4 = 400;
        int num5 = 1200;
        int num6 = 4600;
        if ((double) player1.beetleCounter > (double) (num4 + num5 + num6 + num5))
          player1.beetleCounter = (float) (num4 + num5 + num6 + num5);
        if (fargoSoulsPlayer.TerrariaSoul && (double) player1.beetleCounter > (double) (num4 + num5 + num6))
        {
          player1.AddBuff(100, 5, false, false);
          num3 = 3;
        }
        else
        {
          player1.buffImmune[100] = true;
          if ((double) player1.beetleCounter > (double) (num4 + num5))
          {
            player1.AddBuff(99, 5, false, false);
            num3 = 2;
          }
          else if ((double) player1.beetleCounter > (double) num4)
          {
            player1.AddBuff(98, 5, false, false);
            num3 = 1;
          }
        }
        if (num3 < player1.beetleOrbs)
          player1.beetleCountdown = 0;
        else if (num3 > player1.beetleOrbs)
          player1.beetleCounter += 200f;
        float num7 = (float) num3 * 0.1f;
        ref StatModifier local1 = ref player1.GetDamage(DamageClass.Generic);
        local1 = StatModifier.op_Addition(local1, num7);
        ref StatModifier local2 = ref player1.GetDamage(DamageClass.Melee);
        local2 = StatModifier.op_Subtraction(local2, num7);
        if (num3 != player1.beetleOrbs && player1.beetleOrbs > 0)
        {
          for (int index = 0; index < Player.MaxBuffs; ++index)
          {
            if (player1.buffType[index] >= 98 && player1.buffType[index] <= 100 && player1.buffType[index] != 97 + num3)
              player1.DelBuff(index);
          }
        }
      }
      if (!player1.beetleDefense && !player1.beetleOffense)
      {
        player1.beetleCounter = 0.0f;
      }
      else
      {
        ++player1.beetleFrameCounter;
        if (player1.beetleFrameCounter >= 1)
        {
          player1.beetleFrameCounter = 0;
          ++player1.beetleFrame;
          if (player1.beetleFrame > 2)
            player1.beetleFrame = 0;
        }
        for (int beetleOrbs = player1.beetleOrbs; beetleOrbs < 3; ++beetleOrbs)
        {
          player1.beetlePos[beetleOrbs].X = 0.0f;
          player1.beetlePos[beetleOrbs].Y = 0.0f;
        }
        for (int index = 0; index < player1.beetleOrbs; ++index)
        {
          ref Vector2 local3 = ref player1.beetlePos[index];
          local3 = Vector2.op_Addition(local3, player1.beetleVel[index]);
          player1.beetleVel[index].X += (float) Main.rand.Next(-100, 101) * 0.005f;
          player1.beetleVel[index].Y += (float) Main.rand.Next(-100, 101) * 0.005f;
          float x1 = player1.beetlePos[index].X;
          float y1 = player1.beetlePos[index].Y;
          float num8 = (float) Math.Sqrt((double) x1 * (double) x1 + (double) y1 * (double) y1);
          if ((double) num8 > 100.0)
          {
            float num9 = 20f / num8;
            float num10 = x1 * -num9;
            float num11 = y1 * -num9;
            int num12 = 10;
            player1.beetleVel[index].X = (player1.beetleVel[index].X * (float) (num12 - 1) + num10) / (float) num12;
            player1.beetleVel[index].Y = (player1.beetleVel[index].Y * (float) (num12 - 1) + num11) / (float) num12;
          }
          else if ((double) num8 > 30.0)
          {
            float num13 = 10f / num8;
            float num14 = x1 * -num13;
            float num15 = y1 * -num13;
            int num16 = 20;
            player1.beetleVel[index].X = (player1.beetleVel[index].X * (float) (num16 - 1) + num14) / (float) num16;
            player1.beetleVel[index].Y = (player1.beetleVel[index].Y * (float) (num16 - 1) + num15) / (float) num16;
          }
          float x2 = player1.beetleVel[index].X;
          float y2 = player1.beetleVel[index].Y;
          if (Math.Sqrt((double) x2 * (double) x2 + (double) y2 * (double) y2) > 2.0)
          {
            ref Vector2 local4 = ref player1.beetleVel[index];
            local4 = Vector2.op_Multiply(local4, 0.9f);
          }
          ref Vector2 local5 = ref player1.beetlePos[index];
          local5 = Vector2.op_Subtraction(local5, Vector2.op_Multiply(((Entity) player1).velocity, 0.25f));
        }
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2199, 1).AddRecipeGroup("FargowiltasSouls:AnyBeetle", 1).AddIngredient(2202, 1).AddIngredient(2280, 1).AddIngredient(1515, 1).AddIngredient(749, 1).AddTile(125).Register();
    }
  }
}
