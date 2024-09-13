// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosInvaderTime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosInvaderTime : CosmosInvader
  {
    public override string Texture => "Terraria/Images/Projectile_539";

    public override void SetDefaults()
    {
      base.SetDefaults();
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (Projectile => false);
    }

    public override bool PreAI()
    {
      if (!this.spawned)
      {
        this.Projectile.localAI[1] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        SoundStyle soundStyle = SoundID.Item25;
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        ((SoundStyle) ref soundStyle).Pitch = 0.0f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 176, 0.0f, 0.0f, 200, new Color(), 3.7f);
          Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, 3.14159274101257), (float) Main.rand.NextDouble()), (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index4].noGravity = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
        for (int index5 = 0; index5 < 20; ++index5)
        {
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 180, 0.0f, 0.0f, 0, new Color(), 2.7f);
          Main.dust[index6].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index6].noGravity = true;
          Dust dust = Main.dust[index6];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
        for (int index7 = 0; index7 < 10; ++index7)
        {
          int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index8].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 3.14159274101257), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()), (float) ((Entity) this.Projectile).width), 2f));
          Main.dust[index8].noGravity = true;
          Dust dust = Main.dust[index8];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
      }
      return base.PreAI();
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] < 60.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Addition(((Entity) projectile).velocity, Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.ai[1]), this.Projectile.localAI[1]), 60f));
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if (++this.Projectile.frameCounter >= 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      ++this.Projectile.localAI[0];
    }
  }
}
