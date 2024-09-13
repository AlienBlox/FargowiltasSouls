// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomSaucer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomSaucer : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 5;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 9);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<MutantNibbleBuff>();
      int num8 = num7 + 1;
      span[num8] = ModContent.BuffType<OceanicMaulBuff>();
      int num9 = num8 + 1;
      span[num9] = ModContent.BuffType<LightningRodBuff>();
      int num10 = num9 + 1;
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.PortraitScale = new float?(1f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>()], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[3]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 25;
      ((Entity) this.NPC).height = 25;
      this.NPC.defense = 90;
      this.NPC.lifeMax = 600;
      this.NPC.scale = 2f;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath14);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.dontTakeDamage = true;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.damage = (int) ((double) this.NPC.damage * 0.5);
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => false;

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.NPC.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
      if (npc == null || npc.dontTakeDamage)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        this.NPC.checkDead();
        ((Entity) this.NPC).active = false;
      }
      else
      {
        this.NPC.target = npc.target;
        this.NPC.dontTakeDamage = (double) npc.ai[0] == 0.0 && (double) npc.ai[2] < 3.0;
        if ((double) ++this.NPC.ai[1] > 90.0)
        {
          ((Entity) this.NPC).velocity = Vector2.Zero;
          if ((double) this.NPC.ai[3] == 0.0)
          {
            this.NPC.localAI[2] = ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center);
            this.NPC.ai[3] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center));
            if (((Entity) this.NPC).whoAmI == NPC.FindFirstNPC(this.NPC.type) && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) Main.player[this.NPC.target]).Center, Vector2.Zero, ModContent.ProjectileType<AbomReticle>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          if ((double) this.NPC.ai[1] > 120.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index1 = 0; index1 < 5; ++index1)
              {
                Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(16f, Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.ai[3]), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 12.0, new Vector2())), Utils.NextFloat(Main.rand, 0.9f, 1.1f));
                int index2 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomLaser>(), FargoSoulsUtil.ScaledProjectileDamage(npc.damage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (index2 != Main.maxProjectiles)
                  Main.projectile[index2].timeLeft = (int) ((double) this.NPC.localAI[2] / (double) ((Vector2) ref vector2).Length()) + 1;
              }
            }
            this.NPC.netUpdate = true;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[3] = 0.0f;
          }
        }
        else
        {
          Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) Main.player[this.NPC.target]).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[2], new Vector2()), (double) this.NPC.ai[1] < 45.0 ? 200f : 500f)), ((Entity) this.NPC).Center), 8f);
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, 19f), vector2), 20f);
        }
        this.NPC.ai[2] -= 0.045f;
        if ((double) this.NPC.ai[2] < -3.1415927410125732)
          this.NPC.ai[2] += 6.28318548f;
        if ((double) this.NPC.localAI[1] == 0.0)
          this.NPC.localAI[1] = Utils.NextBool(Main.rand) ? 1f : -1f;
        this.NPC.rotation = (float) (Math.Sin(2.0 * Math.PI * (double) this.NPC.localAI[0]++ / 90.0) * 3.1415927410125732 / 8.0) * this.NPC.localAI[1];
        if ((double) this.NPC.localAI[0] <= 180.0)
          return;
        this.NPC.localAI[0] = 0.0f;
      }
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      if (this.NPC.life > 0)
        return;
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
      }
    }

    public virtual Color? GetAlpha(Color drawColor) => new Color?(Color.White);

    public virtual bool CheckActive() => false;

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
