// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.DestroyerHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class DestroyerHead : ModProjectile
  {
    public float modifier;
    private int syncTimer;
    private Vector2 mousePos;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
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
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.netImportant = true;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 10;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
      writer.Write(this.modifier);
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.modifier = reader.ReadSingle();
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(lightColor);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Minions/DestroyerHead_glow", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Color alpha1 = this.Projectile.GetAlpha(lightColor);
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
      if (((Entity) Main.player[this.Projectile.owner]).whoAmI == Main.myPlayer)
      {
        this.mousePos = Main.MouseWorld;
        if (++this.syncTimer > 20)
        {
          this.syncTimer = 0;
          this.Projectile.netUpdate = true;
        }
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        float num1 = 0.0f;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && !Main.projectile[index].hostile && Main.projectile[index].owner == this.Projectile.owner && (double) Main.projectile[index].minionSlots > 0.0)
            num1 += Main.projectile[index].minionSlots;
        }
        float num2 = (float) Main.player[this.Projectile.owner].maxMinions - num1;
        if ((double) num2 < 0.0)
          num2 = 0.0f;
        if ((double) num2 > 3.0)
          num2 = 3f;
        if (this.Projectile.owner == Main.myPlayer)
        {
          this.Projectile.netUpdate = true;
          int index1 = ((Entity) this.Projectile).whoAmI;
          for (int index2 = 0; (double) index2 <= (double) num2 * 3.0; ++index2)
            index1 = FargoSoulsUtil.NewSummonProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<DestroyerBody>(), this.Projectile.originalDamage, this.Projectile.knockBack, this.Projectile.owner, (float) Main.projectile[index1].identity);
          int index3 = index1;
          int index4 = FargoSoulsUtil.NewSummonProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, ModContent.ProjectileType<DestroyerTail>(), this.Projectile.originalDamage, this.Projectile.knockBack, this.Projectile.owner, (float) Main.projectile[index1].identity);
          Main.projectile[index3].localAI[1] = (float) Main.projectile[index4].identity;
          Main.projectile[index3].netUpdate = true;
        }
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      float num3 = (float) (20.0 + (double) this.modifier * 6.0);
      float num4 = (float) (100.0 - (double) this.modifier * 12.0);
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 30.0)
        return;
      this.Projectile.ai[0] = 30f;
      if ((double) ((Entity) this.Projectile).Distance(this.mousePos) <= 50.0)
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, this.mousePos), num3), 1f / num4);
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
      if (!Main.dedServ)
      {
        int index = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 2f), ModContent.Find<ModGore>("FargowiltasSouls/DestroyerHeadGore").Type, this.Projectile.scale);
        Main.gore[index].timeLeft = 20;
      }
      SoundEngine.PlaySound(ref SoundID.NPCDeath14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }
  }
}
