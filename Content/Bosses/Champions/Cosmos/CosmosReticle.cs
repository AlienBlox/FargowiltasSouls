// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosReticle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosReticle : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 110;
      ((Entity) this.Projectile).height = 110;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<CosmosChampion>());
      if (npc == null || (double) npc.ai[0] != 11.0)
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[npc.target];
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        if ((double) ++this.Projectile.ai[1] > 45.0)
        {
          if ((double) this.Projectile.ai[1] % 5.0 == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item89, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 center = ((Entity) this.Projectile).Center;
              center.X += (float) Main.rand.Next(-200, 201);
              center.Y -= 700f;
              Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 10f, 15f), Vector2.Normalize(Vector2.op_Subtraction(((Entity) this.Projectile).Center, center)));
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), center, vector2, ModContent.ProjectileType<CosmosMeteor>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, Utils.NextFloat(Main.rand, 1f, 1.5f), 0.0f);
            }
          }
          if ((double) this.Projectile.ai[1] > 90.0)
          {
            this.Projectile.ai[1] = 0.0f;
            this.Projectile.localAI[0] = 0.0f;
            this.Projectile.netUpdate = true;
          }
          this.Projectile.rotation = 0.0f;
          this.Projectile.alpha = 0;
          this.Projectile.scale = 1f;
        }
        else
        {
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
          this.Projectile.localAI[0] = MathHelper.Lerp(this.Projectile.localAI[0], ((Entity) player).velocity.X * 30f, 0.1f);
          ((Entity) this.Projectile).position.X += this.Projectile.localAI[0];
          SoundStyle soundStyle;
          if ((double) this.Projectile.ai[1] % 15.0 == 0.0 && !Main.dedServ)
          {
            soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleBeep", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
          if ((double) this.Projectile.ai[1] == 45.0)
          {
            this.Projectile.netUpdate = true;
            if (!Main.dedServ)
            {
              soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ReticleLockOn", (SoundType) 0);
              SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            }
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -5f, 0.0f);
          }
          float num = (float) (1.0 - (double) this.Projectile.ai[1] / 45.0);
          this.Projectile.rotation = 9.424778f * num;
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) num);
          this.Projectile.scale = (float) (1.0 + 2.0 * (double) num);
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
