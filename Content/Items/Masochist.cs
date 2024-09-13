// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Masochist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.NPCs;
using Fargowiltas.Projectiles;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class Masochist : SoulsItem
  {
    public virtual string Texture => "FargowiltasSouls/Content/Items/Placeholder";

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public static bool CanPlayMaso
    {
      get
      {
        if (WorldSavingSystem.CanPlayMaso)
          return true;
        return ((Entity) Main.LocalPlayer).active && Main.LocalPlayer.FargoSouls().Toggler.CanPlayMaso;
      }
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      if (!Masochist.CanPlayMaso)
        return;
      Mod mod = ((ModType) this).Mod;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
      interpolatedStringHandler.AppendLiteral("Mods.");
      interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
      interpolatedStringHandler.AppendLiteral(".Items.");
      interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
      interpolatedStringHandler.AppendLiteral(".ExtraTooltip");
      string textValue = Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
      TooltipLine tooltipLine = new TooltipLine(mod, "tooltip", textValue);
      tooltips.Add(tooltipLine);
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if (!Masochist.CanPlayMaso || (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName")) && (!(((TooltipLine) line).Mod == ((ModType) this).Mod.Name) || !(((TooltipLine) line).Name == "tooltip")))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
      shader.TrySetParameter("mainColor", (object) new Color(28, 222, 152));
      shader.TrySetParameter("secondaryColor", (object) new Color(168, 245, 228));
      shader.Apply("PulseUpwards");
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
    }

    public virtual bool? UseItem(Player player)
    {
      if (FargoSoulsUtil.WorldIsExpertOrHarder())
      {
        if (!Luminance.Common.Utilities.Utilities.AnyBosses())
        {
          WorldSavingSystem.ShouldBeEternityMode = !WorldSavingSystem.ShouldBeEternityMode;
          int num = ModContent.NPCType<Deviantt>();
          if (FargoSoulsUtil.HostCheck && WorldSavingSystem.ShouldBeEternityMode && !WorldSavingSystem.SpawnedDevi && !NPC.AnyNPCs(num))
          {
            WorldSavingSystem.SpawnedDevi = true;
            Vector2 vector2 = Main.zenithWorld || Main.remixWorld ? ((Entity) player).Center : Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(1000f, Vector2.UnitY));
            Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, (float) num, 0.0f, 0.0f);
            FargoSoulsUtil.PrintLocalization("Announcement.HasAwoken", new Color(175, 75, (int) byte.MaxValue), (object) Language.GetTextValue("Mods.Fargowiltas.NPCs.Deviantt.DisplayName"));
          }
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      else
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
        interpolatedStringHandler.AppendLiteral("Mods.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
        interpolatedStringHandler.AppendLiteral(".Items.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
        interpolatedStringHandler.AppendLiteral(".WrongDifficulty");
        FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(175, 75, (int) byte.MaxValue));
      }
      return new bool?(true);
    }
  }
}
