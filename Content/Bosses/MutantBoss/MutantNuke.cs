// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantNuke
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantNuke : ModProjectile
  {
    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/BossWeapons/FishNuke" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantNuke_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 30;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.scale = 4f;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = WorldSavingSystem.MasochistModeReal ? 120 : 180;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item20, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) || Main.npc[EModeGlobalNPC.mutantBoss].dontTakeDamage)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (WorldSavingSystem.MasochistModeReal && Main.getGoodWorld && (double) ++this.Projectile.localAI[2] > 6.0)
        {
          this.Projectile.localAI[2] = 0.0f;
          this.Projectile.AI();
        }
        ((Entity) this.Projectile).velocity.Y += this.Projectile.ai[0];
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
        if ((double) ++this.Projectile.localAI[0] >= 24.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          for (int index1 = 0; index1 < 36; ++index1)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, -8f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 3.14159274101257 / 36.0 * 2.0, new Vector2())), new Vector2(2f, 4f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].scale = 2f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2, 6f));
            Main.dust[index2].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.0f);
          }
        }
        Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) this.Projectile.rotation, new Vector2()), 8f), 2f);
        int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
        Main.dust[index].scale = 1.5f;
        Main.dust[index].noGravity = true;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num = 36;
      for (int index1 = 0; index1 < num; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) ((Entity) this.Projectile).width / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * 6.28318548202515 / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Main.dust[index2].velocity = vector2_2;
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900, true, false);
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
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 5)
      {
        Color cyan = Color.Cyan;
        ((Color) ref cyan).A = (byte) 0;
        Color color = Color.op_Multiply(cyan, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
