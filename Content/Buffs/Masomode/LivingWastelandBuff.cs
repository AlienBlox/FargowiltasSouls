// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.LivingWastelandBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class LivingWastelandBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && (double) ((Entity) Main.npc[index]).Distance(((Entity) player).Center) < 300.0)
          Main.npc[index].AddBuff(ModContent.BuffType<RottingBuff>(), 2, false);
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (((Entity) Main.player[index]).active && !Main.player[index].dead && index != ((Entity) player).whoAmI && (double) ((Entity) Main.player[index]).Distance(((Entity) player).Center) < 300.0)
          Main.player[index].AddBuff(ModContent.BuffType<RottingBuff>(), 2, true, false);
      }
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2.X += (float) (Math.Sin(num) * 300.0);
        vector2.Y += (float) (Math.Cos(num) * 300.0);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2), new Vector2(4f, 4f)), 0, 0, 119, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.velocity = ((Entity) player).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -5f));
        }
        dust1.noGravity = true;
      }
      player.FargoSouls().Rotting = true;
    }
  }
}
