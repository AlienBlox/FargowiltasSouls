// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.TitaniumEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class TitaniumEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<TitaniumEnchant>();

    public override float ContactDamageDR(
      Player player,
      NPC npc,
      ref Player.HurtModifiers modifiers)
    {
      return base.ContactDamageDR(player, npc, ref modifiers);
    }

    public override float ProjectileDamageDR(
      Player player,
      Projectile projectile,
      ref Player.HurtModifiers modifiers)
    {
      return TitaniumEffect.TitaniumDR(player, (Entity) projectile);
    }

    public static float TitaniumDR(Player player, Entity attacker)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.TitaniumDRBuff)
        return 0.0f;
      int num;
      switch (attacker)
      {
        case NPC _:
          num = 1;
          break;
        case Projectile projectile:
          NPC sourceNpc = projectile.GetSourceNPC();
          if (sourceNpc != null)
          {
            num = (double) ((Entity) player).Distance(((Entity) sourceNpc).Center) < (double) (Math.Max(((Entity) sourceNpc).width, ((Entity) sourceNpc).height) + 128) ? 1 : 0;
            break;
          }
          goto default;
        default:
          num = 0;
          break;
      }
      return num != 0 ? (float) ((1.0 - (double) player.endurance) * (fargoSoulsPlayer.ForceEffect<TitaniumEnchant>() ? 0.34999999403953552 : 0.25)) : 0.0f;
    }

    public static void TitaniumShards(FargoSoulsPlayer modPlayer, Player player)
    {
      if (modPlayer.TitaniumCD)
        return;
      player.AddBuff(306, 600, true, false);
      if (player.ownedProjectileCounts[908] < 20)
      {
        int dmg = 50;
        if (modPlayer.ForceEffect(player.EffectItem<TitaniumEffect>().ModItem))
          dmg = FargoSoulsUtil.HighestDamageTypeScaling(player, dmg);
        Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<TitaniumEffect>(), (string) null), ((Entity) player).Center, Vector2.Zero, 908, dmg, 15f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        if (!player.HasBuff(ModContent.BuffType<TitaniumDRBuff>()))
        {
          for (int index1 = 0; index1 < 20; ++index1)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 9) * 6.2831854820251465 / 20.0, new Vector2()), ((Entity) player).Center);
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) player).Center);
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 146, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].scale = 1.5f;
            Main.dust[index2].velocity = vector2_2;
          }
        }
        int num = 240;
        player.AddBuff(ModContent.BuffType<TitaniumDRBuff>(), num, true, false);
      }
    }

    public override void PostUpdateMiscEffects(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.TitaniumDRBuff && fargoSoulsPlayer.prevDyes == null)
      {
        fargoSoulsPlayer.prevDyes = new List<int>();
        int shaderIdFromItemId = GameShaders.Armor.GetShaderIdFromItemId(3026);
        for (int index = 0; index < player.dye.Length; ++index)
        {
          fargoSoulsPlayer.prevDyes.Add(player.dye[index].dye);
          player.dye[index].dye = shaderIdFromItemId;
        }
        for (int index = 0; index < player.miscDyes.Length; ++index)
        {
          fargoSoulsPlayer.prevDyes.Add(player.miscDyes[index].dye);
          player.miscDyes[index].dye = shaderIdFromItemId;
        }
        player.UpdateDyes();
      }
      else
      {
        if (player.HasBuff(ModContent.BuffType<TitaniumDRBuff>()) || fargoSoulsPlayer.prevDyes == null)
          return;
        for (int index = 0; index < player.dye.Length; ++index)
          player.dye[index].dye = fargoSoulsPlayer.prevDyes[index];
        for (int index = 0; index < player.miscDyes.Length; ++index)
          player.miscDyes[index].dye = fargoSoulsPlayer.prevDyes[index + player.dye.Length];
        player.UpdateDyes();
        fargoSoulsPlayer.prevDyes = (List<int>) null;
      }
    }
  }
}
