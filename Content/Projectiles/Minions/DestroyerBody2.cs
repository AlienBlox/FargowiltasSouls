// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.DestroyerBody2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class DestroyerBody2 : ModProjectile
  {
    public int attackTimer;

    public virtual string Texture => "Terraria/Images/NPC_135";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      Main.projFrames[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 300;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
      this.Projectile.netImportant = true;
      this.Projectile.hide = true;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
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

    public virtual Color? GetAlpha(Color lightColor) => new Color?(lightColor);

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Minions/DestroyerBody2_glow", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Color alpha1 = this.Projectile.GetAlpha(Lighting.GetColor((int) ((double) ((Entity) this.Projectile).Center.X / 16.0), (int) ((double) ((Entity) this.Projectile).Center.Y / 16.0)));
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(new Rectangle(0, num2, texture2D1.Width, num1)), alpha1, this.Projectile.rotation, new Vector2((float) texture2D1.Width / 2f, (float) num1 / 2f), this.Projectile.scale, this.Projectile.spriteDirection == 1 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY));
      Rectangle? nullable = new Rectangle?(new Rectangle(0, num2, texture2D1.Width, num1));
      Color alpha2 = this.Projectile.GetAlpha(Color.White);
      double rotation = (double) this.Projectile.rotation;
      Vector2 vector2_2 = new Vector2((float) texture2D1.Width / 2f, (float) num1 / 2f);
      double scale = (double) this.Projectile.scale;
      int num3 = this.Projectile.spriteDirection == 1 ? 0 : 1;
      Main.EntitySpriteDraw(texture2D2, vector2_1, nullable, alpha2, (float) rotation, vector2_2, (float) scale, (SpriteEffects) num3, 0.0f);
      return false;
    }

    public virtual void AI()
    {
      Main.player[this.Projectile.owner].FargoSouls();
      if ((int) Main.time % 120 == 0)
        this.Projectile.netUpdate = true;
      int num1 = 30;
      bool flag = false;
      Vector2 vector2_1 = Vector2.Zero;
      Vector2 zero = Vector2.Zero;
      float num2 = 0.0f;
      if ((double) this.Projectile.ai[1] == 1.0)
      {
        this.Projectile.ai[1] = 0.0f;
        this.Projectile.netUpdate = true;
      }
      int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[0], new int[2]
      {
        this.Projectile.type,
        ModContent.ProjectileType<DestroyerHead2>()
      });
      if (projectileByIdentity >= 0 && ((Entity) Main.projectile[projectileByIdentity]).active)
      {
        flag = true;
        vector2_1 = ((Entity) Main.projectile[projectileByIdentity]).Center;
        Vector2 velocity = ((Entity) Main.projectile[projectileByIdentity]).velocity;
        num2 = Main.projectile[projectileByIdentity].rotation;
        double num3 = (double) MathHelper.Clamp(Main.projectile[projectileByIdentity].scale, 0.0f, 50f);
        if (Main.projectile[projectileByIdentity].alpha == 0)
        {
          this.Projectile.alpha -= 84;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        Main.projectile[projectileByIdentity].localAI[0] = this.Projectile.localAI[0] + 1f;
        if (Main.projectile[projectileByIdentity].type != ModContent.ProjectileType<DestroyerHead2>())
          Main.projectile[projectileByIdentity].localAI[1] = (float) this.Projectile.identity;
      }
      if (!flag)
        return;
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
      if ((double) num2 != (double) this.Projectile.rotation)
      {
        float num4 = MathHelper.WrapAngle(num2 - this.Projectile.rotation);
        vector2_2 = Utils.RotatedBy(vector2_2, (double) num4 * 0.10000000149011612, new Vector2());
      }
      this.Projectile.rotation = Utils.ToRotation(vector2_2) + 1.57079637f;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = (int) ((double) num1 * (double) this.Projectile.scale);
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      if (Vector2.op_Inequality(vector2_2, Vector2.Zero))
        ((Entity) this.Projectile).Center = Vector2.op_Subtraction(vector2_1, Vector2.op_Multiply(Vector2.Normalize(vector2_2), 36f));
      this.Projectile.spriteDirection = (double) vector2_2.X > 0.0 ? 1 : -1;
      if (--this.attackTimer <= 0)
        this.attackTimer = Main.rand.Next(90) + 90;
      if (this.attackTimer != 1 || this.Projectile.owner != Main.myPlayer)
        return;
      int index1 = -1;
      for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
      {
        if (Main.npc[index2].CanBeChasedBy((object) this.Projectile, false) && Collision.CanHit(((Entity) this.Projectile).Center, 0, 0, ((Entity) Main.npc[index2]).Center, 0, 0) && (double) ((Entity) this.Projectile).Distance(((Entity) Main.npc[index2]).Center) <= 750.0 && Utils.NextBool(Main.rand))
          index1 = index2;
      }
      if (index1 == -1)
        return;
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.npc[index1]).Center), (double) Utils.NextFloat(Main.rand, -0.7853982f, 0.7853982f), new Vector2()), ModContent.ProjectileType<MechElectricOrbHomingFriendly>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, (float) index1, 0.0f, 0.0f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 6;
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), Main.rand.Next(300, 1200), false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      if (Main.dedServ)
        return;
      int index = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f), ModContent.Find<ModGore>("FargowiltasSouls/DestroyerGunEXBodyGore").Type, this.Projectile.scale);
      Main.gore[index].timeLeft = 20;
    }
  }
}
