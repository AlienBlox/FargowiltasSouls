// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.CrimsonRegenBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class CrimsonRegenBuff : ModBuff
  {
    public virtual void SetStaticDefaults() => Main.buffNoSave[this.Type] = true;

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      ++fargoSoulsPlayer.CrimsonRegenTime;
      if (fargoSoulsPlayer.CrimsonRegenTime % 420 == 0)
      {
        player.statLife += fargoSoulsPlayer.CrimsonRegenAmount;
        player.HealEffect(fargoSoulsPlayer.CrimsonRegenAmount, true);
      }
      if (fargoSoulsPlayer.CrimsonRegenTime > (fargoSoulsPlayer.ForceEffect<CrimsonEnchant>() ? 840 : 420))
        player.DelBuff(buffIndex);
      for (int index1 = 0; index1 < 6; ++index1)
      {
        int num1 = fargoSoulsPlayer.CrimsonRegenTime > 420 ? 115 : 5;
        int index2 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, num1, 0.0f, 0.0f, 175, new Color(), 1.75f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.75f);
        int num2 = Main.rand.Next(-40, 41);
        int num3 = Main.rand.Next(-40, 41);
        Main.dust[index2].position.X += (float) num2;
        Main.dust[index2].position.Y += (float) num3;
        Main.dust[index2].velocity.X = -(float) num2 * 0.075f;
        Main.dust[index2].velocity.Y = -(float) num3 * 0.075f;
      }
    }
  }
}
