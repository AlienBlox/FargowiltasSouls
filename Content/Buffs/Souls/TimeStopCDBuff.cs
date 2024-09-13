// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.TimeStopCDBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class TimeStopCDBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (player.buffTime[buffIndex] != 2)
        return;
      for (int index1 = 0; index1 < Main.maxProjectiles; ++index1)
      {
        Projectile projectile = Main.projectile[index1];
        if (((Entity) projectile).active && projectile.type == 623 && projectile.owner == ((Entity) player).whoAmI)
        {
          for (int index2 = 0; index2 < 20; ++index2)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index2 - 9) * 6.2831854820251465 / 20.0, new Vector2()), ((Entity) projectile).Center);
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) projectile).Center);
            int index3 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 20, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index3].noGravity = true;
            Main.dust[index3].velocity = vector2_2;
            Main.dust[index3].scale = 1.5f;
          }
          break;
        }
      }
    }
  }
}
