// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Magmaw.MagmawHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Magmaw
{
  public class MagmawHand : ModProjectile
  {
    public const int SwordLength = 150;
    public const int ArmWidth = 20;
    public const int DefaultDistance = 220;
    private const int Right = 1;
    private const int Left = -1;
    public bool HitPlayer;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = 1;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 10;
      this.Projectile.hide = true;
    }

    public virtual bool CanHitPlayer(Player target) => this.HitPlayer;

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (!this.HitPlayer)
        return new bool?(false);
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num1 = 0.0f;
      if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 150f), this.Projectile.scale)), (float) ((Entity) this.Projectile).width * this.Projectile.scale, ref num1))
        return new bool?(true);
      NPC parent = this.GetParent();
      float num2 = 0.0f;
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, ((Entity) parent).Center, 20f, ref num2) ? new bool?(true) : new bool?(false);
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      if (!this.Projectile.hide)
        return;
      behindNPCs.Add(index);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 75f));
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, drawPos: new Vector2?(vector2_1));
      int num1 = 1;
      Texture2D texture2D = TextureAssets.Projectile[ModContent.ProjectileType<GlowLine>()].Value;
      int num2 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num3 = num2 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num3, texture2D.Width, num2);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      NPC parent = this.GetParent();
      Vector2 vector2_3 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) parent).Center);
      int num4 = (int) ((Entity) this.Projectile).Distance(((Entity) parent).Center);
      Vector2 vector2_4 = Vector2.op_Division(Vector2.op_Multiply(vector2_3, (float) num4), 2f);
      Vector2 vector2_5 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenLastPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), vector2_4);
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector((int) vector2_5.X, (int) vector2_5.Y, num4, 20);
      Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.Orange, this.Projectile.Opacity), (float) Main.mouseTextColor / (float) byte.MaxValue), 0.9f);
      for (int index = 0; index < num1; ++index)
        Main.EntitySpriteDraw(new DrawData(texture2D, rectangle2, new Rectangle?(rectangle1), color, Utils.ToRotation(vector2_3), vector2_2, (SpriteEffects) 0, 0.0f));
      return false;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
      writer.Write(this.Projectile.localAI[2]);
      writer.Write(this.HitPlayer);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.Projectile.localAI[2] = reader.ReadSingle();
      this.HitPlayer = reader.ReadBoolean();
    }

    public ref float ParentID => ref this.Projectile.ai[0];

    public ref float Side => ref this.Projectile.ai[1];

    public virtual void AI()
    {
      this.Side = (float) Luminance.Common.Utilities.Utilities.NonZeroSign(this.Side);
      NPC parent = this.GetParent();
      FargowiltasSouls.Content.Bosses.Magmaw.Magmaw magmaw = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.Magmaw.Magmaw>(parent);
      this.Projectile.timeLeft = 60;
      this.Projectile.damage = parent.damage;
      magmaw.HandleHandState(this);
    }

    public void RotateTowards(Vector2 vectorToAlignWith, float fraction)
    {
      float num = FargoSoulsUtil.RotationDifference(Utils.ToRotationVector2(this.Projectile.rotation), vectorToAlignWith);
      if ((double) Math.Abs(num) < 0.078539818525314331)
        fraction = 1f;
      this.Projectile.rotation += num * fraction;
    }

    public NPC GetParent()
    {
      int index = (int) this.ParentID;
      if (!index.IsWithinBounds(Main.maxNPCs))
      {
        this.Projectile.Kill();
        return (NPC) null;
      }
      NPC npc = Main.npc[index];
      if (npc.TypeAlive<FargowiltasSouls.Content.Bosses.Magmaw.Magmaw>())
        return npc;
      this.Projectile.Kill();
      return (NPC) null;
    }
  }
}
