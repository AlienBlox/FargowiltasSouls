// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PhantasmalEyeLeashProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PhantasmalEyeLeashProj : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_452";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = 2;
      this.Projectile.timeLeft = 180;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 0.0)
        return;
      this.Projectile.ai[0] = 0.0f;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 2000f), Array.Empty<int>());
      if (!npc.Alive() || (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) <= (double) Math.Max(((Entity) npc).width, ((Entity) npc).height))
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 45f), 0.2f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 1;
      this.Projectile.timeLeft = 0;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.op_Multiply(Color.Lerp(new Color(31, 187, 192, 0), Color.Transparent, 0.84f), 0.5f);
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Utils.RotatedBy(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f), 0.62831854820251465 - (double) index * 3.1415927410125732 / 5.0, new Vector2())), Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 8f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      for (float index = this.Projectile.ai[0] - 1f; (double) index > 0.0; index -= this.Projectile.ai[0] / 6f)
      {
        float num3 = 0.2f;
        if ((double) index > 5.0 && (double) index < 10.0)
          num3 = 0.4f;
        if ((double) index >= 10.0)
          num3 = 0.6f;
        Color color2 = Color.op_Multiply(Color.Lerp(color1, Color.Transparent, 0.1f + num3), (float) ((int) (((double) this.Projectile.ai[0] - (double) index) / (double) this.Projectile.ai[0]) ^ 2));
        float num4 = this.Projectile.scale * (this.Projectile.ai[0] - index) / this.Projectile.ai[0] + (float) Math.Sin((double) this.Projectile.ai[1]) / 10f;
        Vector2 vector2_4 = Vector2.op_Subtraction(this.Projectile.oldPos[(int) index], Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_4, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num4 * 0.8f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
