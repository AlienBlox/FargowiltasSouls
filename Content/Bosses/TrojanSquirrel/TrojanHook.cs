// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanHook
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public class TrojanHook : ModProjectile
  {
    private NPC npc;
    private Vector2 offset;
    private int dir;

    public virtual string Texture => "Terraria/Images/Projectile_13";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 8000;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity))
        return;
      this.npc = entity;
      this.offset = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) this.npc).Center);
      this.dir = ((Entity) entity).direction;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.npc != null ? ((Entity) this.npc).whoAmI : -1);
      Utils.WritePackedVector2(writer, this.offset);
      writer.Write((byte) this.dir);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.npc = FargoSoulsUtil.NPCExists(reader.ReadInt32(), Array.Empty<int>());
      this.offset = Utils.ReadPackedVector2(reader);
      this.dir = (int) reader.ReadByte();
    }

    public virtual void PostAI()
    {
      if (this.npc == null || this.dir == ((Entity) this.npc).direction)
        return;
      this.dir = ((Entity) this.npc).direction;
      this.offset.X *= -1f;
    }

    public virtual void AI()
    {
      if (this.npc != null)
        this.npc = FargoSoulsUtil.NPCExists(((Entity) this.npc).whoAmI, Array.Empty<int>());
      if (this.npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        this.Projectile.extraUpdates = WorldSavingSystem.EternityMode ? 1 : 0;
        if ((double) this.Projectile.ai[0] == 0.0)
        {
          if (!Collision.SolidTiles(((Entity) this.Projectile).Center, 0, 0))
            this.Projectile.tileCollide = true;
        }
        else if ((double) this.Projectile.ai[0] == 1.0)
        {
          this.Projectile.tileCollide = false;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          this.Projectile.ai[0] = 2f;
          this.Projectile.netUpdate = true;
        }
        if ((double) this.Projectile.ai[0] == 2.0)
        {
          ++this.Projectile.extraUpdates;
          this.Projectile.tileCollide = false;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(12f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, this.ChainOrigin));
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) this.npc).position, ((Entity) this.npc).oldPosition), 2f));
          if ((double) ((Entity) this.Projectile).Distance(this.ChainOrigin) < 12.0)
            this.Projectile.Kill();
        }
        else if ((double) ((Entity) this.Projectile).Distance(this.ChainOrigin) > 1600.0)
        {
          this.Projectile.ai[0] = 2f;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(this.ChainOrigin)) + 1.57079637f;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.Projectile.ai[0] = 1f;
      return false;
    }

    private Vector2 ChainOrigin
    {
      get
      {
        return this.npc != null ? Vector2.op_Addition(((Entity) this.npc).Center, this.offset) : ((Entity) this.Projectile).Center;
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return this.npc == null || !WorldSavingSystem.EternityMode ? base.Colliding(projHitbox, targetHitbox) : new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), this.ChainOrigin, ((Entity) this.Projectile).Center));
    }

    protected virtual bool flashingZapEffect => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D lightningTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/TrojanSquirrel/TrojanHookLightning", (AssetRequestMode) 2).Value;
      int lightningFrames = 5;
      if (this.npc != null && TextureAssets.Chain.IsLoaded)
      {
        Texture2D texture2D = TextureAssets.Chain.Value;
        Vector2 position = ((Entity) this.Projectile).Center;
        Vector2 chainOrigin = this.ChainOrigin;
        Rectangle? nullable = new Rectangle?();
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector((float) texture2D.Width * 0.5f, (float) texture2D.Height * 0.5f);
        float height = (float) texture2D.Height;
        Vector2 vector2_2 = Vector2.op_Subtraction(chainOrigin, position);
        float rotation = (float) Math.Atan2((double) vector2_2.Y, (double) vector2_2.X) - 1.57f;
        bool flag = true;
        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
          flag = false;
        if (float.IsNaN(vector2_2.X) && float.IsNaN(vector2_2.Y))
          flag = false;
        while (flag)
        {
          if ((double) ((Vector2) ref vector2_2).Length() < (double) height + 1.0)
          {
            flag = false;
          }
          else
          {
            Vector2 vector2_3 = vector2_2;
            ((Vector2) ref vector2_3).Normalize();
            position = Vector2.op_Addition(position, Vector2.op_Multiply(vector2_3, height));
            vector2_2 = Vector2.op_Subtraction(chainOrigin, position);
            Color color1 = Lighting.GetColor((int) position.X / 16, (int) ((double) position.Y / 16.0));
            Color color2 = this.flashingZapEffect ? Color.op_Multiply(Color.White, this.Projectile.Opacity) : this.Projectile.GetAlpha(color1);
            Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(position, Main.screenPosition), nullable, color2, rotation, vector2_1, 1f, (SpriteEffects) 0, 0.0f);
            int num = Utils.NextBool(Main.rand) ? 1 : 0;
            if (num != 0)
              DrawLightning(position, lightColor, rotation);
            if (this.flashingZapEffect)
            {
              ((Color) ref color2).A = (byte) 0;
              Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(position, Main.screenPosition), nullable, color2, rotation, vector2_1, 1f, (SpriteEffects) 0, 0.0f);
            }
            if (num == 0)
              DrawLightning(position, lightColor, rotation);
          }
        }
      }
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Color color = this.flashingZapEffect ? Color.op_Multiply(Color.White, this.Projectile.Opacity) : this.Projectile.GetAlpha(lightColor);
      int num3 = Utils.NextBool(Main.rand) ? 1 : 0;
      if (num3 != 0)
        DrawLightning(((Entity) this.Projectile).Center, lightColor, this.Projectile.rotation);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      if (this.flashingZapEffect)
      {
        ((Color) ref color).A = (byte) 0;
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      if (num3 == 0)
        DrawLightning(((Entity) this.Projectile).Center, lightColor, this.Projectile.rotation);
      return false;

      Rectangle GetRandomLightningFrame()
      {
        int num1 = lightningTexture.Height / lightningFrames;
        int num2 = Main.rand.Next(lightningFrames);
        return new Rectangle(0, num1 * num2, lightningTexture.Width, num1);
      }

      void DrawLightning(Vector2 position, Color color, float rotation)
      {
        if (!Utils.NextBool(Main.rand, 4))
          return;
        Rectangle randomLightningFrame = GetRandomLightningFrame();
        Vector2 vector2 = Vector2.op_Division(Utils.Size(randomLightningFrame), 2f);
        Main.EntitySpriteDraw(lightningTexture, Vector2.op_Subtraction(position, Main.screenPosition), new Rectangle?(randomLightningFrame), Color.White, rotation, vector2, 1f, (SpriteEffects) 0, 0.0f);
      }
    }
  }
}
