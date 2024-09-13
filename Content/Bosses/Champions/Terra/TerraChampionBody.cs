// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Terra.TerraChampionBody
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Terra
{
  public class TerraChampionBody : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 5;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.ImmuneToAllBuffs[this.Type] = true;
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 45;
      ((Entity) this.NPC).height = 45;
      this.NPC.damage = 140;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 170000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath14);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.behindTiles = true;
      this.NPC.chaseable = false;
      this.NPC.scale *= 1.25f;
      this.NPC.trapImmune = true;
      this.NPC.dontCountMe = true;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return (double) ((Entity) this.NPC).Distance(((Entity) target).Center) < 30.0 * (double) this.NPC.scale;
    }

    public virtual void AI()
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.NPC.ai[1], ModContent.NPCType<TerraChampion>(), ModContent.NPCType<TerraChampionBody>());
      NPC npc2 = FargoSoulsUtil.NPCExists(this.NPC.ai[3], ModContent.NPCType<TerraChampion>());
      if (npc1 == null || npc2 == null || WorldSavingSystem.EternityMode && npc1.life < npc1.lifeMax / 10)
      {
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        float num = 0.5f;
        for (int index6 = 0; index6 < 4; ++index6)
        {
          int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
          Gore gore = Main.gore[index7];
          gore.velocity = Vector2.op_Multiply(gore.velocity, num);
          ++Main.gore[index7].velocity.X;
          ++Main.gore[index7].velocity.Y;
        }
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<TerraLightningOrb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, this.NPC.ai[3], 0.0f, 0.0f);
        ((Entity) this.NPC).active = false;
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        int index8 = NPCID.Sets.TrailCacheLength[this.NPC.type] - (int) npc2.ai[3] - 1;
        if ((double) this.NPC.localAI[0] == 0.0)
        {
          this.NPC.localAI[0] = 1f;
          for (int index9 = 0; index9 < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index9)
            this.NPC.oldPos[index9] = ((Entity) this.NPC).position;
        }
        ((Entity) this.NPC).Center = Vector2.op_Addition(npc1.oldPos[index8], Vector2.op_Division(((Entity) npc1).Size, 2f));
        this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) npc1).Center));
        if ((double) ((Entity) this.NPC).Distance(Vector2.op_Addition(this.NPC.oldPos[index8 - 1], Vector2.op_Division(((Entity) this.NPC).Size, 2f))) > 45.0 * (double) this.NPC.scale)
          this.NPC.oldPos[index8 - 1] = Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(this.NPC.oldPos[index8 - 1], ((Entity) this.NPC).position)), 45f), this.NPC.scale));
        this.NPC.timeLeft = npc1.timeLeft;
      }
    }

    public virtual bool CheckActive() => false;

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ((NPC.HitModifiers) ref modifiers).SetMaxDamage(1);
      ((NPC.HitModifiers) ref modifiers).DisableCrit();
    }

    public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
      return new bool?(false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(24, 600, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D, vector2_2, nullable, alpha, (float) rotation, vector2_3, (float) scale, spriteEffects2, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Terra/" + ((ModType) this).Name + "_Glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.White, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects1, 0.0f);
      return false;
    }
  }
}
