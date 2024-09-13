// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Potato.RazorBlade
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Potato
{
  public class RazorBlade : ModProjectile
  {
    private Vector2 mousePos;
    private int syncTimer;
    private int MaxDistance = 100;

    public bool Retreating
    {
      get
      {
        return (double) this.Projectile.ai[0] == 2.0 && (double) MathF.Abs(Utils.ToRotation(((Entity) this.Projectile).velocity) - Utils.ToRotation(((Entity) this.Projectile).DirectionTo(((Entity) Main.player[this.Projectile.owner]).Center))) % 6.2831854820251465 < 3.1415927410125732;
      }
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 28;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 20;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.tileCollide = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 25;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (!player.Alive() || !player.GetModPlayer<PatreonPlayer>().RazorContainer || this.Projectile.hostile)
        this.Projectile.Kill();
      if (((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => p.TypeAlive(this.Type) && p.owner == this.Projectile.owner && ((Entity) p).whoAmI < ((Entity) this.Projectile).whoAmI)))
        this.Projectile.Kill();
      ++this.Projectile.timeLeft;
      this.Projectile.rotation += 0.3f;
      if ((double) this.Projectile.ai[0] != 1.0)
        this.Projectile.ai[1] = 0.0f;
      if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 750.0)
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) player).DirectionTo(((Entity) this.Projectile).Center), 750f));
      this.Projectile.tileCollide = true;
      float num1 = this.Projectile.ai[0];
      if ((double) num1 != 0.0)
      {
        if ((double) num1 != 1.0)
        {
          if ((double) num1 != 2.0)
            return;
          if (this.Retreating)
            this.Projectile.tileCollide = false;
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center)), 25f), 0.08f);
          float num2 = (float) (int) Vector2.Distance(((Entity) this.Projectile).Center, ((Entity) player).Center);
          if (((double) num2 > (double) this.MaxDistance || Collision.SolidTiles(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height)) && (double) num2 >= (double) (((Entity) this.Projectile).width / 2))
            return;
          this.Projectile.ai[0] = 0.0f;
        }
        else
        {
          if ((double) this.Projectile.ai[1]++ <= 10.0)
            return;
          this.Projectile.ai[0] = 2f;
        }
      }
      else
      {
        if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 1200.0)
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
        if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > (double) this.MaxDistance * 1.5)
          this.Projectile.ai[0] = 2f;
        if (((Entity) player).whoAmI == Main.myPlayer)
        {
          this.mousePos = Main.MouseWorld;
          if (++this.syncTimer > 20)
          {
            this.syncTimer = 0;
            this.Projectile.netUpdate = true;
          }
        }
        float num3 = MathF.Min((float) this.MaxDistance, ((Entity) player).Distance(this.mousePos));
        Vector2 vector2 = Vector2.Normalize(Vector2.op_Subtraction(this.mousePos, ((Entity) player).Center));
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, num3)), ((Entity) this.Projectile).Center), 0.08f);
        ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.ClampLength(((Entity) this.Projectile).velocity, 0.0f, 14f);
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Addition(((Entity) projectile).velocity, Vector2.op_Division(((Entity) player).velocity, 3f));
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if (this.Projectile.soundDelay == 0)
        SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.Projectile.soundDelay = 10;
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X && (double) Math.Abs(oldVelocity.X) > 1.0)
        ((Entity) this.Projectile).velocity.X = oldVelocity.X * -0.5f;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 1.0)
        ((Entity) this.Projectile).velocity.Y = oldVelocity.Y * -0.5f;
      return false;
    }

    public virtual Asset<Texture2D> ChainTexture
    {
      get
      {
        return ModContent.Request<Texture2D>("FargowiltasSouls/Content/Patreon/Potato/RazorChain", (AssetRequestMode) 2);
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) Vector2.Distance(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center) < 5.0)
        return true;
      Vector2 vector2_1 = ((Entity) this.Projectile).Center;
      Vector2 mountedCenter = Main.player[this.Projectile.owner].MountedCenter;
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector((float) Utils.Width(this.ChainTexture) * 0.5f, (float) Utils.Height(this.ChainTexture) * 0.5f);
      float num1 = (float) Utils.Height(this.ChainTexture);
      Vector2 vector2_3 = Vector2.op_Subtraction(mountedCenter, vector2_1);
      float num2 = (float) Math.Atan2((double) vector2_3.Y, (double) vector2_3.X) - 1.57f;
      bool flag = (!float.IsNaN(vector2_1.X) || !float.IsNaN(vector2_1.Y)) && (!float.IsNaN(vector2_3.X) || !float.IsNaN(vector2_3.Y));
      int num3 = 0;
      while (flag && num3++ <= 100)
      {
        if ((double) ((Vector2) ref vector2_3).Length() < (double) num1 + 1.0)
        {
          flag = false;
        }
        else
        {
          Vector2 vector2_4 = vector2_3;
          ((Vector2) ref vector2_4).Normalize();
          vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_4, num1));
          vector2_3 = Vector2.op_Subtraction(mountedCenter, vector2_1);
          Color color = Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0));
          Main.spriteBatch.Draw(Asset<Texture2D>.op_Explicit(this.ChainTexture), Vector2.op_Subtraction(vector2_1, Main.screenPosition), new Rectangle?(), color, num2, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
        }
      }
      return true;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      if ((double) this.Projectile.ai[0] != 1.0)
        return;
      ((NPC.HitModifiers) ref modifiers).SetCrit();
      ref AddableFloat local = ref modifiers.ArmorPenetration;
      local = AddableFloat.op_Addition(local, 10f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (!Main.player[this.Projectile.owner].Alive() || !this.Retreating)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, -0.5f);
      }
      if ((double) this.Projectile.ai[0] == 1.0)
        this.Projectile.ai[0] = 2f;
      base.OnHitNPC(target, hit, damageDone);
    }
  }
}
