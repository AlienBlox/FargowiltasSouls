// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Sky.MainMenuBackgroundStyle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Sky
{
  public class MainMenuBackgroundStyle : ModSurfaceBackgroundStyle
  {
    private float intensity = 0.6f;
    private float lifeIntensity = 1f;
    private float specialColorLerp;
    private Color? specialColor;
    private int delay;
    private readonly int[] xPos = new int[50];
    private readonly int[] yPos = new int[50];
    public int fadeIn;

    public virtual void ModifyFarFades(float[] fades, float transitionSpeed)
    {
      for (int index = 0; index < fades.Length; ++index)
      {
        if (index == ((ModBackgroundStyle) this).Slot)
        {
          fades[index] += transitionSpeed;
          if ((double) fades[index] > 1.0)
            fades[index] = 1f;
        }
        else
        {
          fades[index] -= transitionSpeed;
          if ((double) fades[index] < 0.0)
            fades[index] = 0.0f;
        }
      }
    }

    private Color ColorToUse(ref float opacity)
    {
      Color use;
      // ISSUE: explicit constructor call
      ((Color) ref use).\u002Ector(51, (int) byte.MaxValue, 191);
      opacity = (float) ((double) this.intensity * 0.5 + (double) this.lifeIntensity * 0.5);
      opacity *= Math.Min((float) this.fadeIn / 60f, 1f);
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

    public virtual bool PreDrawCloseBackground(SpriteBatch spriteBatch)
    {
      ++this.fadeIn;
      float opacity = 0.0f;
      Color use = this.ColorToUse(ref opacity);
      spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantSky", (AssetRequestMode) 1).Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.op_Multiply(use, opacity));
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
        spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/MutantStatic", (AssetRequestMode) 1).Value, new Rectangle(this.xPos[index] - num / 2, this.yPos[index], num, 3), Color.op_Multiply(Color.op_Multiply(use, 1f), 0.75f));
      }
      return base.PreDrawCloseBackground(spriteBatch);
    }
  }
}
