// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantDestroyerHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantDestroyerHead : ModProjectile
  {
    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_134" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantDestroyerHead_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 900;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.netImportant = true;
      this.CooldownSlot = 1;
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

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = (double) this.Projectile.ai[2] == 0.0 ? TextureAssets.Projectile[this.Projectile.type].Value : ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_13", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(new Rectangle(0, num2, texture2D.Width, num1)), this.Projectile.GetAlpha(Color.White), this.Projectile.rotation, new Vector2((float) texture2D.Width / 2f, (float) num1 / 2f), this.Projectile.scale, this.Projectile.spriteDirection == 1 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      return false;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
      float num1 = 10f * this.Projectile.ai[1];
      float num2 = 25f / this.Projectile.ai[1];
      if ((double) ++this.Projectile.localAI[1] > 60.0)
      {
        int index = (int) this.Projectile.ai[0];
        Player player = Main.player[index];
        if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 700.0)
        {
          num1 *= 2f;
          num2 /= 2f;
        }
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), num1), 1f / num2);
      }
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == this.Projectile.type && ((Entity) p).whoAmI != ((Entity) this.Projectile).whoAmI && (double) ((Entity) p).Distance(((Entity) this.Projectile).Center) < (double) ((Entity) this.Projectile).width)))
      {
        ((Entity) this.Projectile).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) this.Projectile).position.X < (double) ((Entity) projectile).position.X ? -1.0 : 1.0));
        ((Entity) this.Projectile).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) this.Projectile).position.Y < (double) ((Entity) projectile).position.Y ? -1.0 : 1.0));
        ((Entity) projectile).velocity.X += (float) (0.05000000074505806 * ((double) ((Entity) projectile).position.X < (double) ((Entity) this.Projectile).position.X ? -1.0 : 1.0));
        ((Entity) projectile).velocity.Y += (float) (0.05000000074505806 * ((double) ((Entity) projectile).position.Y < (double) ((Entity) this.Projectile).position.Y ? -1.0 : 1.0));
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff((double) this.Projectile.ai[2] == 0.0 ? ModContent.BuffType<LightningRodBuff>() : 33, Main.rand.Next(300, 1200), true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 62, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      SoundEngine.PlaySound(ref SoundID.NPCDeath14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }
  }
}
