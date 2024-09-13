// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.GelatinSubject
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class GelatinSubject : ModNPC
  {
    public virtual string Texture => "Terraria/Images/NPC_660";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[660];
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.SpecificDebuffImmunity[this.Type] = NPCID.Sets.SpecificDebuffImmunity[657];
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      this.NPC.CloneDefaults(660);
      this.AIType = 660;
      this.NPC.lifeMax = 120;
      this.NPC.damage = 50;
      this.NPC.lifeMax *= 10;
      this.NPC.timeLeft = NPC.activeTime * 30;
      this.NPC.scale *= 1.5f;
      ((Entity) this.NPC).width = ((Entity) this.NPC).height = (int) ((double) ((Entity) this.NPC).height * 0.9);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.NPC.knockBackResist *= 0.1f;
    }

    public virtual void AI()
    {
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.queenSlimeBoss, 657) && !NPC.AnyNPCs(657))
      {
        this.NPC.life = 0;
        this.NPC.HitEffect(0, 10.0, new bool?());
        this.NPC.checkDead();
      }
      else
      {
        foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == this.NPC.type && ((Entity) n).whoAmI != ((Entity) this.NPC).whoAmI && (double) ((Entity) this.NPC).Distance(((Entity) n).Center) < (double) ((Entity) this.NPC).width)))
        {
          ((Entity) this.NPC).velocity.X += (float) (0.02500000037252903 * ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) npc).Center.X ? -1.0 : 1.0));
          ((Entity) this.NPC).velocity.Y += (float) (0.02500000037252903 * ((double) ((Entity) this.NPC).Center.Y < (double) ((Entity) npc).Center.Y ? -1.0 : 1.0));
          ((Entity) npc).velocity.X += (float) (0.02500000037252903 * ((double) ((Entity) npc).Center.X < (double) ((Entity) this.NPC).Center.X ? -1.0 : 1.0));
          ((Entity) npc).velocity.Y += (float) (0.02500000037252903 * ((double) ((Entity) npc).Center.Y < (double) ((Entity) this.NPC).Center.Y ? -1.0 : 1.0));
        }
        this.NPC.spriteDirection = ((Entity) this.NPC).direction;
        this.NPC.rotation = Math.Abs(((Entity) this.NPC).velocity.X * 0.1f) * (float) ((Entity) this.NPC).direction;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) < 600.0 && (Main.npc[EModeGlobalNPC.queenSlimeBoss].GetGlobalNPC<QueenSlime>().RainTimer > 0 || NPC.AnyNPCs(ModContent.NPCType<GelatinSlime>())))
          this.NPC.localAI[0] = 90f;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.npc[EModeGlobalNPC.queenSlimeBoss]).Center) > 2000.0)
          ((Entity) this.NPC).Center = ((Entity) Main.npc[EModeGlobalNPC.queenSlimeBoss]).Center;
        if ((double) this.NPC.localAI[0] > 0.0)
          --this.NPC.localAI[0];
        if (!this.NPC.HasValidTarget || (double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.NPC).velocity) - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center)))) >= 1.5707963705062866)
          return;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) < 80.0)
        {
          NPC npc = this.NPC;
          ((Entity) npc).position = Vector2.op_Subtraction(((Entity) npc).position, Vector2.op_Multiply(((Entity) this.NPC).velocity, 0.33f));
        }
        else
        {
          if ((double) this.NPC.localAI[0] <= 0.0)
            return;
          float num = this.NPC.localAI[0] / 90f;
          NPC npc = this.NPC;
          ((Entity) npc).position = Vector2.op_Subtraction(((Entity) npc).position, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.NPC).velocity, 0.66f), num));
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(137, 180, true, false);
    }

    public virtual bool CheckActive() => false;

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
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        Main.dust[index2].scale += 0.75f;
      }
      for (int index = 0; index < 2; ++index)
      {
        if (!Main.dedServ)
          Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height))), Vector2.op_Division(((Entity) this.NPC).velocity, 2f), 1260, this.NPC.scale);
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
      if (!TextureAssets.Npc[660].IsLoaded)
        return false;
      Texture2D texture2D = TextureAssets.Npc[660].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 1 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect) null, Main.Transform);
      GameShaders.Misc["HallowBoss"].Apply(new DrawData?());
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return false;
    }
  }
}
