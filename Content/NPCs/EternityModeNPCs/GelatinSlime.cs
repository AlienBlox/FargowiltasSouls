// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.GelatinSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class GelatinSlime : ModNPC
  {
    public virtual string Texture => "Terraria/Images/NPC_658";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.SpecificDebuffImmunity[this.Type] = NPCID.Sets.SpecificDebuffImmunity[657];
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.UIInfoProvider = (IBestiaryUICollectionInfoProvider) new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[657], true);
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[3]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      this.NPC.CloneDefaults(658);
      this.NPC.lifeMax = 40;
      this.NPC.damage = 30;
      this.NPC.aiStyle = -1;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.timeLeft = NPC.activeTime * 30;
      this.NPC.noTileCollide = true;
      this.NPC.scale *= 1.5f;
      this.NPC.lifeMax *= 3;
    }

    public virtual bool CanHitNPC(NPC target) => false;

    public virtual bool CanHitPlayer(Player target, ref int cooldownSlot) => false;

    public virtual void AI()
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.queenSlimeBoss, 657))
      {
        if (Main.npc[EModeGlobalNPC.queenSlimeBoss].GetGlobalNPC<QueenSlime>().RainTimer > -90)
          Main.npc[EModeGlobalNPC.queenSlimeBoss].GetGlobalNPC<QueenSlime>().RainTimer = -90;
        if (!WorldSavingSystem.MasochistModeReal && Main.npc[EModeGlobalNPC.queenSlimeBoss].GetGlobalNPC<QueenSlime>().StompTimer > -30)
          Main.npc[EModeGlobalNPC.queenSlimeBoss].GetGlobalNPC<QueenSlime>().StompTimer = -30;
      }
      if ((double) --this.NPC.ai[0] > 0.0)
      {
        ((Entity) this.NPC).position.X += this.NPC.ai[2];
        ((Entity) this.NPC).position.Y += this.NPC.ai[3];
        this.NPC.ai[3] += this.NPC.ai[1];
        ((Entity) this.NPC).velocity = Vector2.Zero;
      }
      else
      {
        this.NPC.noTileCollide = false;
        if (Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height))
          ((Entity) this.NPC).position.Y -= 16f;
        if ((double) this.NPC.ai[0] < -210.0)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            float num = !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.queenSlimeBoss, 657) || Main.npc[EModeGlobalNPC.queenSlimeBoss].life >= Main.npc[EModeGlobalNPC.queenSlimeBoss].lifeMax / 2 ? 8f : 12f;
            for (int index = 0; index < 20; ++index)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(Utils.NextFloat(Main.rand, -0.5f, 0.5f), Utils.NextFloat(Main.rand, -num, -4f)), 920, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          this.NPC.life = 0;
          this.NPC.HitEffect(0, 10.0, new bool?());
          this.NPC.checkDead();
        }
      }
      if ((double) this.NPC.ai[0] != 0.0)
        return;
      ((Entity) this.NPC).velocity.Y = 12f;
    }

    public virtual bool CheckDead()
    {
      if (this.NPC.DeathSound.HasValue)
      {
        SoundStyle soundStyle = this.NPC.DeathSound.Value;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      ((Entity) this.NPC).active = false;
      return false;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 59, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        Main.dust[index2].scale += 0.75f;
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      ++this.NPC.frameCounter;
      if (this.NPC.frameCounter > 4.0)
      {
        this.NPC.frame.Y += frameHeight;
        this.NPC.frameCounter = 0.0;
      }
      if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
        return;
      this.NPC.frame.Y = 0;
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (!TextureAssets.Npc[658].IsLoaded)
        return false;
      Texture2D texture2D = TextureAssets.Npc[658].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
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
