// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronWhirlpool
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronWhirlpool : ModProjectile
  {
    public bool Fade;
    private bool parentNPC;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = 16;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 186;
      ((Entity) this.Projectile).height = 48;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(148, 600, true, false);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
      writer.Write(this.Projectile.localAI[2]);
      writer.Write(this.parentNPC);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.Projectile.localAI[2] = reader.ReadSingle();
      this.parentNPC = reader.ReadBoolean();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC))
        return;
      this.parentNPC = true;
    }

    public virtual void AI()
    {
      double num1 = (double) this.Projectile.ai[0];
      ref float local1 = ref this.Projectile.ai[1];
      ref float local2 = ref this.Projectile.ai[2];
      ref float local3 = ref this.Projectile.localAI[0];
      ref float local4 = ref this.Projectile.localAI[1];
      ref float local5 = ref this.Projectile.localAI[2];
      if ((double) local3 > 240.0)
        this.Fade = true;
      if (this.Projectile.alpha == 0)
      {
        int num2 = (int) ((double) local1 % 3.0) + (int) local5;
        if ((double) local2 == 1.0)
        {
          int num3 = 46;
          if ((double) local3 % (double) num3 == 0.0 && (double) local3 % (double) (num3 * 3) == (double) (num3 * num2))
          {
            SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = -1; index < 2; index += 2)
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index), (float) ((Entity) this.Projectile).width), Utils.NextFloat(Main.rand, 0.2f, 0.35f))), Vector2.op_Multiply(Vector2.UnitY, (float) Main.rand.Next(-((Entity) this.Projectile).height / 4, ((Entity) this.Projectile).height / 4))), Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) index), (double) Utils.NextFloat(Main.rand, 0.1308997f), new Vector2()), ModContent.ProjectileType<BaronWhirlpoolBolt>(), (int) ((double) this.Projectile.damage * 0.800000011920929), this.Projectile.knockBack, this.Projectile.owner, 1f, 0.0f, 0.0f);
            }
          }
        }
        else if ((double) local3 % 50.0 == 0.0 && (double) local3 % 150.0 == (double) (50 * num2))
        {
          SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = -1; index < 2; index += 2)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) index), (float) ((Entity) this.Projectile).width), Utils.NextFloat(Main.rand, 0.2f, 0.35f))), Vector2.op_Multiply(Vector2.UnitY, (float) Main.rand.Next(-((Entity) this.Projectile).height / 4, ((Entity) this.Projectile).height / 4))), Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) index), (double) Utils.NextFloat(Main.rand, 0.1308997f), new Vector2()), ModContent.ProjectileType<BaronWhirlpoolBolt>(), (int) ((double) this.Projectile.damage * 0.800000011920929), this.Projectile.knockBack, this.Projectile.owner, 1f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) local3 < 20.0)
      {
        this.Projectile.alpha -= 17;
        if (this.Projectile.alpha <= 0)
          this.Projectile.alpha = 0;
      }
      if ((WorldSavingSystem.MasochistModeReal ? 0 : (Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height) ? 1 : 0)) != 0 && (double) local1 > 3.0)
        local1 = 3f;
      if ((double) local3 == (double) (8 + ((double) local2 == 1.0 ? 5 : 0)) && (double) local1 > 0.0 && FargoSoulsUtil.HostCheck)
      {
        local4 = (float) Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, (float) ((Entity) this.Projectile).height)), Vector2.Zero, this.Type, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, (float) this.Projectile.identity, local1 - 1f, local2);
        int num4 = this.Projectile.frame - 1;
        if (num4 < 0 || num4 >= Main.projFrames[this.Type])
          num4 = Main.projFrames[this.Type] - 1;
        Main.projectile[(int) local4].frame = num4;
      }
      if (this.Fade)
      {
        this.Projectile.alpha += 17;
        if (this.Projectile.alpha >= 238)
        {
          Projectile projectile = Main.projectile[(int) local4];
          if (projectile.TypeAlive(this.Type))
            Luminance.Common.Utilities.Utilities.As<BaronWhirlpool>(projectile).Fade = true;
          this.Projectile.Kill();
        }
      }
      if (++this.Projectile.frameCounter > 2)
      {
        if (++this.Projectile.frame >= Main.projFrames[this.Type])
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      ++local3;
    }
  }
}
