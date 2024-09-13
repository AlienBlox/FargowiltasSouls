// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.UIToggle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.UI;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class UIToggle : UIElement
  {
    public const int CheckboxTextSpace = 4;
    public AccessoryEffect Effect;
    public string Mod;

    public static DynamicSpriteFont Font => FontAssets.ItemStack.Value;

    public UIToggle(AccessoryEffect effect, string mod)
    {
      this.Effect = effect;
      this.Mod = mod;
      ((StyleDimension) ref this.Width).Set(19f, 0.0f);
      ((StyleDimension) ref this.Height).Set(21f, 0.0f);
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 vector2_1 = ((CalculatedStyle) ref dimensions).Position();
      FargoSoulsPlayer fargoSoulsPlayer = Main.LocalPlayer.FargoSouls();
      if (this.IsMouseHovering && Main.mouseLeft && Main.mouseLeftRelease)
      {
        fargoSoulsPlayer.Toggler.Toggles[this.Effect].ToggleBool = !fargoSoulsPlayer.Toggler.Toggles[this.Effect].ToggleBool;
        if (Main.netMode == 1)
          fargoSoulsPlayer.SyncToggle(this.Effect);
      }
      int num1 = this.Effect.MinionEffect || this.Effect.ExtraAttackEffect ? (fargoSoulsPlayer.PrimeSoulActive ? 1 : 0) : 0;
      bool flag1 = fargoSoulsPlayer.MutantPresence && !this.Effect.IgnoresMutantPresence;
      bool flag2 = this.Effect.MinionEffect && fargoSoulsPlayer.Toggler_MinionsDisabled || this.Effect.ExtraAttackEffect && fargoSoulsPlayer.Toggler_ExtraAttacksDisabled;
      bool toggleValue = Main.LocalPlayer.GetToggleValue(this.Effect, true);
      spriteBatch.Draw(FargoUIManager.CheckBox.Value, vector2_1, Color.White);
      if (num1 != 0)
        spriteBatch.Draw(FargoUIManager.Cross.Value, vector2_1, Color.Cyan);
      else if (((!flag1 ? 0 : (fargoSoulsPlayer.PresenceTogglerTimer <= 50 ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
        spriteBatch.Draw(FargoUIManager.Cross.Value, vector2_1, toggleValue ? Color.White : Color.Gray);
      else if (toggleValue)
        spriteBatch.Draw(FargoUIManager.CheckMark.Value, vector2_1, Color.White);
      string str1 = this.Effect.ToggleDescription;
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(vector2_1, new Vector2(this.Width.Pixels * Main.UIScale, 0.0f)), new Vector2(4f, 0.0f)), new Vector2(0.0f, UIToggle.Font.MeasureString(str1).Y * 0.175f));
      Color color1 = Color.White;
      if (this.Effect.ToggleItemType > 0)
      {
        Item obj = ContentSamples.ItemsByType[this.Effect.ToggleItemType];
        if (obj.ModItem != null)
        {
          if (obj.ModItem is BaseEnchant modItem2)
            color1 = modItem2.nameColor;
          else if (obj.ModItem is BaseEssence modItem1)
            color1 = modItem1.nameColor;
        }
      }
      if (num1 != 0)
      {
        color1 = Color.op_Multiply(Color.Cyan, 0.5f);
        string str2 = str1;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        interpolatedStringHandler.AppendLiteral(" [i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<PrimeSoul>());
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str2 + stringAndClear;
      }
      else if (flag1)
      {
        Color color2 = Color.op_Multiply(Color.Gray, 0.5f);
        if (fargoSoulsPlayer.PresenceTogglerTimer > 50)
        {
          color1 = Color.Lerp(color2, color1, (float) (fargoSoulsPlayer.PresenceTogglerTimer - 50) / 50f);
        }
        else
        {
          color1 = color2;
          string str3 = str1;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
          interpolatedStringHandler.AppendLiteral(" [i:");
          interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<OncomingMutantItem>());
          interpolatedStringHandler.AppendLiteral("]");
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          str1 = str3 + stringAndClear;
        }
      }
      Utils.DrawBorderString(spriteBatch, str1, vector2_2, color1, 1f, 0.0f, 0.0f, -1);
      if (fargoSoulsPlayer.PresenceTogglerTimer <= 0)
        return;
      Vector2 vector2_3 = Vector2.op_Multiply(Vector2.UnitX, (float) Utils.Lerp(-1500.0, 1500.0, (double) fargoSoulsPlayer.PresenceTogglerTimer / 100.0));
      Vector2 vector2_4 = Vector2.op_Addition(vector2_2, vector2_3);
      Vector2 vector2_5 = Vector2.op_Addition(vector2_4, Vector2.op_Multiply(Vector2.UnitX, 50f));
      Texture2D texture2D = TextureAssets.Projectile[ModContent.ProjectileType<MonkDashDamage>()].Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2_6 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      int num2 = 18;
      int num3 = 0;
      int num4 = -2;
      float num5 = 1.3f;
      float num6 = 15f;
      for (int index = num2; num4 > 0 && index < num3 || num4 < 0 && index > num3; index += num4)
      {
        Color cyan = Color.Cyan;
        float num7 = (float) (num3 - index);
        if (num4 < 0)
          num7 = (float) (num2 - index);
        Color color3 = Color.op_Multiply(cyan, num7 / 15f);
        Vector2 vector2_7 = Vector2.op_Addition(vector2_4, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Subtraction(vector2_5, vector2_4), num7), 15f));
        float num8 = 0.0f;
        SpriteEffects spriteEffects = (SpriteEffects) 0;
        if (!Vector2.op_Equality(vector2_7, Vector2.Zero))
        {
          Vector2 vector2_8 = vector2_7;
          Main.EntitySpriteDraw(texture2D, vector2_8, new Rectangle?(rectangle), color3, num8, vector2_6, MathHelper.Lerp(1f, num5, (float) index / num6), spriteEffects, 0.0f);
        }
      }
    }
  }
}
