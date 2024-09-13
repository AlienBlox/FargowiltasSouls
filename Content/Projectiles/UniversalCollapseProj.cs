// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.UniversalCollapseProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class UniversalCollapseProj : ModProjectile
  {
    public int countdown = 4;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.aiStyle = 16;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 2400;
    }

    public virtual void AI()
    {
      if (this.Projectile.timeLeft % 600 == 0)
      {
        CombatText.NewText(((Entity) this.Projectile).Hitbox, new Color(51, 102, 0), this.countdown, true, false);
        --this.countdown;
      }
      this.Projectile.scale += 0.01f;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 600)
        return;
      ++this.Projectile.frame;
      this.Projectile.frameCounter = 0;
      if (this.Projectile.frame <= 3)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
        {
          Tile tile = ((Tilemap) ref Main.tile)[index1, index2];
          ((Tile) ref tile).ClearEverything();
          if (WorldGen.InWorld(index1, index2, 0))
            Main.Map.Update(index1, index2, byte.MaxValue);
        }
      }
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.boss && !npc.dontTakeDamage)
          npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      }
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
      {
        Player.DefenseStat statDefense = Main.LocalPlayer.statDefense;
        float endurance = Main.LocalPlayer.endurance;
        ref MultipliableFloat local = ref Main.LocalPlayer.statDefense.FinalMultiplier;
        local = MultipliableFloat.op_Multiply(local, 0.0f);
        Main.LocalPlayer.endurance = 0.0f;
        int num = Math.Max(9999, Main.LocalPlayer.statLifeMax2 * 2);
        Main.LocalPlayer.Hurt(PlayerDeathReason.ByProjectile(((Entity) Main.LocalPlayer).whoAmI, ((Entity) this.Projectile).whoAmI), num, 0, false, false, -1, true, 0.0f, 0.0f, 4.5f);
        Main.LocalPlayer.statDefense = statDefense;
        Main.LocalPlayer.endurance = endurance;
      }
      Main.refreshMap = true;
      if (Main.dedServ)
        return;
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(), (SoundUpdateCallback) null);
    }
  }
}
