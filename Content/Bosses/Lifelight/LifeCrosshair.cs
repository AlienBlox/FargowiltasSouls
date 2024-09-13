// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeCrosshair
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeCrosshair : ModProjectile
  {
    private int npc = -1;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = (int) byte.MaxValue;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity) || entity.type != ModContent.NPCType<LifeChallenger>())
        return;
      this.npc = ((Entity) entity).whoAmI;
      if ((double) this.Projectile.ai[1] != 2.0)
        return;
      this.Projectile.localAI[1] = MathHelper.WrapAngle(Utils.ToRotation(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) entity).Center)) - Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[entity.target]).Center, ((Entity) entity).Center)));
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.npc);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.npc = reader.Read7BitEncodedInt();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] > 0.0)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
      this.Projectile.alpha -= 12;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.scale = 2f - this.Projectile.Opacity;
      if ((double) this.Projectile.ai[1] == 1.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, 0.030543264001607895, new Vector2());
      }
      else
      {
        if ((double) this.Projectile.ai[1] != 2.0)
          return;
        NPC npc = FargoSoulsUtil.NPCExists(this.npc, Array.Empty<int>());
        if (npc == null)
          return;
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(vector2, (double) this.Projectile.localAI[1], new Vector2()));
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      if ((double) this.Projectile.ai[1] != 1.0 && (double) this.Projectile.ai[1] != 3.0 || !FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<LifeCageExplosion>(), this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200), this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
