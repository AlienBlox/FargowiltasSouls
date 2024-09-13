// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.BaseEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public abstract class BaseEnchant : SoulsItem
  {
    public static int[] CraftsInto;
    public static int[] Force;
    private int drawTimer;

    public abstract Color nameColor { get; }

    public string wizardEffect()
    {
      string textValue = Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".WizardEffect." + ((ModType) this).Name.Replace("Enchantment", "").Replace("Enchant", ""));
      return textValue.Contains("Mods." + ((ModType) this).Mod.Name + ".WizardEffect") || textValue.Length <= 1 ? Language.GetTextValue("Mods.FargowiltasSouls.WizardEffect.NoUpgrade") : textValue;
    }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ItemID.Sets.ItemNoGravity[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      TooltipLine tooltipLine1;
      if (tooltips.TryFindTooltipLine("ItemName", out tooltipLine1))
        tooltipLine1.OverrideColor = new Color?(this.nameColor);
      FargoSoulsPlayer fargoSoulsPlayer = Main.LocalPlayer.FargoSouls();
      if (!fargoSoulsPlayer.WizardTooltips || this.Type == ModContent.ItemType<WizardEnchant>())
        return;
      if (fargoSoulsPlayer.ForceEffect(new int?(this.Type)))
      {
        if (this.wizardEffect().Length == 0)
          return;
        List<TooltipLine> tooltipLineList = tooltips;
        Mod mod = ((ModType) this).Mod;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
        interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.WizardEffect.Active"));
        interpolatedStringHandler.AppendLiteral(" [i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<WizardEnchant>());
        interpolatedStringHandler.AppendLiteral("]: ");
        string str = interpolatedStringHandler.ToStringAndClear() + this.wizardEffect();
        TooltipLine tooltipLine2 = new TooltipLine(mod, "wizard", str);
        tooltipLineList.Add(tooltipLine2);
      }
      else
      {
        if (this.wizardEffect().Length == 0)
          return;
        List<TooltipLine> tooltipLineList = tooltips;
        Mod mod = ((ModType) this).Mod;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
        interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.WizardEffect.Inactive"));
        interpolatedStringHandler.AppendLiteral(" [i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<WizardEnchant>());
        interpolatedStringHandler.AppendLiteral("]: ");
        string str = interpolatedStringHandler.ToStringAndClear() + this.wizardEffect();
        TooltipLine tooltipLine3 = new TooltipLine(mod, "wizard", str);
        tooltipLineList.Add(tooltipLine3);
        tooltips[tooltips.Count - 1].OverrideColor = new Color?(Color.Gray);
      }
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
    }

    public virtual bool PreDrawInInventory(
      SpriteBatch spriteBatch,
      Vector2 position,
      Rectangle frame,
      Color drawColor,
      Color itemColor,
      Vector2 origin,
      float scale)
    {
      if (Main.LocalPlayer.FargoSouls().WizardedItem == this.Item)
      {
        for (int index = 0; index < 12; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), 1f);
          float num1 = (float) (0.5 + Math.Sin((double) this.drawTimer / 30.0) / 6.0);
          Color color1 = Color.Blue;
          ((Color) ref color1).A = (byte) 0;
          Color color2 = color1;
          color1 = Color.Silver;
          ((Color) ref color1).A = (byte) 0;
          Color color3 = color1;
          double num2 = (double) num1;
          Color color4 = Color.op_Multiply(Color.Lerp(color2, color3, (float) num2), 0.5f);
          Texture2D texture2D = TextureAssets.Item[this.Item.type].Value;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(position, vector2), new Rectangle?(), color4, 0.0f, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Item.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      ++this.drawTimer;
      return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }

    public virtual void UpdateEquip(Player player)
    {
      player.FargoSouls().EquippedEnchants.Add(this);
    }
  }
}
