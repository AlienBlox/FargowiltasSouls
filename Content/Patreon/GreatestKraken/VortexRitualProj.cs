// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.GreatestKraken.VortexRitualProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.GreatestKraken
{
  public class VortexRitualProj : ModProjectile
  {
    private int syncTimer;
    private Vector2 mousePos;
    private const int baseDimension = 70;

    public virtual string Texture => "Terraria/Images/Projectile_465";

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 70;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 0.5f;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().NinjaCanSpeedup = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(((Entity) this.Projectile).width);
      writer.Write7BitEncodedInt(((Entity) this.Projectile).height);
      writer.Write(this.Projectile.scale);
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      ((Entity) this.Projectile).width = reader.Read7BitEncodedInt();
      ((Entity) this.Projectile).height = reader.Read7BitEncodedInt();
      this.Projectile.scale = reader.ReadSingle();
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      this.Projectile.timeLeft = 2;
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active || player.HeldItem.type != ModContent.ItemType<VortexMagnetRitual>() || !player.channel || !player.CheckMana(player.HeldItem.mana, false, false))
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.damage = player.GetWeaponDamage(player.HeldItem, false);
        this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
        this.Projectile.knockBack = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
        this.Projectile.alpha -= 10;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        if (this.Projectile.owner == Main.myPlayer)
        {
          if (--this.syncTimer < 0)
          {
            this.syncTimer = 20;
            this.Projectile.netUpdate = true;
          }
          this.mousePos = Main.MouseWorld;
        }
        if ((double) this.Projectile.scale < 5.0)
          this.Projectile.scale *= 1.007f;
        else
          this.Projectile.scale = 5f;
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = (int) (70.0 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) (70.0 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        float num1 = (float) ((Entity) this.Projectile).width * 2f;
        if ((double) ++this.Projectile.localAI[0] > 6.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          if (this.Projectile.owner == Main.myPlayer)
          {
            int num2 = 8;
            for (int index1 = 0; index1 < Main.maxNPCs; ++index1)
            {
              NPC npc = Main.npc[index1];
              if (npc.CanBeChasedBy((object) null, false) && (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < (double) num1)
              {
                Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.Projectile).width / 4), (float) (((Entity) this.Projectile).height / 4)));
                if (Collision.CanHitLine(vector2_1, 0, 0, ((Entity) npc).Center, 0, 0))
                {
                  if (--num2 >= 0)
                  {
                    Vector2 vector2_2 = Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc).Center, vector2_1));
                    Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, Vector2.op_Multiply(6f, vector2_2), (int) byte.MaxValue, this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
                    int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, Vector2.op_Multiply(21f, vector2_2), ModContent.ProjectileType<VortexBolt>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(vector2_2), (float) Main.rand.Next(80), 0.0f);
                    if (index2 != Main.maxProjectiles)
                      Main.projectile[index2].DamageType = DamageClass.Magic;
                  }
                  else
                    break;
                }
              }
            }
          }
        }
        int num3 = 5 + 5 * (int) this.Projectile.scale;
        for (int index = 0; index < num3; ++index)
        {
          Vector2 vector2 = new Vector2();
          double num4 = Main.rand.NextDouble() * 2.0 * Math.PI;
          vector2.X += (float) Math.Sin(num4) * num1;
          vector2.Y += (float) Math.Cos(num4) * num1;
          Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), 0, 0, 226, 0.0f, 0.0f, 100, Color.White, this.Projectile.scale / 5f)];
          dust1.velocity = ((Entity) this.Projectile).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -Utils.NextFloat(Main.rand, 5f)));
            Dust dust3 = dust1;
            dust3.position = Vector2.op_Addition(dust3.position, Vector2.op_Multiply(dust1.velocity, 10f));
          }
          dust1.noGravity = true;
        }
        if ((double) ((Entity) this.Projectile).Distance(this.mousePos) <= 2.0)
        {
          ((Entity) this.Projectile).Center = this.mousePos;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
        }
        else
          ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, this.mousePos);
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.85f, 0.9f);
        ++this.Projectile.frameCounter;
        if (this.Projectile.frameCounter > 3)
        {
          this.Projectile.frameCounter = 0;
          ++this.Projectile.frame;
          if (this.Projectile.frame > 3)
            this.Projectile.frame = 0;
        }
        this.Projectile.rotation -= (float) Math.PI / 150f;
        this.Projectile.localAI[1] += 0.07330383f;
        if ((double) this.Projectile.rotation < 6.2831854820251465)
          this.Projectile.rotation += 6.28318548f;
        if ((double) this.Projectile.localAI[1] <= 6.2831854820251465)
          return;
        this.Projectile.localAI[1] -= 6.28318548f;
      }
    }

    public virtual void OnKill(int timeLeft) => this.MakeDust();

    private void MakeDust()
    {
      for (int index1 = 0; index1 < 25; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f * this.Projectile.scale);
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 226, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4f * this.Projectile.scale);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].noLight = true;
      }
      int num = 30 * (int) this.Projectile.scale;
      for (int index4 = 0; index4 < num; ++index4)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 10f), this.Projectile.scale), (double) ((index4 - 39) * num), new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index5 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 92, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index5].noGravity = true;
        Main.dust[index5].velocity = vector2_2;
      }
      for (int index6 = 0; index6 < (int) this.Projectile.scale; ++index6)
      {
        for (int index7 = 0; index7 < 3; ++index7)
        {
          int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Main.dust[index8].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        }
        for (int index9 = 0; index9 < 10; ++index9)
        {
          int index10 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index10].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
          Main.dust[index10].noGravity = true;
          Dust dust3 = Main.dust[index10];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 1f);
          int index11 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Main.dust[index11].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
          Dust dust4 = Main.dust[index11];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 1f);
          Main.dust[index11].noGravity = true;
        }
        for (int index12 = 0; index12 < 10; ++index12)
        {
          int index13 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index13];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index14 = 0; index14 < 10; ++index14)
        {
          int index15 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index15].noGravity = true;
          Dust dust5 = Main.dust[index15];
          dust5.velocity = Vector2.op_Multiply(dust5.velocity, 7f);
          int index16 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust6 = Main.dust[index16];
          dust6.velocity = Vector2.op_Multiply(dust6.velocity, 3f);
        }
        for (int index17 = 0; index17 < 10; ++index17)
        {
          int index18 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index18].noGravity = true;
          Dust dust7 = Main.dust[index18];
          dust7.velocity = Vector2.op_Multiply(dust7.velocity, 21f * this.Projectile.scale);
          Main.dust[index18].noLight = true;
          int index19 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust8 = Main.dust[index19];
          dust8.velocity = Vector2.op_Multiply(dust8.velocity, 12f);
          Main.dust[index19].noGravity = true;
          Main.dust[index19].noLight = true;
        }
        for (int index20 = 0; index20 < 10; ++index20)
        {
          int index21 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 2f, 3.5f));
          if (Utils.NextBool(Main.rand, 3))
            Main.dust[index21].noGravity = true;
          Dust dust = Main.dust[index21];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 9f, 12f));
        }
      }
    }

    private int GetDamage(int damage)
    {
      return (int) ((double) damage * (double) this.Projectile.scale / 5.0);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      modifiers.FinalDamage.Base = (float) this.GetDamage((int) modifiers.FinalDamage.Base);
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      modifiers.FinalDamage.Base = (float) this.GetDamage((int) modifiers.FinalDamage.Base);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Patreon/GreatestKraken/VortexRitualRing", (AssetRequestMode) 1).Value;
      Rectangle bounds = texture2D2.Bounds;
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(bounds), 2f);
      float num3 = (float) ((double) this.Projectile.scale / 360.0 * 96.0);
      float num4 = this.Projectile.rotation + this.Projectile.localAI[1];
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), this.Projectile.GetAlpha(lightColor), num4, vector2_2, num3, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
