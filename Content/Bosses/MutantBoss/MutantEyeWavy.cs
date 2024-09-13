// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantEyeWavy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantEyeWavy : MutantEye
  {
    public float oldRot;

    public override string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_452" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_April";
      }
    }

    public override int TrailAdditive => 150;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 180;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.CooldownSlot = 0;
    }

    private float Amplitude => this.Projectile.ai[0];

    private float Period => this.Projectile.ai[1];

    private float Counter => this.Projectile.localAI[1] * 4f;

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(EModeGlobalNPC.mutantBoss, Array.Empty<int>());
      if (npc != null && ((double) npc.ai[0] == -5.0 || (double) npc.ai[0] == -7.0))
      {
        float num = npc.ai[3];
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), Utils.ToRotationVector2(num + (float) (0.78539818525314331 * Math.Sin(6.2831854820251465 * (double) this.Counter / (double) this.Period)) * this.Amplitude));
        if ((double) this.oldRot != 0.0)
        {
          Vector2 center = ((Entity) this.Projectile).Center;
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) npc).Center), (double) num - (double) this.oldRot, new Vector2()));
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, center);
          for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
          {
            ref Vector2 local = ref this.Projectile.oldPos[index];
            local = Vector2.op_Addition(local, vector2);
          }
        }
        this.oldRot = num;
        this.Projectile.localAI[0] += 0.1f;
        base.AI();
      }
      else
        this.Projectile.Kill();
    }

    public override void OnKill(int timeleft)
    {
    }
  }
}
