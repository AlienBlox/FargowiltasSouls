// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GalacticReformerProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GalacticReformerProj : ModProjectile
  {
    public int countdown = 5;
    private const int radius = 300;
    private bool die;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 5;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.aiStyle = 16;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 1000;
    }

    public virtual void AI()
    {
      if (this.Projectile.timeLeft % 200 == 0)
      {
        CombatText.NewText(((Entity) this.Projectile).Hitbox, new Color(51, 102, 0), this.countdown, true, false);
        --this.countdown;
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 200)
        return;
      ++this.Projectile.frame;
      this.Projectile.frameCounter = 0;
      if (this.Projectile.frame <= 4)
        return;
      this.Projectile.frame = 0;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (!this.die)
        return new bool?();
      Vector2 vector2 = Vector2.op_Subtraction(Utils.ToVector2(((Rectangle) ref targetHitbox).Center), Utils.ToVector2(((Rectangle) ref projHitbox).Center));
      return new bool?((double) ((Vector2) ref vector2).Length() < (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual bool? CanHitNPC(NPC target) => target.boss ? new bool?(false) : new bool?();

    public virtual void OnKill(int timeLeft)
    {
      if (!this.die)
      {
        this.die = true;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 9616;
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        this.Projectile.hostile = true;
        this.Projectile.damage = 2000;
        this.Projectile.Damage();
      }
      Vector2 center = ((Entity) this.Projectile).Center;
      for (int index1 = -300; index1 <= 300; ++index1)
      {
        for (int index2 = -300; index2 <= 300; ++index2)
        {
          if (Math.Sqrt((double) (index1 * index1 + index2 * index2)) <= 300.0)
          {
            int num1 = (int) ((double) index1 + (double) center.X / 16.0);
            int num2 = (int) ((double) index2 + (double) center.Y / 16.0);
            if (num1 >= 0 && num1 < Main.maxTilesX && num2 >= 0 && num2 < Main.maxTilesY)
            {
              Tile tile = ((Tilemap) ref Main.tile)[num1, num2];
              if (!Tile.op_Equality(tile, (ArgumentException) null) && WorldGen.InWorld(num1, num2, 0))
              {
                ((Tile) ref tile).ClearEverything();
                Main.Map.Update(num1, num2, byte.MaxValue);
              }
            }
          }
        }
      }
      Main.refreshMap = true;
      if (Main.dedServ)
        return;
      SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
    }
  }
}
