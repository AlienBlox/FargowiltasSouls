// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.LunarCultist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class LunarCultist : ModProjectile
  {
    private Vector2 target;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 12;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 9;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
      Utils.WriteVector2(writer, this.target);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
      this.target = Utils.ReadVector2(reader);
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).active && !player.dead && player.FargoSouls().LunarCultist)
        this.Projectile.timeLeft = 2;
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
          this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
        ++this.Projectile.localAI[0];
        NPC npc1 = Main.npc[(int) this.Projectile.ai[0]];
        if (npc1.CanBeChasedBy((object) null, false))
        {
          if ((double) this.Projectile.ai[1] % 2.0 != 0.0)
          {
            NPC npc2 = FargoSoulsUtil.NPCExists(EModeGlobalNPC.moonBoss, new int[1]
            {
              398
            });
            if (npc2 != null)
            {
              switch (npc2.GetGlobalNPC<MoonLordCore>().VulnerabilityState)
              {
                case 0:
                  this.Projectile.ai[1] = 1f;
                  break;
                case 1:
                  this.Projectile.ai[1] = 3f;
                  break;
                case 2:
                  this.Projectile.ai[1] = 5f;
                  break;
                case 3:
                  this.Projectile.ai[1] = 7f;
                  break;
              }
            }
          }
          this.Projectile.localAI[1] = this.Projectile.ai[1] + 1f;
          switch ((int) this.Projectile.ai[1])
          {
            case 0:
            case 2:
            case 4:
            case 6:
              this.Projectile.localAI[0] = 0.0f;
              ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(this.target, ((Entity) this.Projectile).Center);
              float num1 = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
              if ((double) num1 > 1000.0)
              {
                this.Projectile.ai[0] = -1f;
                this.Projectile.ai[1] = 1f;
                this.Projectile.netUpdate = true;
                break;
              }
              if ((double) num1 > 24.0)
              {
                ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
                Projectile projectile = this.Projectile;
                ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 24f);
                break;
              }
              ++this.Projectile.ai[1];
              break;
            case 1:
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              if ((double) this.Projectile.localAI[0] <= 30.0 && (double) this.Projectile.localAI[0] % 10.0 == 0.0)
              {
                SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
                Vector2 center = ((Entity) this.Projectile).Center;
                center.X -= (float) (30 * this.Projectile.spriteDirection);
                center.Y += 12f;
                Vector2 vector2 = Utils.RotatedByRandom(Vector2.op_Subtraction(((Entity) npc1).Center, center), Math.PI / 6.0);
                ((Vector2) ref vector2).Normalize();
                vector2 = Vector2.op_Multiply(vector2, Utils.NextFloat(Main.rand, 6f, 10f));
                if (this.Projectile.owner == Main.myPlayer)
                {
                  int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), center, vector2, ModContent.ProjectileType<LunarCultistFireball>(), this.Projectile.damage, 9f, this.Projectile.owner, 0.0f, this.Projectile.ai[0], 0.0f);
                  if (index != Main.maxProjectiles)
                    Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Melee);
                }
              }
              if ((double) this.Projectile.localAI[0] > 60.0)
              {
                ++this.Projectile.ai[1];
                this.target = ((Entity) npc1).Center;
                this.target.Y -= (float) (((Entity) npc1).height + 100);
                break;
              }
              break;
            case 3:
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              if ((double) this.Projectile.localAI[0] == 15.0)
              {
                SoundEngine.PlaySound(ref SoundID.Item121, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
                Vector2 center = ((Entity) this.Projectile).Center;
                center.Y -= 100f;
                if (this.Projectile.owner == Main.myPlayer)
                {
                  int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), center, Vector2.Zero, ModContent.ProjectileType<LunarCultistLightningOrb>(), this.Projectile.damage, 8f, this.Projectile.owner, (float) ((Entity) this.Projectile).whoAmI, 0.0f, 0.0f);
                  if (index != Main.maxProjectiles)
                    Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Ranged);
                }
              }
              if ((double) this.Projectile.localAI[0] > 90.0)
              {
                ++this.Projectile.ai[1];
                this.target = ((Entity) npc1).Center;
                this.target.Y -= (float) (((Entity) npc1).height + 100);
                break;
              }
              break;
            case 5:
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              if ((double) this.Projectile.localAI[0] == 20.0)
              {
                Vector2 center = ((Entity) this.Projectile).Center;
                center.X -= (float) (30 * this.Projectile.spriteDirection);
                center.Y += 12f;
                Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc1).Center, center);
                ((Vector2) ref vector2).Normalize();
                vector2 = Vector2.op_Multiply(vector2, 4.25f);
                if (this.Projectile.owner == Main.myPlayer)
                {
                  int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), center, vector2, ModContent.ProjectileType<LunarCultistIceMist>(), this.Projectile.damage, this.Projectile.knockBack * 2f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
                  if (index != Main.maxProjectiles)
                    Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Magic);
                }
              }
              if ((double) this.Projectile.localAI[0] > 60.0)
              {
                ++this.Projectile.ai[1];
                this.target = ((Entity) npc1).Center;
                this.target.Y -= (float) (((Entity) npc1).height + 100);
                break;
              }
              break;
            case 7:
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              if ((double) this.Projectile.localAI[0] == 30.0)
              {
                Vector2 center = ((Entity) this.Projectile).Center;
                center.Y -= (float) ((Entity) this.Projectile).height;
                if (this.Projectile.owner == Main.myPlayer)
                  Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) -this.Projectile.spriteDirection), 12f), ModContent.ProjectileType<AncientVisionLunarCultist>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              }
              if ((double) this.Projectile.localAI[0] > 90.0)
              {
                ++this.Projectile.ai[1];
                this.target = ((Entity) npc1).Center;
                this.target.Y -= (float) (((Entity) npc1).height + 100);
                break;
              }
              break;
            default:
              this.Projectile.ai[1] = 0.0f;
              goto case 0;
          }
          if ((double) ((Entity) this.Projectile).velocity.X == 0.0)
          {
            float num2 = ((Entity) npc1).Center.X - ((Entity) this.Projectile).Center.X;
            if ((double) num2 != 0.0)
              this.Projectile.spriteDirection = (double) num2 < 0.0 ? 1 : -1;
          }
          else
            this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? 1 : -1;
        }
        else
          this.TargetEnemies();
      }
      else
      {
        if ((double) this.Projectile.ai[1] == 0.0)
        {
          if (Vector2.op_Equality(this.target, Vector2.Zero))
          {
            this.target = ((Entity) Main.player[this.Projectile.owner]).Center;
            this.target.Y -= 100f;
          }
          ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(this.target, ((Entity) this.Projectile).Center);
          float num = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
          if ((double) num > 1500.0)
          {
            ((Entity) this.Projectile).Center = ((Entity) Main.player[this.Projectile.owner]).Center;
            ((Entity) this.Projectile).velocity = Vector2.Zero;
            this.Projectile.ai[1] = 1f;
          }
          else if ((double) num > 24.0)
          {
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 24f);
          }
          else
            this.Projectile.ai[1] = 1f;
        }
        else
        {
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          ++this.Projectile.localAI[0];
          if ((double) this.Projectile.localAI[0] > 30.0)
          {
            this.TargetEnemies();
            this.Projectile.localAI[0] = 0.0f;
          }
        }
        if ((double) ((Entity) this.Projectile).velocity.X == 0.0)
        {
          float num = ((Entity) Main.player[this.Projectile.owner]).Center.X - ((Entity) this.Projectile).Center.X;
          if ((double) num != 0.0)
            this.Projectile.spriteDirection = (double) num < 0.0 ? 1 : -1;
        }
        else
          this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? 1 : -1;
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter <= 6)
        return;
      this.Projectile.frameCounter = 0;
      this.Projectile.frame = (this.Projectile.frame + 1) % 3;
      if ((double) this.Projectile.ai[0] <= -1.0 || (double) this.Projectile.ai[0] >= 200.0)
        return;
      switch (this.Projectile.ai[1])
      {
        case 1f:
          this.Projectile.frame += 6;
          break;
        case 3f:
          this.Projectile.frame += 3;
          break;
        case 5f:
          this.Projectile.frame += 6;
          break;
        case 7f:
          this.Projectile.frame += 3;
          break;
        case 9f:
          this.Projectile.frame += 6;
          break;
      }
    }

    private void TargetEnemies()
    {
      this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, true, ((Entity) Main.player[this.Projectile.owner]).Center);
      if ((double) this.Projectile.ai[0] != -1.0)
      {
        this.target = ((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center;
        this.target.Y -= (float) (((Entity) Main.npc[(int) this.Projectile.ai[0]]).height + 100);
        this.Projectile.ai[1] = this.Projectile.localAI[1];
        if ((double) this.Projectile.ai[1] % 2.0 != 0.0)
          --this.Projectile.ai[1];
      }
      else
      {
        this.target = ((Entity) Main.player[this.Projectile.owner]).Center;
        this.target.Y -= (float) (((Entity) Main.player[this.Projectile.owner]).height + 100);
        this.Projectile.ai[1] = 0.0f;
      }
      this.Projectile.netUpdate = true;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Minions/LunarCultistTrail", (AssetRequestMode) 1).Value;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1, 0.0f);
      return false;
    }
  }
}
