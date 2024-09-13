// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberHook
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.TrojanSquirrel;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberHook : TrojanHook
  {
    public override string Texture => "Terraria/Images/Projectile_13";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.extraUpdates = 2;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(false);

    protected override bool flashingZapEffect => false;

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampion>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) this.Projectile).Center)) + 1.57079637f;
        if ((double) --this.Projectile.ai[1] > 0.0)
        {
          if (!this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).Center, 0, 0))
            this.Projectile.tileCollide = true;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
        }
        else
        {
          this.Projectile.extraUpdates = 0;
          this.Projectile.tileCollide = false;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          if ((double) this.Projectile.localAI[0] == 0.0)
          {
            npc.localAI[0] = (float) Math.Sign(((Entity) this.Projectile).Center.X - ((Entity) npc).Center.X);
            this.Projectile.localAI[0] = 1f;
            this.Projectile.localAI[1] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) this.Projectile).Center));
          }
          if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > 600.0)
            npc.localAI[0] = (float) Math.Sign(((Entity) this.Projectile).Center.X - ((Entity) npc).Center.X);
          if ((double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) this.Projectile).Center)))) > 1.5707963705062866)
          {
            this.Projectile.Kill();
          }
          else
          {
            Vector2 vector2 = Vector2.op_Multiply(42f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) this.Projectile).Center));
            float num = (float) ((double) Math.Min(((Entity) npc).Distance(((Entity) this.Projectile).Center) / 2400f, 1f) * 0.800000011920929 + 0.20000000298023224) * 0.06f;
            ((Entity) npc).velocity = Vector2.Lerp(((Entity) npc).velocity, vector2, num);
            if (this.Projectile.timeLeft <= 180)
              return;
            this.Projectile.timeLeft = 180;
          }
        }
      }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.ai[1] = 0.0f;
      return false;
    }
  }
}
