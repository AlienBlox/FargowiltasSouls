// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.CrystalLeaf
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class CrystalLeaf : ModNPC
  {
    public virtual string Texture => "Terraria/Images/Projectile_226";

    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.ImmuneToAllBuffs[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 28;
      ((Entity) this.NPC).height = 28;
      this.NPC.damage = 60;
      this.NPC.defense = WorldSavingSystem.MasochistModeReal ? 9999 : 20;
      this.NPC.lifeMax = WorldSavingSystem.MasochistModeReal ? 9999 : 4500;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.dontCountMe = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.alpha = (int) byte.MaxValue;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.NPC.lifeMax = 9999;
      this.NPC.life = 9999;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.NPC.localAI[2]);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[2] = reader.ReadSingle();
    }

    public virtual bool? CanBeHitByProjectile(Projectile projectile)
    {
      return ProjectileID.Sets.IsAWhip[projectile.type] ? new bool?(false) : base.CanBeHitByProjectile(projectile);
    }

    public virtual void AI()
    {
      bool flag1 = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      if (this.NPC.buffType[0] != 0)
        this.NPC.DelBuff(0);
      NPC npc = FargoSoulsUtil.NPCExists(this.NPC.ai[0], 262);
      if (npc == null || WorldSavingSystem.SwarmActive)
      {
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        ((Entity) this.NPC).active = false;
        this.NPC.netUpdate = true;
      }
      else
      {
        if ((double) this.NPC.localAI[1] == 0.0)
        {
          this.NPC.localAI[1] = 1f;
          for (int index1 = 0; index1 < 30; ++index1)
          {
            int num = flag1 ? (Utils.NextBool(Main.rand) ? 41 : 307) : (Utils.NextBool(Main.rand) ? 107 : 157);
            Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 4f, 4f);
            int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, num, vector2.X, vector2.Y, 0, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
          }
        }
        this.NPC.target = npc.target;
        if (flag1)
          Lighting.AddLight(((Entity) this.NPC).Center, 0.09803922f, 0.184313729f, 0.2509804f);
        else
          Lighting.AddLight(((Entity) this.NPC).Center, 0.1f, 0.4f, 0.2f);
        this.NPC.scale = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.20000000298023224 + 0.949999988079071);
        ((Entity) this.NPC).position = Vector2.op_Addition(((Entity) npc).Center, Utils.RotatedBy(new Vector2(this.NPC.ai[1], 0.0f), (double) this.NPC.ai[3], new Vector2()));
        ((Entity) this.NPC).position.X -= (float) (((Entity) this.NPC).width / 2);
        ((Entity) this.NPC).position.Y -= (float) (((Entity) this.NPC).height / 2);
        bool flag2 = false;
        if ((double) npc.GetLifePercent() < 0.25)
        {
          if ((double) npc.ai[1] == 1.0)
            this.NPC.scale *= 1.5f;
          flag2 = true;
        }
        if (!flag2 && npc.GetGlobalNPC<Plantera>().RingTossTimer > 120 && npc.GetGlobalNPC<Plantera>().RingTossTimer < 165 && (double) this.NPC.ai[1] == 130.0)
        {
          this.NPC.localAI[3] = 1f;
          this.NPC.scale *= 1.5f;
        }
        else
        {
          this.NPC.localAI[3] = 0.0f;
          float num = 1f;
          if ((double) this.NPC.localAI[0] > 90.0)
            this.NPC.localAI[0] = 90f;
          this.NPC.ai[3] += ((double) this.NPC.ai[1] == 130.0 ? 0.03f : -0.015f) * num;
          if ((double) this.NPC.ai[3] > 3.1415927410125732)
          {
            this.NPC.ai[3] -= 6.28318548f;
            this.NPC.netUpdate = true;
          }
          this.NPC.rotation = this.NPC.ai[3] + 1.57079637f;
          if ((double) this.NPC.ai[1] > 130.0)
          {
            this.NPC.ai[2] += (float) Math.PI / 180f * num;
            if ((double) this.NPC.ai[2] > 3.1415927410125732)
              this.NPC.ai[2] -= 6.28318548f;
            this.NPC.ai[1] += (float) (Math.Sin((double) this.NPC.ai[2]) * 7.0) * num;
            this.NPC.scale *= 1.5f;
          }
        }
        this.NPC.alpha -= (double) this.NPC.ai[1] > 130.0 ? 2 : 3;
        if (this.NPC.alpha < 0)
          this.NPC.alpha = 0;
        this.NPC.dontTakeDamage = this.NPC.alpha > 0;
      }
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => this.NPC.alpha == 0;

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
    }

    public virtual void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      modifiers.Null();
      ++this.NPC.life;
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      if (FargoSoulsUtil.CanDeleteProjectile(projectile))
        projectile.penetrate = 0;
      modifiers.Null();
      ++this.NPC.life;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int num = flag ? (Utils.NextBool(Main.rand) ? 307 : 41) : (Utils.NextBool(Main.rand) ? 107 : 157);
        Vector2 vector2 = Utils.NextVector2Circular(Main.rand, 4f, 4f);
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.NPC).position, ((Entity) this.NPC).Size), ((Entity) this.NPC).width * 2, ((Entity) this.NPC).height * 2, num, vector2.X, vector2.Y, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      }
    }

    public virtual bool CheckActive() => false;

    public virtual bool PreKill() => false;

    public virtual bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
    {
      return new bool?(!WorldSavingSystem.MasochistModeReal);
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      int num = (int) ((double) byte.MaxValue * ((double) Main.mouseTextColor / 200.0 - 0.30000001192092896)) + 50;
      if (num > (int) byte.MaxValue)
        num = (int) byte.MaxValue;
      return new Color?(Color.op_Multiply(new Color(num, num, num, 200), this.NPC.Opacity));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = (!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/NPCs/EternityModeNPCs/CrystalLeaf", (AssetRequestMode) 2).Value : TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      Color color1 = Color.op_Multiply(alpha, 0.75f);
      if (this.NPC.alpha == 0)
      {
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
        {
          Color color2 = color1;
          ((Color) ref color2).A = (double) this.NPC.localAI[3] == 0.0 ? (byte) 150 : (byte) 0;
          color2 = Color.op_Multiply(color2, (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
          Vector2 oldPo = this.NPC.oldPos[index];
          float rotation = this.NPC.rotation;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color2, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color1, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
