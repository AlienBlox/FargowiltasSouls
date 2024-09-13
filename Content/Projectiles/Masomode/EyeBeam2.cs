// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.EyeBeam2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class EyeBeam2 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_259";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(259);
      this.AIType = 259;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 300, true, false);
      NPC npc = FargoSoulsUtil.NPCExists(NPC.golemBoss, new int[1]
      {
        245
      });
      if (npc != null)
      {
        target.AddBuff(36, 600, true, false);
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
        target.AddBuff(195, 600, true, false);
        if (!Tile.op_Equality(((Tilemap) ref Main.tile)[(int) ((Entity) npc).Center.X / 16, (int) ((Entity) npc).Center.Y / 16], (ArgumentException) null))
        {
          Tile tile = ((Tilemap) ref Main.tile)[(int) ((Entity) npc).Center.X / 16, (int) ((Entity) npc).Center.Y / 16];
          if (((Tile) ref tile).WallType == (ushort) 87)
            goto label_4;
        }
        target.AddBuff(67, 120, true, false);
      }
label_4:
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<EarthChampion>()))
        return;
      target.AddBuff(67, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
