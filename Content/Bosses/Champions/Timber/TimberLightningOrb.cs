// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberLightningOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberLightningOrb : ModProjectile
  {
    private readonly int originalSize = 70;
    private NPC npc;
    private bool spawned;

    public virtual string Texture => "Terraria/Images/Projectile_465";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = this.originalSize;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 360;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 0.1f;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity))
        return;
      this.npc = entity;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.npc != null ? ((Entity) this.npc).whoAmI : -1);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.npc = FargoSoulsUtil.NPCExists(reader.ReadInt32(), Array.Empty<int>());
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        if (!Main.dedServ)
        {
          SoundStyle roar = SoundID.Roar;
          ((SoundStyle) ref roar).Volume = 0.5f;
          SoundEngine.PlaySound(ref roar, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
      }
      this.Projectile.rotation += Utils.NextFloat(Main.rand, -0.2f, 0.2f);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      this.Projectile.scale += 0.3f;
      if ((double) this.Projectile.scale > 3.0)
        this.Projectile.scale = 3f;
      ((Entity) this.Projectile).width = (int) ((double) this.originalSize * (double) this.Projectile.scale);
      ((Entity) this.Projectile).height = (int) ((double) this.originalSize * (double) this.Projectile.scale);
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      if (this.Projectile.timeLeft < 30)
      {
        this.Projectile.alpha += 10;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
        }
      }
      if (++this.Projectile.frameCounter > 2)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      if (Utils.NextBool(Main.rand, 3))
      {
        float num1 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
        if ((double) num1 < -0.5)
          num1 = -0.5f;
        if ((double) num1 > 0.5)
          num1 = 0.5f;
        Vector2 vector2_1 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.2f * this.Projectile.scale, 0.0f), (double) num1 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
        int index1 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
        Main.dust[index1].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_1, this.Projectile.scale));
        Main.dust[index1].velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Main.dust[index1].position, ((Entity) this.Projectile).Center)), 2f);
        Main.dust[index1].noGravity = true;
        float num2 = (float) (Main.rand.NextDouble() * 1.0 - 0.5);
        if ((double) num2 < -0.5)
          num2 = -0.5f;
        if ((double) num2 > 0.5)
          num2 = 0.5f;
        Vector2 vector2_2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) -((Entity) this.Projectile).width * 0.6f * this.Projectile.scale, 0.0f), (double) num2 * 6.28318548202515, new Vector2()), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2());
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.One, 5f)), 10, 10, 226, (float) (-(double) ((Entity) this.Projectile).velocity.X / 3.0), (float) (-(double) ((Entity) this.Projectile).velocity.Y / 3.0), 150, Color.Transparent, 0.7f);
        Main.dust[index2].velocity = Vector2.Zero;
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, this.Projectile.scale));
        Main.dust[index2].noGravity = true;
      }
      float num = (float) (((Entity) this.npc).width / 2 + ((Entity) this.Projectile).width);
      if ((double) ((Entity) this.Projectile).Distance(((Entity) this.npc).Center) >= (double) num)
        return;
      ((Entity) this.npc).Center = Vector2.Lerp(((Entity) this.npc).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) this.npc).Center), num)), 0.1f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
