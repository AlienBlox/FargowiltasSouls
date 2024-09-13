// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.SoulsItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public abstract class SoulsItem : ModItem
  {
    public bool HasDisabledEffects;

    public virtual bool Eternity => false;

    public virtual List<string> Articles
    {
      get
      {
        List<string> articles = new List<string>();
        CollectionsMarshal.SetCount<string>(articles, 1);
        Span<string> span = CollectionsMarshal.AsSpan<string>(articles);
        int num1 = 0;
        span[num1] = "The";
        int num2 = num1 + 1;
        return articles;
      }
    }

    public virtual void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
    }

    public virtual string Glowmaskstring => this.Texture + "_glow";

    public virtual int NumFrames => 1;

    public virtual void SafePostDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      float rotation,
      float scale,
      int whoAmI)
    {
    }

    public virtual void PostDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      float rotation,
      float scale,
      int whoAmI)
    {
      Asset<Texture2D> asset;
      if (((ModType) this).Mod.RequestAssetIfExists<Texture2D>(this.Glowmaskstring, ref asset))
      {
        Item obj = Main.item[whoAmI];
        Texture2D texture2D = ModContent.Request<Texture2D>(this.Glowmaskstring, (AssetRequestMode) 1).Value;
        int num1 = texture2D.Height / this.NumFrames;
        int width = texture2D.Width;
        int num2 = this.NumFrames > 1 ? num1 * Main.itemFrame[whoAmI] : 0;
        SpriteEffects spriteEffects = ((Entity) obj).direction < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, width, num1);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(((Entity) obj).Center.X, ((Entity) obj).position.Y + (float) ((Entity) obj).height - (float) (num1 / 2));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2, Main.screenPosition), new Rectangle?(rectangle), Color.White, rotation, Vector2.op_Division(Utils.Size(rectangle), 2f), scale, spriteEffects, 0.0f);
      }
      this.SafePostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
    }

    public virtual void ModifyTooltips(List<TooltipLine> tooltips)
    {
      TooltipLine tooltipLine;
      if (tooltips.TryFindTooltipLine("ItemName", out tooltipLine))
      {
        if (this.Eternity)
          tooltipLine.OverrideColor = new Color?(FargowiltasSouls.FargowiltasSouls.EModeColor());
        tooltipLine.ArticlePrefixAdjustment(this.Articles.ToArray());
      }
      this.SafeModifyTooltips(tooltips);
      if (this.Eternity)
        tooltips.Add(new TooltipLine(((ModType) this).Mod, ((ModType) this).Mod.Name + ":Eternity", Language.GetTextValue("Mods.FargowiltasSouls.Items.Extra.EternityItem")));
      if (!this.HasDisabledEffects)
        return;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
      interpolatedStringHandler.AppendLiteral("[i:");
      interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<TogglerIconItem>());
      interpolatedStringHandler.AppendLiteral("] [c/BC5252:");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.Items.Extra.DisabledEffects"));
      interpolatedStringHandler.AppendLiteral("]");
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      tooltips.Add(new TooltipLine(((ModType) this).Mod, ((ModType) this).Mod.Name + ":DisabledEffects", stringAndClear));
    }
  }
}
