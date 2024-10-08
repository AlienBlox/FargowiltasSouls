﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Sky.MutantSky
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Sky
{
  public class MutantSky : CustomSky
  {
    private bool isActive;
    private float intensity;
    private float lifeIntensity;
    private float specialColorLerp;
    private Color? specialColor;
    private int delay;
    private readonly int[] xPos = new int[50];
    private readonly int[] yPos = new int[50];

    public virtual void Update(GameTime gameTime)
    {
      bool useSpecialColor = false;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && ((double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] < 0.0 || (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] >= 10.0))
      {
        this.intensity += 0.01f;
        this.lifeIntensity = (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] < 0.0 ? 1f : (float) (1.0 - (double) Main.npc[EModeGlobalNPC.mutantBoss].life / (double) Main.npc[EModeGlobalNPC.mutantBoss].lifeMax);
        switch ((int) Main.npc[EModeGlobalNPC.mutantBoss].ai[0])
        {
          case -5:
            if ((double) Main.npc[EModeGlobalNPC.mutantBoss].ai[2] >= 420.0)
            {
              ChangeColorIfDefault(FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 180, 50) : Color.Cyan);
              break;
            }
            break;
          case 10:
            useSpecialColor = true;
            this.specialColor = new Color?(Color.Black);
            this.specialColorLerp = 1f;
            break;
          case 27:
            ChangeColorIfDefault(Color.Red);
            break;
          case 36:
            if (WorldSavingSystem.MasochistModeReal && (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[2] > 480.0)
            {
              ChangeColorIfDefault(Color.Blue);
              break;
            }
            break;
          case 44:
            ChangeColorIfDefault(Color.DeepPink);
            break;
          case 48:
            ChangeColorIfDefault(Color.Purple);
            break;
        }
        if ((double) this.intensity > 1.0)
          this.intensity = 1f;
      }
      else
      {
        this.lifeIntensity -= 0.01f;
        if ((double) this.lifeIntensity < 0.0)
          this.lifeIntensity = 0.0f;
        this.specialColorLerp -= 0.02f;
        if ((double) this.specialColorLerp < 0.0)
          this.specialColorLerp = 0.0f;
        this.intensity -= 0.01f;
        if ((double) this.intensity < 0.0)
        {
          this.intensity = 0.0f;
          this.lifeIntensity = 0.0f;
          this.specialColorLerp = 0.0f;
          this.specialColor = new Color?();
          this.delay = 0;
          ((GameEffect) this).Deactivate(Array.Empty<object>());
          return;
        }
      }
      if (useSpecialColor)
      {
        this.specialColorLerp += 0.02f;
        if ((double) this.specialColorLerp <= 1.0)
          return;
        this.specialColorLerp = 1f;
      }
      else
      {
        this.specialColorLerp -= 0.02f;
        if ((double) this.specialColorLerp >= 0.0)
          return;
        this.specialColorLerp = 0.0f;
        this.specialColor = new Color?();
      }

      void ChangeColorIfDefault(Color color)
      {
        if (!this.specialColor.HasValue)
          this.specialColor = new Color?(color);
        if (!this.specialColor.HasValue)
          return;
        Color? specialColor = this.specialColor;
        Color color1 = color;
        if ((specialColor.HasValue ? (Color.op_Equality(specialColor.GetValueOrDefault(), color1) ? 1 : 0) : 0) == 0)
          return;
        useSpecialColor = true;
      }
    }

    private Color ColorToUse(ref float opacity)
    {
      Color use = FargoSoulsUtil.AprilFools ? Color.OrangeRed : new Color(51, (int) byte.MaxValue, 191);
      opacity = (float) ((double) this.intensity * 0.5 + (double) this.lifeIntensity * 0.5);
      if ((double) this.specialColorLerp > 0.0 && this.specialColor.HasValue)
      {
        use = Color.Lerp(use, this.specialColor.Value, this.specialColorLerp);
        Color? specialColor = this.specialColor;
        Color black = Color.Black;
        if ((specialColor.HasValue ? (Color.op_Equality(specialColor.GetValueOrDefault(), black) ? 1 : 0) : 0) != 0)
          opacity = Math.Min(1f, opacity + Math.Min(this.intensity, this.lifeIntensity) * 0.5f);
      }
      return use;
    }

    public virtual void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) maxDepth < 0.0 || (double) minDepth >= 0.0)
        return;
      float opacity = 0.0f;
      Color use = this.ColorToUse(ref opacity);
      spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantSky" + FargoSoulsUtil.TryAprilFoolsTexture, (AssetRequestMode) 1).Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.op_Multiply(use, opacity));
      if (--this.delay < 0)
      {
        this.delay = Main.rand.Next(5 + (int) (85.0 * (1.0 - (double) this.lifeIntensity)));
        for (int index = 0; index < 50; ++index)
        {
          this.xPos[index] = Main.rand.Next(Main.screenWidth);
          this.yPos[index] = Main.rand.Next(Main.screenHeight);
        }
      }
      for (int index = 0; index < 50; ++index)
      {
        int num = Main.rand.Next(3, 251);
        spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantStatic", (AssetRequestMode) 1).Value, new Rectangle(this.xPos[index] - num / 2, this.yPos[index], num, 3), Color.op_Multiply(Color.op_Multiply(use, this.lifeIntensity), 0.75f));
      }
    }

    public virtual float GetCloudAlpha() => 1f - this.intensity;

    public virtual void Activate(Vector2 position, params object[] args) => this.isActive = true;

    public virtual void Deactivate(params object[] args) => this.isActive = false;

    public virtual void Reset() => this.isActive = false;

    public virtual bool IsActive() => this.isActive;

    public virtual Color OnTileColor(Color inColor)
    {
      float opacity = 0.0f;
      return Color.Lerp(Color.Lerp(Color.White, this.ColorToUse(ref opacity), 0.5f), inColor, 1f - this.intensity);
    }
  }
}
