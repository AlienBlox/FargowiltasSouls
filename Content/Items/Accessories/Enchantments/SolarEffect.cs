// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.SolarEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class SolarEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<CosmoHeader>();

    public override int ToggleItemType => ModContent.ItemType<SolarEnchant>();

    public static void AddDash(Player player)
    {
      Player player1 = player;
      if (!player1.setSolar && !player.FargoSouls().TerrariaSoul)
        player1.endurance -= 0.15f;
      player1.AddBuff(172, 5, false, false);
      player1.setSolar = true;
      int num = 240;
      if (++player1.solarCounter >= num)
      {
        if (player1.solarShields > 0 && player1.solarShields < 3)
        {
          for (int index = 0; index < Player.MaxBuffs; ++index)
          {
            if (player1.buffType[index] >= 170 && player1.buffType[index] <= 171)
              player1.DelBuff(index);
          }
        }
        if (player1.solarShields < 3)
        {
          player1.AddBuff(170 + player1.solarShields, 5, false, false);
          for (int index = 0; index < 16; ++index)
          {
            Dust dust = Main.dust[Dust.NewDust(((Entity) player1).position, ((Entity) player1).width, ((Entity) player1).height, 6, 0.0f, 0.0f, 100, new Color(), 1f)];
            dust.noGravity = true;
            dust.scale = 1.7f;
            dust.fadeIn = 0.5f;
            dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
          }
          player1.solarCounter = 0;
        }
        else
          player1.solarCounter = num;
      }
      for (int solarShields = player1.solarShields; solarShields < 3; ++solarShields)
        player1.solarShieldPos[solarShields] = Vector2.Zero;
      for (int index = 0; index < player1.solarShields; ++index)
      {
        ref Vector2 local = ref player1.solarShieldPos[index];
        local = Vector2.op_Addition(local, player1.solarShieldVel[index]);
        Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) ((double) player1.miscCounter / 100.0 * 6.2831854820251465 + (double) index * (6.2831854820251465 / (double) player1.solarShields))), 6f);
        vector2.X = (float) (((Entity) player1).direction * 20);
        player1.solarShieldVel[index] = Vector2.op_Multiply(Vector2.op_Subtraction(vector2, player1.solarShieldPos[index]), 0.2f);
      }
      if (player1.dashDelay >= 0)
      {
        player1.solarDashing = false;
        player1.solarDashConsumedFlare = false;
      }
      bool flag = player1.solarDashing && player1.dashDelay < 0;
      if (!(player1.solarShields > 0 | flag))
        return;
      player1.dashType = 3;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.FargoDash = DashManager.DashType.None;
      fargoSoulsPlayer.HasDash = true;
    }
  }
}
