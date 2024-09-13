// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.TheLightningRodProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class TheLightningRodProj : ModProjectile
  {
    public int damage;
    private const int maxTime = 80;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 132;
      ((Entity) this.Projectile).height = 132;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1.5f;
      this.Projectile.hide = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.alpha = 0;
      this.Projectile.timeLeft = 45;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (this.Projectile.owner == Main.myPlayer && !player.controlUseItem && (double) this.Projectile.ai[0] == 0.0)
        this.Projectile.Kill();
      else if (player.dead || !((Entity) player).active || player.ghost)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.localAI[0] % 20.0 == 0.0)
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        Vector2 vector2_1 = player.RotatedRelativePoint(player.MountedCenter, false, true);
        if ((double) this.Projectile.ai[0] == 0.0 && (double) ((Entity) player).velocity.X != 0.0)
          player.ChangeDir(Math.Sign(((Entity) player).velocity.X));
        ((Entity) this.Projectile).direction = ((Entity) player).direction;
        player.heldProj = ((Entity) this.Projectile).whoAmI;
        player.itemTime = 2;
        player.itemAnimation = 2;
        float num1 = (float) (0.30575111508369446 * ((double) this.Projectile.ai[0] == 0.0 ? (double) Math.Min(1f, this.Projectile.localAI[0] / 80f) : 1.0));
        if ((double) this.Projectile.ai[0] == 0.0)
        {
          this.Projectile.localAI[0] += (float) ((double) player.GetAttackSpeed(DamageClass.Melee) + (double) player.FargoSouls().AttackSpeed - 1.0);
          this.Projectile.timeLeft = 82;
          this.damage = this.Projectile.damage;
          this.Projectile.numHits = 0;
          this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X);
          this.Projectile.rotation += num1 * (float) ((Entity) player).direction;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
          player.itemRotation = MathHelper.WrapAngle(this.Projectile.rotation);
          if ((double) this.Projectile.localAI[0] > 40.0)
            this.Projectile.ai[1] = 1f;
          if ((double) this.Projectile.localAI[0] > 120.0 && this.Projectile.owner == Main.myPlayer)
          {
            this.Projectile.ai[0] = 1f;
            this.Projectile.localAI[0] = 0.0f;
            this.Projectile.localAI[1] = ((Entity) this.Projectile).Distance(Main.MouseWorld);
            if ((double) this.Projectile.localAI[1] < 200.0)
              this.Projectile.localAI[1] = 200f;
            ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.MouseWorld);
            this.Projectile.netUpdate = true;
          }
        }
        else
        {
          ++this.Projectile.localAI[0];
          this.Projectile.damage = (int) ((double) this.damage * Math.Pow(0.933, (double) this.Projectile.numHits));
          if (this.Projectile.damage < this.damage / 2)
            this.Projectile.damage = this.damage / 2;
          if ((double) this.Projectile.localAI[0] > 80.0)
          {
            if (this.Projectile.owner != Main.myPlayer)
              return;
            this.Projectile.Kill();
            return;
          }
          ((Entity) this.Projectile).direction = Math.Sign(((Entity) this.Projectile).Center.X - ((Entity) player).Center.X);
          player.ChangeDir(((Entity) this.Projectile).direction);
          player.itemRotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y * (double) ((Entity) this.Projectile).direction, (double) ((Entity) this.Projectile).velocity.X * (double) ((Entity) this.Projectile).direction);
          if ((double) this.Projectile.localAI[0] < 40.0)
          {
            if ((double) this.Projectile.localAI[0] % 6.0 == 0.0 && this.Projectile.owner == Main.myPlayer)
            {
              Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.Projectile).width / 4), (float) (((Entity) this.Projectile).height / 2))), Vector2.op_Multiply(Utils.NextFloat(Main.rand, 900f, 1800f), Vector2.UnitY));
              float num2 = ((Entity) this.Projectile).Center.Y + Utils.NextFloat(Main.rand, (float) (-((Entity) this.Projectile).height / 4), (float) (((Entity) this.Projectile).height / 4));
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, Vector2.op_Multiply(12f, Vector2.UnitY), ModContent.ProjectileType<TheLightning>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, Utils.ToRotation(Vector2.UnitY), num2, 0.0f);
            }
            if (Collision.SolidTiles(((Entity) this.Projectile).Center, 2, 2, false) && this.Projectile.owner == Main.myPlayer)
            {
              SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
              this.Projectile.localAI[0] = 80f - this.Projectile.localAI[0];
              this.Projectile.netUpdate = true;
            }
          }
          this.Projectile.rotation += (float) ((double) num1 * (double) ((Entity) player).direction * 1.25);
          float num3 = (float) Math.Sin(Math.PI / 80.0 * (double) this.Projectile.localAI[0]);
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num3), this.Projectile.localAI[1]);
        }
        if ((double) this.Projectile.ai[1] != 0.0)
        {
          int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 156, 0.0f, 0.0f, 100, new Color(), 1f);
          Main.dust[index1].noGravity = true;
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 156, 0.0f, 0.0f, 100, new Color(), 1f);
          Main.dust[index2].noGravity = true;
        }
        ((Entity) this.Projectile).Center = vector2_1;
      }
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      ((NPC.HitModifiers) ref modifiers).HitDirectionOverride = new int?(Math.Sign(((Entity) target).Center.X - ((Entity) Main.player[this.Projectile.owner]).Center.X));
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[1] != 0.0);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual bool? CanHitNPC(NPC target)
    {
      return !target.noTileCollide && !Collision.CanHitLine(((Entity) this.Projectile).Center, 0, 0, ((Entity) target).Center, 0, 0) ? new bool?(false) : base.CanHitNPC(target);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      if ((double) this.Projectile.ai[1] != 0.0)
      {
        Color alpha = this.Projectile.GetAlpha(lightColor);
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
